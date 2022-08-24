using Framework.Domain;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.MeetingAgg;

namespace VideoRayan.Domain.CustomerAgg.Contracts
{
    public interface IAudienceRepository : IRepository<Audience>
    {
        Task<AudienceDto> GetBy(Guid id);
        Task<EditAudienceDto> GetDetailForEditBy(Guid id);
        Task<IEnumerable<AudienceDto>> GetAllBy(Guid meetingId);
        Task<IEnumerable<AudienceDto>> GetAllByFaceToFace(Guid id);
        Task<IEnumerable<AudienceDto>> GetAll(SearchAudienceDto filter);
        Task<GetAllAudienceDto> GetAllPaginated(SearchAudienceDto filter);
        Task<SendStatusMeetingDto> GetForSendingSms(Guid id, bool isMeeting);
    }
}