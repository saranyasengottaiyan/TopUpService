using TopUpService.Models;

namespace TopUpService
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool> EditUserAsync(User updatedUser);
        Task<Guid> CreateUserAsync(User newUser);
    }
}