using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Polly;

namespace DatabaseUtils
{
    public static class DatabaseEx
    {
        public static void ApplyMigrations(this DbContext context)
        {
            // workaround for postgres container not accepting connections on startup
            // retry until it accepts connection
            var retryPolicy = Policy
                .Handle<NpgsqlException>()
                .WaitAndRetry(12, retryAttempt => TimeSpan.FromSeconds(5));

            retryPolicy.Execute(context.Database.Migrate);
        }

        public static IServiceCollection AddDatabase<T>(this IServiceCollection services, IConfiguration conf) where T : DbContext
        {
            services.Configure<DatabaseOptions>(conf.GetSection("Database"));
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddDbContext<T>(x => x.UseNpgsql(conf["Database:ConnectionString"]).UseSnakeCaseNamingConvention()); // snake_case naming in order to make dapper work with EF Core created tables and columns
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            return services;
        }
    }
}
