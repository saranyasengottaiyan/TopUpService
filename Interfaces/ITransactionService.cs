using TopUpService.Models;

namespace TopUpService
{
    public interface ITransactionService
    {
        Task<Guid> TopUpTransaction(Guid userId, UserTransaction userTransaction);
        Task<IEnumerable<UserTransaction>> GetAllTransactionAsync(Guid userId);
        Task<UserTransaction> GetTransactionByIdAsync(Guid transactionId);
        Task<bool> DeleteTransactionAsync(Guid transactionId);
    }
}