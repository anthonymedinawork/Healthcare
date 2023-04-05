using Data.Models;
using System.Linq.Expressions;

namespace HealthcareApp.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(string id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter);
        ValueTask<T> GetByIdAsync(string id);
        Task UpdateAsync(T entity);
    }
}
