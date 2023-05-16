using Data.Models;
using HealthcareApp.Data;
using HealthcareApp.Data.Enums;
using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthcareApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly HealthcareAppDbContext _context;
        private SignInManager<User> _signManager;
        private UserManager<User> _userManager;

        public UserRepository(HealthcareAppDbContext context, UserManager<User> userManager, SignInManager<User> signManager)
        {
            _context = context;
            _signManager = signManager;
            _userManager = userManager;
        }
        public async Task<User> CreateAsync(User entity)
        {
            User user = new User()
            {
                UserEmail = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Password = entity.Password,
                Role = entity.Role
            };

            _context.Set<User>().Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> CreateFromViewAsync(User entity, Role role)
        {
            User user = new User()
            {
                UserEmail = entity.UserEmail,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(entity.Password),
                Role = role,
                UserName = entity.UserEmail,
                Email = entity.UserEmail
            };     

            _context.Set<User>().Add(user);

            var result  = await _userManager.CreateAsync(user, entity.Password);
            
            await _userManager.AddToRoleAsync(user, role.ToString());

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"No such {typeof(User)} with id: {id}");
            }

            _context.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _context.Set<User>().AsQueryable();
        }

        public IQueryable<User> GetAll(Expression<Func<User, bool>> filter)
        {
            return _context.Set<User>().Where(filter);
        }

        public async ValueTask<User> GetByIdAsync(string id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<User> UpdateAsync(User entity)
        {
            var dbEntity = await GetByIdAsync(entity.Id);

            if (dbEntity == null)
            {
                throw new ArgumentException($"No such {typeof(User)} with id: {entity.Id}");
            }

            _context.Entry(dbEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
