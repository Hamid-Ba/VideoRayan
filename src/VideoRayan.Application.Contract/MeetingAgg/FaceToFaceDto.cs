using Framework.Application.Enums;

namespace VideoRayan.Application.Contract.MeetingAgg
{
    public class FaceToFaceDto : DtoBase
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public MeetingType Type { get; set; }
        public MeetingStatus Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public string? PersianCreationDate { get; set; }
        public string? PersianStartDate { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
        public int AudienceCount { get; set; }
    }
    public class CreateFaceToFaceDto
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public MeetingType Type { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
    }

    public class EditFaceToFaceDto : CreateMeetingDto
    {
        public Guid Id { get; set; }
    }
}