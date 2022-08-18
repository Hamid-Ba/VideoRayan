using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Domain;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Infrastructure.EfCore.Repositories
{
    public class AudienceFaceToFaceRepository : Repository<AudienceFaceToFace>, IAudienceFaceToFaceRepository
    {
        private readonly VideoRayanContext _context;

        public AudienceFaceToFaceRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<AudienceFaceToFace>> GetAllBy(Guid meetingId) => await _context.AudienceFaceToFaces.Where(a => a.FaceToFaceId == meetingId).AsNoTracking().ToListAsync();
    }
}