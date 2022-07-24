using System;
using Framework.Domain;
using VideoRayan.Application.Contract.CustomerAgg;

namespace VideoRayan.Domain.CustomerAgg.Contracts
{
    public interface IAudienceRepository : IRepository<Audience>
    {
        Task<AudienceDto> GetBy(Guid id);
        Task<EditAudienceDto> GetDetailForEditBy(Guid id);
        Task<IEnumerable<AudienceDto>> GetAll(Guid customerId, string categoryName);
    }
}