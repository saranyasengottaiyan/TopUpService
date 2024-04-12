using TopUpService.Interfaces;
using TopUpService.Models;
using TopUpService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TopUpService.Services
{

    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IBenificiaryRepository beneficiaryRepository;
        private readonly IOptionsRepository optionsRepository;


        public TransactionService(ITransactionRepository transactionRepository, IOptionsRepository optionsRepository, IBenificiaryRepository beneficiaryRepository)
        {
            this.transactionRepository = transactionRepository;
            this.beneficiaryRepository = beneficiaryRepository;
            this.optionsRepository = optionsRepository;
        }

        public async Task<IEnumerable<UserTransaction>> GetAllTransactionAsync(Guid userId)
        {
            var beneficiaries = await beneficiaryRepository.GetAllBeneficiariesAsync(userId);
            return await transactionRepository.GetAllTransactionAsync(beneficiaries.ToList().Select(s => s.BenificiaryId).ToList());
        }

        public async Task<UserTransaction> GetTransactionByIdAsync(Guid TransactionId)
        {
            return await transactionRepository.GetTransactionByIdAsync(TransactionId);
        }

        public async Task<bool> DeleteTransactionAsync(Guid TransactionId)
        {
            return await transactionRepository.DeleteTransactionAsync(TransactionId);
        }

        public async Task<Guid> TopUpTransaction(Guid userId, UserTransaction newTransaction)
        {
            // call the external service to get the user balance
            // int balance = userBalanceService.GetBalance(userId)
            int balance = 3000;

            if(balance < newTransaction.TopUpOption?.Amount)
            {
                throw new Exception("Not having enough credit balance for top up");
            }

            // external service call to confirm its verified user or not
            // bool verifiedUser = securityService.verifyUser(userId);
            // need to add a method to query the current month transactions
            // here considered the get all transactions gives the current month transactions

            bool verifiedUser = true;
            var transactions = await GetAllTransactionAsync(userId);

            var beneficiariestotalAmount = transactions
                .GroupBy(s => s.BeneficiaryId)
                .Select(g => new
                {
                    benefiaciary = g.Key,
                    topUpAmount = g.Sum(i => i.TopUpOption?.Amount)
                }).ToList();
            var totalAmount = beneficiariestotalAmount
                .Where(s => s.benefiaciary == newTransaction.BeneficiaryId)
                .Select(s => s.topUpAmount).FirstOrDefault();

            if ((verifiedUser && totalAmount >= 500) || (!verifiedUser && totalAmount >= 3000))
            {
                    throw new Exception("User crossed the monthly limit to top up");
            }

            // call the external service to debit the balance
            // debit balance external call should be success to update the transaction status
            // bool debitStatus = await userBalanceService.debitBalance(

            int? debitAmount = newTransaction.TopUpOption?.Amount + 1;
            // bool debitStatus = await userBalanceService.debitBalance(userId, debitAmount)
            bool debitStatus = true;

            if(!debitStatus)
            {
                throw new Exception("Not able to complete the transaction. Please try again later");
            }

            return await transactionRepository.TopUpTransaction(userId, newTransaction);
        }
    }
}
