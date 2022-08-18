using Framework.Domain;

namespace VideoRayan.Domain.MeetingAgg.Repositories
{
    public interface IAudienceFaceToFaceRepository : IRepository<AudienceFaceToFace>
    {
        Task<IEnumerable<AudienceFaceToFace>> GetAllBy(Guid faceToFaceId);
    }
}