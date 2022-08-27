using Framework.Application.Enums;
using Framework.Domain;
using VideoRayan.Domain.CustomerAgg;

namespace VideoRayan.Domain.MeetingAgg
{
    public class FaceToFace : EntityBase
    {
        public Guid UserId { get; private set; }
        public Guid HostId { get; private set; }
        public string Title { get; private set; }
        public string Address { get; private set; }
        public string Description { get; private set; }
        public MeetingType Type { get; private set; }
        public DateTime StartDateTime { get; private set; }

        public Customer? User { get; private set; }
        public List<AudienceFaceToFace>? Audiences { get; private set; }

        public FaceToFace(Guid userId, string title, string address,string description, MeetingType type, DateTime startDateTime)
        {
            Guard(title, startDateTime);
            UserId = userId;
            Title = title;
            Address = address;
            Description = description;
            Type = type;
            StartDateTime = startDateTime;
        }

        public void Edit(string title, string address,string description, MeetingType type, DateTime startDateTime)
        {
            Title = title;
            Address = address;
            Description = description;
            Type = type;
            StartDateTime = startDateTime;
        }

        public void SetHost(Guid hostId) => HostId = HostId;

        private void Guard(string title, DateTime? startDate)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new InvalidDataException($"{nameof(title)} can't be null or empty");
            if (startDate is null) throw new InvalidDataException($"{nameof(startDate)} can't be null or empty");
        }
    }
}