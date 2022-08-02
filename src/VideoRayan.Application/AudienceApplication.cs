using Framework.Application;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.CustomerAgg.Contracts;

namespace VideoRayan.Application
{
    public class AudienceApplication : IAudienceApplication
    {
        private readonly IAudienceRepository _audienceRepository;

        public AudienceApplication(IAudienceRepository audienceRepository) => _audienceRepository = audienceRepository;

        public async Task<(OperationResult, AudienceDto)> Create(CreateAudienceDto command)
        {
            OperationResult result = new();

            if (_audienceRepository.Exists(a => a.Mobile == command.Mobile && a.UserId == command.UserId))
                return (result.Failed(ApplicationMessage.DuplicatedMobile), default)!;

            var audience = new Audience(command.UserId, command.CategoryId, command.FullName, command.Mobile, command.Position);
            await _audienceRepository.AddEntityAsync(audience);
            await _audienceRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(audience.Id));
        }

        public async Task<(OperationResult, AudienceDto)> Delete(Guid id, Guid customerId)
        {
            OperationResult result = new();

            var customer = await _audienceRepository.GetEntityByIdAsync(id);

            var returnObj = await GetBy(id);

            if (customer is null) return (result.Failed(ApplicationMessage.NotExist), default)!;
            if (customer.UserId != customerId) return (result.Failed(ApplicationMessage.DoNotAccessToOtherAccount), default)!;

            customer.Delete();
            await _audienceRepository.SaveChangesAsync();

            return (result.Succeeded(), returnObj);
        }

        public async Task<(OperationResult, AudienceDto)> Edit(EditAudienceDto command)
        {
            OperationResult result = new();

            var audience = await _audienceRepository.GetEntityByIdAsync(command.Id);

            if (_audienceRepository.Exists(a => a.Mobile == command.Mobile && a.UserId == command.UserId && command.Id != a.Id))
                return (result.Failed(ApplicationMessage.DuplicatedMobile), default)!;

            if (audience.UserId != command.UserId) return (result.Failed(ApplicationMessage.DoNotAccessToOtherAccount), default)!;

            audience.Edit(command.CategoryId, command.FullName, command.Mobile, command.Position);
            await _audienceRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(command.Id));
        }

        public async Task<IEnumerable<AudienceDto>> GetAll(Guid customerId, string categoryName) => await _audienceRepository.GetAll(customerId, categoryName);

        public async Task<AudienceDto> GetBy(Guid id) => await _audienceRepository.GetBy(id);

        public async Task<EditAudienceDto> GetDetailForEditBy(Guid id) => await _audienceRepository.GetDetailForEditBy(id);

    }
}