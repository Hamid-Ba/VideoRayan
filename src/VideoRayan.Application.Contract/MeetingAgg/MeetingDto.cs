namespace VideoRayan.Application.Contract.MeetingAgg
{
    public class MeetingDto : DtoBase
	{
		public Guid UserId { get;  set; }
		public string? Title { get; set; }
		public DateTime StartDateTime { get; set; }
	}

	public class CreateMeetingDto
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
    }

    public class EditMeetingDto : CreateMeetingDto
    {
        public Guid Id { get; set; }
    }
}