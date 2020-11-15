using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using JobService.DTO;
using JobService.Models;
using JobService.Queries;
using DatabaseUtils;

namespace JobService.Handlers
{
    public class GetAllJobsHandler : IRequestHandler<GetAllJobsQuery, IEnumerable<JobResponse>>
    {
        private readonly IConnectionFactory _factory;

        private static readonly string QUERY_TEMPLATE = $"SELECT /**select**/ FROM {JobItem.Table} /**where**/ /**orderby**/";

        public GetAllJobsHandler(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<JobResponse>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate(QUERY_TEMPLATE);

            sqlBuilder.Select("*");
            sqlBuilder.OrderBy($"{JobItem.Columns.CreatedAt} DESC");
            if (!string.IsNullOrEmpty(filter.Name))
            {
                sqlBuilder.Where($"{nameof(JobItem.Columns.Name)} ILIKE @Name", new { Name = $"{EscapeForLike(filter.Name)}%" });
            }

            using var conn = _factory.GetConnection();
            var jobs = await conn.QueryAsync<JobItem>(template.RawSql, template.Parameters);

            return jobs.Select(job => JobResponse.From(job));
        }

        public static string EscapeForLike(string value)
        {
            return value.Replace("_", @"\\_").Replace("[", @"\\[").Replace("%", @"\\%");
        }
    }
}
