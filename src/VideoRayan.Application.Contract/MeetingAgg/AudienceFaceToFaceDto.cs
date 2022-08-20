namespace VideoRayan.Application.Contract.MeetingAgg
{
    public class AudienceFaceToFaceDto : DtoBase
    {
        public Guid FaceToFaceId { get; set; }
        public Guid HostId { get; set; }
        public List<Guid>? AudiencesId { get; set; }
    }
}