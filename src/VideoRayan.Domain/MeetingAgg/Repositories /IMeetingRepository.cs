using Framework.Domain;
using VideoRayan.Application.Contract.MeetingAgg;

namespace VideoRayan.Domain.MeetingAgg.Repositories
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        Task<IEnumerable<MeetingDto>> GetAll();
        Task<MeetingDto> GetBy(Guid id);
        Task<EditMeetingDto> GetDetailForEditBy(Guid id);
    }
}