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
        Task<OperationResult> Edit(EditFaceToFaceDto command);
        Task<IEnumerable<FaceToFaceDto>> GetAll(Guid customerId);
        Task<OperationResult> Delete(Guid customerId, Guid id);
        Task<OperationResult> Create(CreateFaceToFaceDto command);
    }
}