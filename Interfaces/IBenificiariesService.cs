namespace TopUpService.Interfaces
{
    public interface IBenificiariesService
    {
        Task<IEnumerable<Beneficiary>> GetAllBeneficiariesAsync(Guid userId);
        Task<IEnumerable<Beneficiary>> GetActiveBeneficiariesAsync(Guid userId);
        Task<Beneficiary> GetBeneficiaryByIdAsync(Guid beneficiaryId);
        Task<bool> DeleteBeneficiaryAsync(Guid beneficiaryId);
        Task<bool> EditBeneficiaryAsync(Beneficiary updatedBeneficiary);
        Task<Guid> CreateBeneficiaryAsync(Beneficiary newBeneficiary);
    }
}
