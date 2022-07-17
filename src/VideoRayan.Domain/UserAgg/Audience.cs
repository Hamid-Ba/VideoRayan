using Framework.Application.Enums;
using Framework.Domain;
using VideoRayan.Domain.MeetingAgg;

namespace VideoRayan.Domain.UserAgg
{
    public class Audience : EntityBase
    {
        public Guid UserId { get; private set; }
        public Guid CategoryId { get; private set; }
        public string? FullName { get; private set; }
        public string? Mobile { get; private set; }
        public string? Position { get; private set; }
        public AudienceType Type { get; private set; }

        public User? User { get; private set; }
        public Category? Category { get; private set; }
        public List<AudienceMeeting>? Meetings { get; private set; }

        public Audience(Guid userId, Guid categoryId, string? fullName, string? mobile, string? position,AudienceType type = 0)
        {
            UserId = userId;
            CategoryId = categoryId;
            FullName = fullName;
            Mobile = mobile;
            Position = position;
            Type = type;
        }

        public void Edit(Guid categoryId, string? fullName, string? mobile, string? position,AudienceType type)
        {
            CategoryId = categoryId;
            FullName = fullName;
            Mobile = mobile;
            Position = position;
            Type = type;

            LastUpdateDate = DateTime.Now;
        }
    }
}