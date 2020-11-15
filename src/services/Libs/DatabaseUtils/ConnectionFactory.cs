using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Npgsql;

namespace DatabaseUtils
{
    internal class ConnectionFactory : IConnectionFactory
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
