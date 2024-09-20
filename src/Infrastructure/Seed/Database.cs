using Dapper;
using Infrastructure.DAL;
using Microsoft.Extensions.Options;
using SharedKernel.Domain.Settings;

namespace Infrastructure.Seed
{
    public class Database(AppDbContext context, IOptions<DatabaseSettings> settings)
    {
        private readonly AppDbContext _context = context;
        private readonly DatabaseSettings _dbSettings = settings.Value;


        public void CreateDatabase()
        {
            using var connection = _context.CreateMasterConnection();
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
            var dbCount = connection.ExecuteScalar<int>(sqlDbCount);
            if (dbCount == 0)
            {
                var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
                connection.Execute(sql);
            }
        }
    }
}