using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SharedKernel.Domain.Seedwork
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetById(int id);
        T Update(T entity);
        bool Delete(T entity);
    }
}