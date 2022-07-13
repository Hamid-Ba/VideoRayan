using System;
using Framework.Domain;
using VideoRayan.Application.Contract.UserAgg;

namespace VideoRayan.Domain.UserAgg.Contracts
{
    public interface IAudienceRepository : IRepository<Audience>
    {
        Task<IEnumerable<AudienceDto>> GetAll(string categoryName);
        Task<AudienceDto> GetBy(Guid id);
        Task<EditAudienceDto> GetDetailForEditBy(Guid id);
    }
}