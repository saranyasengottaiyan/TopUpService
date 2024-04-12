using TopUpService.Interfaces;
using TopUpService.Repositories;

namespace TopUpService.Services
{
    public class OptionsService: IOptionsService
    {
        private readonly IOptionsRepository optionsRepository;

        public OptionsService(IOptionsRepository optionsRepository)
        {
            this.optionsRepository = optionsRepository;
        }

        public async Task<IEnumerable<TopUpOption>> GetAllTopupOptionsAsync()
        {
            return await optionsRepository.GetAllTopupOptionsAsync();
        }

        public async Task<TopUpOption> GetTopUpOptionByIdAsync(Guid optionId)
        {
            return await optionsRepository.GetTopUpOptionByIdAsync(optionId);
        }

        public async Task<bool> DeleteTopUpOptionAsync(Guid optionId)
        {
            return await optionsRepository.DeleteTopUpOptionAsync(optionId);
        }

        public async Task<bool> EditTopUpOptionAsync(TopUpOption option)
        {
            return await optionsRepository.EditTopUpOptionAsync(option);
        }

        public async Task<Guid> CreateTopUpOptionAsync(TopUpOption newOption)
        {
            return await optionsRepository.CreateTopUpOptionAsync(newOption);
        }

    }
}
