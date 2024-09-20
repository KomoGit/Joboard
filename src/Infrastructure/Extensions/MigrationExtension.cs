using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharedKernel.Domain.Settings;
using System.Reflection;

namespace Infrastructure.Extensions
{
    public static class MigrationExtension
    {
        public static void Migrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<Database>();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            database.CreateDatabase();
            runner.MigrateUp();
        }

        public static void AddFluentMigratorService(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddFluentMigratorCore()
                   //for seeing logs in the console
                   .ConfigureRunner(conf =>
                   {
                       conf.AddPostgres()
                       .WithGlobalConnectionString(configuration.GetConnectionString("Default"))
                           .ScanIn(Assembly.Load("Infrastructure.Migrations")).For.All();
                   });
        }
    }
}
