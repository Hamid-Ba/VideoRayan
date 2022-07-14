using Framework.Application;
using VideoRayan.Application.Contract.UserAgg;
using VideoRayan.Application.Contract.UserAgg.Contracts;
using VideoRayan.Domain.UserAgg;
using VideoRayan.Domain.UserAgg.Contracts;

namespace VideoRayan.Application
{
    public class AudienceApplication : IAudienceApplication
    {
        private readonly IAudienceRepository _audienceRepository;

        public AudienceApplication(IAudienceRepository audienceRepository) => _audienceRepository = audienceRepository;

        public async Task<OperationResult> Create(CreateAudienceDto command)
        {
            OperationResult result = new();

            if (_audienceRepository.Exists(a => a.Mobile == command.Mobile && a.UserId == command.UserId))
                return result.Failed(ApplicationMessage.DuplicatedMobile);

            var audience = new Audience(command.UserId, command.CategoryId, command.FullName, command.Mobile, command.Position);
            await _audienceRepository.AddEntityAsync(audience);
            await _audienceRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditAudienceDto command)
        {
            OperationResult result = new();

            var audience = await _audienceRepository.GetEntityByIdAsync(command.Id);

            if (_audienceRepository.Exists(a => a.Mobile == command.Mobile && a.UserId == command.UserId && command.Id != a.Id))
                return result.Failed(ApplicationMessage.DuplicatedMobile);

            audience.Edit(command.CategoryId, command.FullName, command.Mobile, command.Position);
            await _audienceRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<AudienceDto>> GetAll(string categoryName) => await _audienceRepository.GetAll(categoryName);

        public async Task<AudienceDto> GetBy(Guid id) => await _audienceRepository.GetBy(id);

        public async Task<EditAudienceDto> GetDetailForEditBy(Guid id) => await _audienceRepository.GetDetailForEditBy(id);

    }
}