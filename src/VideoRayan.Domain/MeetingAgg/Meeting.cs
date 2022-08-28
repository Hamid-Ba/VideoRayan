using Framework.Application.Enums;
using Framework.Domain;
using VideoRayan.Domain.CustomerAgg;

namespace VideoRayan.Domain.MeetingAgg
{
    public class Meeting : EntityBase
    {
        public Guid UserId { get; private set; }
        public Guid HostId { get; private set; }
        public string Title { get; private set; }
        public bool IsLive { get; private set; }
        public bool IsMute { get; private set; }
        public bool IsRecord { get; private set; }
        public bool CanTalk { get; private set; }
        public int Duration { get; private set; }
        public string? UserPinCode { get; private set; }
        public string? MasterPinCode { get; private set; }
        public bool IsInteractiveBoard { get; private set; }
        public string? Description { get; private set; }
        public MeetingType Type { get; private set; }
        public DateTime StartDateTime { get; private set; }

        public Customer? User { get; private set; }
        public List<AudienceMeeting>? Audiences { get; private set; }

        public Meeting(Guid userId, string title, bool isLive, bool isMute, bool isRecord, bool canTalk, int duration,string userPinCode,string masterPinCode,
            bool isInteractiveBoard, string description, MeetingType type, DateTime startDateTime)
        {
            Guard(title, startDateTime);

            UserId = userId;
            Title = title;
            IsLive = isLive;
            IsMute = isMute;
            IsRecord = isRecord;
            CanTalk = canTalk;
            UserPinCode = userPinCode;
            MasterPinCode = masterPinCode;
            Duration = duration;
            IsInteractiveBoard = isInteractiveBoard;
            Description = description;
            Type = type;
            StartDateTime = startDateTime;
        }

        public void Edit(string title, bool isLive, bool isMute, bool isRecord, bool canTalk, int duration,
            bool isInteractiveBoard, string description, MeetingType type, DateTime? startDateTime)
        {
            Guard(title, startDateTime);

            Title = title;
            IsLive = isLive;
            IsMute = isMute;
            IsRecord = isRecord;
            CanTalk = canTalk;
            Duration = duration;
            IsInteractiveBoard = isInteractiveBoard;
            Description = description;
            Type = type;

            if (startDateTime != null)
                StartDateTime = (DateTime)startDateTime;
        }

        public void SetHost(Guid hostId) => HostId = hostId;

        private void Guard(string title, DateTime? startDate)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new InvalidDataException($"{nameof(title)} can't be null or empty");
            if (startDate is null) throw new InvalidDataException($"{nameof(startDate)} can't be null or empty");
        }
    }
}