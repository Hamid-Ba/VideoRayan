using Framework.Application;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
    public interface IMeetingApplication
	{
		Task<MeetingDto> GetBy(Guid id);
		Task<OperationResult> Delete(Guid id);
		Task<IEnumerable<MeetingDto>> GetAll();
		Task<EditMeetingDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditMeetingDto command);
		Task<OperationResult> Create(CreateMeetingDto command);
	}
}