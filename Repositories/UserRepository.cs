using Microsoft.EntityFrameworkCore;
using TopUpService.Models;
using TopUpService.Providers;

namespace TopUpService.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly TopUpDbContext dbContext;

        public UserRepository(TopUpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateUserAsync(User newUser)
        {
            newUser.Created = DateTime.Now;
            newUser.Updated = DateTime.Now;

            dbContext.User.Add(newUser);
            await dbContext.SaveChangesAsync();

            return newUser.UserId;
        }

        public async Task<bool> EditUserAsync(User updatedUser)
        {
            var existingUser = await dbContext.User.FindAsync(updatedUser.UserId);
            if (existingUser == null)
                return false; // User not found

            existingUser.UserName = updatedUser.UserName;
            existingUser.Address = updatedUser.Address;
            existingUser.DOB = updatedUser.DOB;
            existingUser.Updated = DateTime.Now; // Update the timestamp

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid UserId)
        {
            var UserToDelete = await dbContext.User.FindAsync(UserId);
            if (UserToDelete == null)
                return false; // User not found

            dbContext.User.Remove(UserToDelete);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserByIdAsync(Guid UserId)
        {
            return await dbContext.User.FindAsync(UserId) ?? new User();
        }
    }
}
