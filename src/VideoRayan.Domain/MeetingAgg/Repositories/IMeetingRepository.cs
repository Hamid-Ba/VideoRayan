using Framework.Domain;
using VideoRayan.Application.Contract.MeetingAgg;

namespace VideoRayan.Domain.MeetingAgg.Repositories
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task<MeetingDto> GetBy(Guid id);
        Task<EditMeetingDto> GetDetailForEditBy(Guid id);
        Task<IEnumerable<MeetingDto>> GetAll(Guid cutomerId);
    }
}