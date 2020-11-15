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
    public class GetAllJobsHandler : IRequestHandler<GetAllJobsQuery, ResponseItems<JobResponse>>
    {
        private readonly IConnectionFactory _factory;

        private static readonly string QUERY_TEMPLATE = $"SELECT /**select**/ FROM {JobItem.Table} /**where**/ /**orderby**/ LIMIT @Limit";

        public GetAllJobsHandler(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<ResponseItems<JobResponse>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var sqlBuilder = new SqlBuilder();
            var template = sqlBuilder.AddTemplate(QUERY_TEMPLATE);
            var limit = filter.PageSize + 1; // add 1 so we can check if next page exists (but we dont return last one)
            sqlBuilder.AddParameters(new { Limit = limit });

            sqlBuilder.Select("*");
            sqlBuilder.OrderBy($"{JobItem.Columns.SerialNumber} DESC");
            if (!string.IsNullOrEmpty(filter.Name))
            {
                sqlBuilder.Where($"{JobItem.Columns.Name} ILIKE @Name", new { Name = $"{EscapeForLike(filter.Name)}%" });
            }

            if (filter.NextCursor != null)
            {
                sqlBuilder.Where($"{JobItem.Columns.SerialNumber} <= @Cursor", new { Cursor = filter.NextCursor });
            }

            using var conn = _factory.GetConnection();
            var jobs = await conn.QueryAsync<JobItem>(template.RawSql, template.Parameters);

            var jobList = jobs.ToList();
            bool isLastPage = jobList.Count < limit;
            int? nextCursor = null;
            if (!isLastPage)
            {
                nextCursor = jobList[^1].SerialNumber;
                jobList.RemoveAt(jobList.Count - 1);
            }

            var items = jobList.Select(job => JobResponse.From(job));
            return new ResponseItems<JobResponse>(items, nextCursor, filter.PageSize);
        }

        public static string EscapeForLike(string value)
        {
            return value.Replace("_", @"\\_").Replace("[", @"\\[").Replace("%", @"\\%");
        }
    }
}
