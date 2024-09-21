using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SharedKernel.Domain.Seedwork
{
    public interface IRepository<T>
    {
        Task<int> AddAsync(T entity, string schema);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}