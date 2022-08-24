using Framework.Application;
using VideoRayan.Application.Contract.CustomerAgg;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
    public interface IFaceToFaceApplication
    {
        Task<FaceToFaceDto> GetBy(Guid id);
        Task<IEnumerable<AudienceDto>> GetAllBy(Guid id);
        Task<OperationResult> SetHost(Guid id, Guid hostId);
        Task<EditFaceToFaceDto> GetDetailForEditBy(Guid id);
        Task<IEnumerable<FaceToFaceDto>> GetAll(Guid customerId);
        Task<OperationResult> SendMeetingSms(Guid id, string template);
        Task<(OperationResult,FaceToFaceDto)> Edit(EditFaceToFaceDto command);
        Task<(OperationResult, FaceToFaceDto)> Delete(Guid customerId, Guid id);
        Task<(OperationResult, FaceToFaceDto)> Create(CreateFaceToFaceDto command);
        Task<GetAllFaceToFaceDto> GetAllFaceToFacePaginated(FilterFaceToFace filter);
    }
}