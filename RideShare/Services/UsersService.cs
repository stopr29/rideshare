using DataAccess;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RideShare.Services
{
    public class UsersService : IUsersService
    {
        private readonly SQLDbContext dbContext;

        public UsersService(SQLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetUserById(string userId)
        {
            User user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            return user;
        }
    }
}