using Microsoft.Extensions.Options;
using Npgsql;
using SharedKernel.Domain.Settings;
using System.Data;

namespace Infrastructure
{
    public class AppDbContext(IOptions<DatabaseSettings> dbSettings)
    {
        private readonly IOptions<DatabaseSettings> _dbSettings = dbSettings;

        /// <summary>
        /// Connection to applications own database. 
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
            => new NpgsqlConnection($"Host={_dbSettings.Value.Server}; Database={_dbSettings.Value.Database}; Username={_dbSettings.Value.UserId}; Password={_dbSettings.Value.Password};");

        /// <summary>
        /// Connection to 'postgres' master database.
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateMasterConnection()
            => new NpgsqlConnection($"Host={_dbSettings.Value.Server}; Database=postgres; Username={_dbSettings.Value.UserId}; Password={_dbSettings.Value.Password};");
    }
}