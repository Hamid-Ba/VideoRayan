using Framework.Application.Enums;
using Framework.Domain;
using VideoRayan.Domain.CustomerAgg;

namespace VideoRayan.Domain.MeetingAgg
{
    public class Meeting : EntityBase
    {
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public bool IsLive { get; private set; }
        public bool IsMute { get; private set; }
        public bool IsRecord { get; private set; }
        public bool CanTalk { get; private set; }
        public bool IsInteractiveBoard { get; private set; }
        public MeetingType Type { get; private set; }
        public DateTime StartDateTime { get; private set; }

        public Customer? User { get; private set; }
        public List<AudienceMeeting>? Audiences { get; private set; }

        public Meeting(Guid userId, string title, bool isLive, bool isMute, bool isRecord, bool canTalk,
            bool isInteractiveBoard, MeetingType type, DateTime startDateTime)
        {
            Guard(title, startDateTime);

            UserId = userId;
            Title = title;
            IsLive = isLive;
            IsMute = isMute;
            IsRecord = isRecord;
            CanTalk = canTalk;
            IsInteractiveBoard = isInteractiveBoard;
            Type = type;
            StartDateTime = startDateTime;
        }

        public void Edit(string title, bool isLive, bool isMute, bool isRecord, bool canTalk,
            bool isInteractiveBoard, MeetingType type, DateTime? startDateTime)
        {
            Guard(title, startDateTime);

            Title = title;
            IsLive = isLive;
            IsMute = isMute;
            IsRecord = isRecord;
            CanTalk = canTalk;
            IsInteractiveBoard = isInteractiveBoard;
            Type = type;

            if (startDateTime != null)
                StartDateTime = (DateTime)startDateTime;
        }

        private void Guard(string title, DateTime? startDate)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new InvalidDataException($"{nameof(title)} can't be null or empty");
            if (startDate is null) throw new InvalidDataException($"{nameof(startDate)} can't be null or empty");
        }
    }
}