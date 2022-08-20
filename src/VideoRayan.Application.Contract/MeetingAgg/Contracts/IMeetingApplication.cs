using Framework.Application;
using VideoRayan.Application.Contract.CustomerAgg;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
    public interface IMeetingApplication
	{
		Task<MeetingDto> GetBy(Guid id);
		Task<IEnumerable<AudienceDto>> GetAllBy(Guid id);
		Task<EditMeetingDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> SetHost(Guid id,Guid hostId);
		Task<OperationResult> Edit(EditMeetingDto command);
		Task<IEnumerable<MeetingDto>> GetAll(Guid cutomerId);
		Task<OperationResult> Delete(Guid customerId,Guid id);
		Task<OperationResult> Create(CreateMeetingDto command);
		Task<GetAllMeetingDto> GetAllMeetingPaginated(FilterMeeting filter);
	}
}