using Domain.Entities;

namespace RideShare.Services
{
    public interface IUsersService
    {
        Task<User> GetUserById(string userId);
    }
}