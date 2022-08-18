using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.MeetingAgg;

namespace VideoRayan.Domain
{
    public class AudienceFaceToFace
    {
        public Guid AudienceId { get; private set; }
        public Guid FaceToFaceId { get; private set; }

        public Audience? Audience { get; set; }
        public FaceToFace? FaceToFace { get; private set; }

        public AudienceFaceToFace(Guid faceToFaceId, Guid audienceId)
        {
            FaceToFaceId = faceToFaceId;
            AudienceId = audienceId;
        }
    }
}