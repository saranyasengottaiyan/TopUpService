using TopUpService.Interfaces;
using TopUpService.Repositories;

namespace TopUpService.Services
{

    public class BeneficiariesService : IBenificiariesService
    {
        private readonly IBenificiaryRepository benificiaryRepository;

        public BeneficiariesService(IBenificiaryRepository benificiaryRepository)
        {
            this.benificiaryRepository = benificiaryRepository;
        }

        public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync(Guid userId)
        {
            return await benificiaryRepository.GetAllBeneficiariesAsync(userId);
        }

        public async Task<IEnumerable<Beneficiary>> GetActiveBeneficiariesAsync(Guid userId)
        {
            return await benificiaryRepository.GetActiveBeneficiariesAsync(userId);
        }

        public async Task<Beneficiary> GetBeneficiaryByIdAsync(Guid beneficiaryId)
        {
            return await benificiaryRepository.GetBeneficiaryByIdAsync(beneficiaryId);
        }

        public async Task<bool> DeleteBeneficiaryAsync(Guid beneficiaryId)
        {
            return await benificiaryRepository.DeleteBeneficiaryAsync(beneficiaryId);
        }

        public async Task<bool> EditBeneficiaryAsync(Beneficiary updatedBeneficiary)
        {
            return await benificiaryRepository.EditBeneficiaryAsync(updatedBeneficiary);
        }

        public async Task<Guid> CreateBeneficiaryAsync(Beneficiary newBeneficiary)
        {
            var beneficiaries = await benificiaryRepository.GetActiveBeneficiariesAsync(newBeneficiary.UserId);
            if(beneficiaries.ToList().Count >= 5)
            {
                throw new Exception("User already has 5 active beneficiaries");
            }
            return await benificiaryRepository.CreateBeneficiaryAsync(newBeneficiary);
        }
    }
}
