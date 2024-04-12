using Microsoft.EntityFrameworkCore;
using TopUpService.Providers;

namespace TopUpService.Repositories
{
    public class BeneficiaryRepository: IBenificiaryRepository
    {
        private readonly TopUpDbContext dbContext;

        public BeneficiaryRepository(TopUpDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateBeneficiaryAsync(Beneficiary newBeneficiary)
        {
            newBeneficiary.Created = DateTime.Now;
            newBeneficiary.Updated = DateTime.Now;
            newBeneficiary.Status = Status.Active;

            dbContext.Beneficiary.Add(newBeneficiary);
            await dbContext.SaveChangesAsync();

            return newBeneficiary.BenificiaryId;
        }

        public async Task<bool> EditBeneficiaryAsync(Beneficiary updatedBeneficiary)
        {
            var existingBeneficiary = await dbContext.Beneficiary.FindAsync(updatedBeneficiary.BenificiaryId);
            if (existingBeneficiary == null)
                return false; // Beneficiary not found

            existingBeneficiary.BenificiaryName = updatedBeneficiary.BenificiaryName;
            existingBeneficiary.NickName = updatedBeneficiary.NickName;
            existingBeneficiary.PhoneNumber = updatedBeneficiary.PhoneNumber;
            existingBeneficiary.Updated = DateTime.Now; // Update the timestamp

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBeneficiaryAsync(Guid beneficiaryId)
        {
            var beneficiaryToDelete = await dbContext.Beneficiary.FindAsync(beneficiaryId);
            if (beneficiaryToDelete == null)
                return false; // Beneficiary not found

            dbContext.Beneficiary.Remove(beneficiaryToDelete);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Beneficiary> GetBeneficiaryByIdAsync(Guid beneficiaryId)
        {
            return await dbContext.Beneficiary.FindAsync(beneficiaryId) ?? new Beneficiary();
        }

        public async Task<IEnumerable<Beneficiary>> GetActiveBeneficiariesAsync(Guid userId)
        {
            return await dbContext.Beneficiary
                .Where(b => b.UserId == userId && b.Status == Status.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync(Guid userId)
        {
            return await dbContext.Beneficiary
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}
