using Microsoft.EntityFrameworkCore;
using TopUpService.Models;
using TopUpService.Providers;

namespace TopUpService.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TopUpDbContext dbContext;

        public TransactionRepository(TopUpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> TopUpTransaction(Guid userId, UserTransaction userTransaction)
        {
            await dbContext.SaveChangesAsync();
            return userTransaction.TransactionId;
        }

        public async Task<IEnumerable<UserTransaction>> GetAllTransactionAsync(List<Guid> beneficiaryIds)
        {

            return await dbContext.UserTransaction.Where(t => beneficiaryIds.Contains(t.BeneficiaryId)).ToListAsync();
        }

        public async Task<UserTransaction> GetTransactionByIdAsync(Guid transactionId)
        {
            return await dbContext.UserTransaction.FindAsync(transactionId) ?? new UserTransaction();
        }

        public async Task<bool> DeleteTransactionAsync(Guid transactionId)
        {
            var existingTransaction = await dbContext.UserTransaction.FindAsync(transactionId);
            if (existingTransaction != null)
            {
                dbContext.UserTransaction.Remove(existingTransaction);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
