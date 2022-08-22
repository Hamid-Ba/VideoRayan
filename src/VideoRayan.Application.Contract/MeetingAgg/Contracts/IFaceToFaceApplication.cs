using Framework.Application;
using VideoRayan.Application.Contract.CustomerAgg;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
    public interface IFaceToFaceApplication
    {
        Task<FaceToFaceDto> GetBy(Guid id);
        Task<IEnumerable<AudienceDto>> GetAllBy(Guid id);
        Task<EditFaceToFaceDto> GetDetailForEditBy(Guid id);
        Task<OperationResult> SetHost(Guid id, Guid hostId);
        Task<(OperationResult,FaceToFaceDto)> Edit(EditFaceToFaceDto command);
        Task<IEnumerable<FaceToFaceDto>> GetAll(Guid customerId);
        Task<(OperationResult, FaceToFaceDto)> Delete(Guid customerId, Guid id);
        Task<(OperationResult, FaceToFaceDto)> Create(CreateFaceToFaceDto command);
        Task<GetAllFaceToFaceDto> GetAllFaceToFacePaginated(FilterFaceToFace filter);
    }
}