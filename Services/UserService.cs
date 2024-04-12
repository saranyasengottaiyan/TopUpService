using TopUpService.Interfaces;
using TopUpService.Models;
using TopUpService.Repositories;

namespace TopUpService.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await userRepository.GetUserByIdAsync(userId);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            return await userRepository.DeleteUserAsync(userId);
        }

        public async Task<bool> EditUserAsync(User updatedUser)
        {
            return await userRepository.EditUserAsync(updatedUser);
        }

        public async Task<Guid> CreateUserAsync(User newUser)
        {
            return await userRepository.CreateUserAsync(newUser);
        }
    }
}
