using Dapper;
using Domain.Exceptions;
using Infrastructure.DAL;
using SharedKernel.Domain.Seedwork;
using System.Data;

namespace Application.Usecase
{
    public class Repository<T>(AppDbContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly IDbConnection _connection = context.CreateConnection();

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            string query = $"SELECT * FROM {typeof(T).Name.ToLower()}s";
            IEnumerable<T> data = await _connection.QueryAsync<T>(query);
            return data.ToList();
        }

        public async Task<T> GetById(int id)
        {
            string query = $"SELECT * FROM {typeof(T).Name.ToLower()}s WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id }) 
                ?? throw new EntityNotFoundException<T>();
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
