﻿using Framework.Application;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
    public interface IMeetingApplication
	{
		Task<MeetingDto> GetBy(Guid id);
		Task<EditMeetingDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditMeetingDto command);
		Task<IEnumerable<MeetingDto>> GetAll(Guid cutomerId);
		Task<OperationResult> Delete(Guid customerId,Guid id);
		Task<OperationResult> Create(CreateMeetingDto command);
	}
}