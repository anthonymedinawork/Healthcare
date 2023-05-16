using HealthcareApp.Data.Enums;
using HealthcareApp.Services.ViewModels;

namespace HealthcareApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> CreateAsync(UserViewModel user);
        Task<UserViewModel> CreateFromViewAsync(UserViewModel user, Role role);
        Task<UserViewModel> UpdateAsync(UserViewModel user);
        Task DeleteAsync(string id);
        Task<UserViewModel> GetByIdAsync(string id);
        Task<UserViewModel> GetByUserNameAsync(string username);
        Task<List<UserViewModel>> GetAllAsync();
    }
}
