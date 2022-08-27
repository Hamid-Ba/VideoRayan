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
		Task<IEnumerable<MeetingDto>> GetAll(Guid cutomerId);
		Task<OperationResult> SendConfirmMeetingSms(Guid id,string template);
		Task<OperationResult> SendDisConfirmMeetingSms(Guid id,string template);
        Task<(OperationResult, MeetingDto)> Edit(EditMeetingDto command);
		Task<(OperationResult, MeetingDto)> Delete(Guid customerId,Guid id);
		Task<(OperationResult,MeetingDto)> Create(CreateMeetingDto command);
		Task<GetAllMeetingDto> GetAllMeetingPaginated(FilterMeeting filter);
	}
}