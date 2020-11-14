using System;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using Polly;
using Serilog;
using WebAPI.Database;
using WebAPI.Models;
using WebAPI.Options;
using WebAPI.Utils;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseOptions>(Configuration.GetSection("Database"));
            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Web API",
                    Description = "My WebAPI"
                });
            });

            services.AddMediatR(typeof(Startup));
            services.AddDbContext<JobContext>(x => x.UseNpgsql(Configuration["Database:ConnectionString"]).UseSnakeCaseNamingConvention()); // lowercasenaming in order to make dapper work with EF Core created tables and columns
            services.AddSingleton<ConnectionFactory>();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection(); // we will use TLS termination on the gateway

            app.UseRouting();

            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            // workaround for postgres container not accepting connections on startup
            // retry until it accepts connection
            var retryPolicy = Policy
                .Handle<NpgsqlException>()
                .WaitAndRetry(12, retryAttempt => TimeSpan.FromSeconds(5));

            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<JobContext>())
                {
                    retryPolicy.Execute(context.Database.Migrate);
                }
            }
        }
    }
}
