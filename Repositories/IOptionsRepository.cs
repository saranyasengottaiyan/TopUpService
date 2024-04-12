namespace TopUpService.Repositories
{
    public interface IOptionsRepository
    {
        Task<IEnumerable<TopUpOption>> GetAllTopupOptionsAsync();
        Task<TopUpOption> GetTopUpOptionByIdAsync(Guid optionId);
        Task<bool> DeleteTopUpOptionAsync(Guid optionId);
        Task<bool> EditTopUpOptionAsync(TopUpOption Option);
        Task<Guid> CreateTopUpOptionAsync(TopUpOption newOption);
    }
}
