namespace TopUpService.Repositories
{
    public interface IBenificiaryRepository
    {
        Task<Guid> CreateBeneficiaryAsync(Beneficiary newBeneficiary);
        Task<bool> EditBeneficiaryAsync(Beneficiary updatedBeneficiary);
        Task<bool> DeleteBeneficiaryAsync(Guid beneficiaryId);
        Task<Beneficiary> GetBeneficiaryByIdAsync(Guid beneficiaryId);
        Task<IEnumerable<Beneficiary>> GetActiveBeneficiariesAsync(Guid userId);
        Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync(Guid userId);
    }
}
