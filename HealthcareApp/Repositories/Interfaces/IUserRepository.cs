using Data.Models;
using HealthcareApp.Data.Enums;
using System.Linq.Expressions;

namespace HealthcareApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User entity);
        Task<User> CreateFromViewAsync(User entity, Role role);
        Task DeleteAsync(string id);
        IQueryable<User> GetAll();
        IQueryable<User> GetAll(Expression<Func<User, bool>> filter);
        ValueTask<User> GetByIdAsync(string id);
        Task<User> GetByUserNameAsync(string username);
        Task<User> UpdateAsync(User entity);
    }
}
