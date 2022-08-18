using Framework.Application;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
    public interface IAudienceFaceToFaceApplication
    {
        Task<OperationResult> AddAudiencesToMeeting(AudienceFaceToFaceDto command);
    }
}