using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Domain;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Infrastructure.EfCore.Repositories
{
    public class AudienceMeetingRepository : Repository<AudienceMeeting>, IAudienceMeetingRepository
    {
        private readonly VideoRayanContext _context;

        public AudienceMeetingRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<AudienceMeeting>> GetAllBy(Guid meetingId) => await _context.AudienceMeetings.Where(a => a.MeetingId == meetingId).AsNoTracking().ToListAsync();

    }
}