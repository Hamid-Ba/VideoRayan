using Framework.Application.Enums;

namespace VideoRayan.Application.Contract.MeetingAgg
{
    public class MeetingDto : DtoBase
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public bool IsLive { get; set; }
        public bool IsMute { get; set; }
        public bool IsRecord { get; set; }
        public bool CanTalk { get; set; }
        public bool IsInteractiveBoard { get; set; }
        public int AudienceCount { get; set; }
        public MeetingType Type { get; set; }
        public DateTime StartDateTime { get; set; }
        public string? PersianCreationDate { get; set; }
        public string? PersianStartDate { get; set; }
    }

    public class CreateMeetingDto
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public bool IsLive { get; set; }
        public bool IsMute { get; set; }
        public bool IsRecord { get; set; }
        public bool CanTalk { get; set; }
        public bool IsInteractiveBoard { get; set; }
        public MeetingType Type { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
    }

    public class EditMeetingDto : CreateMeetingDto
    {
        public Guid Id { get; set; }
    }
}