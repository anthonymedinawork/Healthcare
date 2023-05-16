using AutoMapper;
using Data.Models;
using HealthcareApp.Data.Enums;
using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Linq.Expressions;

namespace HealthcareApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<UserViewModel> CreateAsync(UserViewModel model)
        {
            User user = _mapper.Map<User>(model);

            var newUser = await _repository.CreateAsync(user);

            return _mapper.Map<UserViewModel>(newUser);
        }

        public async Task<UserViewModel> CreateFromViewAsync(UserViewModel model, Role role)
        {
            User user = _mapper.Map<User>(model);

            var newUser = await _repository.CreateFromViewAsync(user, role);

            return _mapper.Map<UserViewModel>(newUser);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new KeyNotFoundException(nameof(id));
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<List<UserViewModel>> GetAllAsync()
        {
            var users = await _repository.GetAll().ToListAsync();

            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<List<UserViewModel>> GetAllAsync(Expression<Func<UserViewModel, bool>> filter)
        {
            var userFilter = _mapper.Map<Expression<Func<User, bool>>>(filter);

            List<User> users = await _repository.GetAll(userFilter).ToListAsync();

            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetByIdAsync(string id)
        {
            User? user = await _repository.GetByIdAsync(id);

            if (user is null)
            {
                throw new ArgumentNullException($"No such {typeof(User)} with id: {id}");
            }
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> GetByUserNameAsync(string username)
        {
            User? user = await _repository.GetByIdAsync(username);

            if (user is null)
            {
                throw new ArgumentNullException($"No such {typeof(User)} with id: {username}");
            }
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> UpdateAsync(UserViewModel model)
        {
            User user = _mapper.Map<User>(model);

            var newUser = await _repository.UpdateAsync(user);

            return _mapper.Map<UserViewModel>(newUser);
        }
    }
}
