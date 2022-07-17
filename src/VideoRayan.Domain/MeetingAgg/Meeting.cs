using Framework.Domain;
using VideoRayan.Domain.CustomerAgg;

namespace VideoRayan.Domain.MeetingAgg
{
    public class Meeting : EntityBase
    {
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public DateTime StartDateTime { get; private set; }

        public Customer? User { get; private set; }
        public List<AudienceMeeting>? Audiences { get;private set; }

        public Meeting(Guid userId,string title, DateTime startDateTime)
        {
            Guard(title, startDateTime);

            UserId = userId;
            Title = title;
            StartDateTime = startDateTime;
        }

        public void Edit(string title, DateTime? startDateTime)
        {
            Title = title;

            if (startDateTime != null)
                StartDateTime = (DateTime)startDateTime;

            LastUpdateDate = DateTime.Now;
        }

        private void Guard(string title, DateTime? startDate)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new InvalidDataException($"{nameof(title)} can't be null or empty");
            if (startDate is null) throw new InvalidDataException($"{nameof(startDate)} can't be null or empty");
        }
    }
}