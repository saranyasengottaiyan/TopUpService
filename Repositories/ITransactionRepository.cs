using TopUpService.Models;

namespace TopUpService
{
    public interface ITransactionRepository
    {
        Task<Guid> TopUpTransaction(Guid userId, UserTransaction userTransaction);
        Task<IEnumerable<UserTransaction>> GetAllTransactionAsync(List<Guid> beneficiaryIds);
        Task<UserTransaction> GetTransactionByIdAsync(Guid transactionId);
        Task<bool> DeleteTransactionAsync(Guid transactionId);
    }
}