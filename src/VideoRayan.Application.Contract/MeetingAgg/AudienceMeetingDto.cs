namespace VideoRayan.Application.Contract.MeetingAgg
{
    public class AudienceMeetingDto
    {
        public Guid MeetingId { get; set; }
        public Guid HostId { get; set; }
        public List<Guid>? AudiencesId { get; set; }
    }
}