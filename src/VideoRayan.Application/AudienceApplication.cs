using System;
using Framework.Application;
using VideoRayan.Application.Contract.UserAgg;
using VideoRayan.Application.Contract.UserAgg.Contracts;
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

            //if(_audienceRepository.Exists(a => a.ph))
            return result.Succeeded();
        }

        public Task<OperationResult> Edit(EditAudienceDto command)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AudienceDto>> GetAll(string categoryName) => await _audienceRepository.GetAll(categoryName);

        public async Task<AudienceDto> GetBy(Guid id) => await _audienceRepository.GetBy(id);

        public async Task<EditAudienceDto> GetDetailForEditBy(Guid id) => await _audienceRepository.GetDetailForEditBy(id);

    }
}