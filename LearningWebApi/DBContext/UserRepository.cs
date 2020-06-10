using LearningModels;
using LearningWebApi.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningWebApi.DBContext
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUserByID(int userId);
        Task AddUserRefreshToken(User user, UserRefreshToken refreshToken);

        Task<User> ValidateUserFromEmailAndPasswords(string email, string password);
    }
    public class UserRepository : IUserRepository
    {


        public readonly BlazorDBContext dbContext;
        public UserRepository(BlazorDBContext _context)
        {
            dbContext = _context;
        }

        public async Task<User> AddUser(User user)
        {
            //var result = await dbContext.AddAsync(user);
            //await dbContext.SaveChangesAsync();
            //return result.Entity;

            var result = await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddUserRefreshToken(User user, UserRefreshToken refreshToken)
        {
            user.UserRefreshTokens.Add(refreshToken);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByID(int userId)
        {
            return await dbContext.Users.Include(u => u.Role)
                                        .Where(u => u.UserId == userId).FirstOrDefaultAsync();

        }

        public async Task<User> ValidateUserFromEmailAndPasswords(string email, string password)
        {
            return await dbContext.Users.Include(u => u.Role)
                                        .Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();

        }
    }
}
