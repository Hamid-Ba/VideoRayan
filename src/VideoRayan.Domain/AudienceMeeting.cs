using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.UserAgg;

namespace VideoRayan.Domain
{
    public class AudienceMeeting
	{
        public Guid MeetingId { get;private set; }
        public Guid AudienceId { get; private set; }

        public Meeting? Meeting { get;private set; }
        public Audience? Audience { get;private set; }

        public AudienceMeeting(Guid meetingId, Guid audienceId)
        {
            MeetingId = meetingId;
            AudienceId = audienceId;
        }
    }
}