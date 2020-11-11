using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace WebAPI.Database
{
    public class ConnectionFactory
    {
        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection("Server=db;Port=5432;Database=Jobs;Username=postgres;Password=password;");
        }
    }
}
