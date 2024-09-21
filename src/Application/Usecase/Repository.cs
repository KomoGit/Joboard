using Dapper;
using Domain.Exceptions;
using Infrastructure.DAL;
using SharedKernel.Domain.Seedwork;
using System.Data;
using System.Data.Common;

namespace Application.Usecase
{
    public class Repository<T>(AppDbContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly IDbConnection _connection = context.CreateConnection();

        public async Task<int> AddAsync(T entity, string schema)
        {
            var query = $"INSERT INTO {schema}.{GetTableName()} {GetInsertColumns()}";
            return await _connection.ExecuteAsync(query, entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            string query = $"SELECT * FROM {GetTableName()}";
            IEnumerable<T> data = await _connection.QueryAsync<T>(query);
            return data.ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            string query = $"SELECT * FROM {GetTableName()} WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id }) 
                ?? throw new EntityNotFoundException<T>();
        }

        public async Task<int> Update(T entity)
        {
            string query = $"UPDATE {GetTableName()} {GetInsertColumns()}";
            return await _connection.ExecuteAsync(query, entity);
        }

        public async Task<int> Delete(T entity)
        {
            string query = $"DELETE FROM {GetTableName()} WHERE Id = @Id";
            return await _connection.ExecuteAsync(query);
        }

        private static string GetInsertColumns()
        {
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var paramNames = string.Join(", ", properties.Select(p => "@" + p.Name));

            return $"({columnNames}) VALUES ({paramNames})";
        }

        private static string GetTableName()
        {
            return $"{typeof(T).Name.ToLower().Replace("entity","")}";
        }
    }
}