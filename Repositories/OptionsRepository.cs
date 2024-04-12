using Microsoft.EntityFrameworkCore;
using TopUpService.Providers;

namespace TopUpService.Repositories
{
    public class OptionsRepository: IOptionsRepository
    {
        private readonly TopUpDbContext dbContext;

        public OptionsRepository(TopUpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<TopUpOption>> GetAllTopupOptionsAsync()
        {
            return await dbContext.TopUpOption.ToListAsync();
        }

        public async Task<TopUpOption> GetTopUpOptionByIdAsync(Guid optionId)
        {
            return await dbContext.TopUpOption.FirstOrDefaultAsync(o => o.OptionId == optionId) ?? new TopUpOption();
        }

        public async Task<bool> DeleteTopUpOptionAsync(Guid optionId)
        {
            var optionToDelete = await dbContext.TopUpOption.FindAsync(optionId);
            if (optionToDelete != null)
            {
                dbContext.TopUpOption.Remove(optionToDelete);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditTopUpOptionAsync(TopUpOption option)
        {
            var existingOption = await dbContext.TopUpOption.FindAsync(option.OptionId);
            if (existingOption != null)
            {
                existingOption.OptionName = option.OptionName;
                existingOption.Amount = option.Amount;
                existingOption.Updated = DateTime.Now;
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Guid> CreateTopUpOptionAsync(TopUpOption newOption)
        {
            newOption.Created = DateTime.Now;
            newOption.Updated = DateTime.Now;
            dbContext.TopUpOption.Add(newOption);
            await dbContext.SaveChangesAsync();
            return newOption.OptionId;
        }
    }
}
