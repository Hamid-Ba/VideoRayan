using System;
using Framework.Application;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
	public interface IAudienceMeetingApplication
	{
		Task<OperationResult> AddAudiencesToMeeting(AudienceMeetingDto command);
	}
}