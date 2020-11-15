using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Npgsql;
using JobService.Options;

namespace JobService.Database
{
    public class ConnectionFactory
    {
        private readonly IOptions<DatabaseOptions> _options;

        public ConnectionFactory(IOptions<DatabaseOptions> options)
        {
            _options = options;
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_options.Value.ConnectionString);
        }
    }
}
