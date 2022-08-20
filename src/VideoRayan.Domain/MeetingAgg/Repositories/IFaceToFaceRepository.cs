using Framework.Domain;
using VideoRayan.Application.Contract.MeetingAgg;

namespace VideoRayan.Domain.MeetingAgg.Repositories
{
    public interface IFaceToFaceRepository : IRepository<FaceToFace>
    {
        Task<FaceToFaceDto> GetBy(Guid id);
        Task<EditFaceToFaceDto> GetDetailForEditBy(Guid id);
        Task<IEnumerable<FaceToFaceDto>> GetAll(Guid customerId);
        Task<GetAllFaceToFaceDto> GetAllFaceToFacePaginated(FilterFaceToFace filter);
    }
}