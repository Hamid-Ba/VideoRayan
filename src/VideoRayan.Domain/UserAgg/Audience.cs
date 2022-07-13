using System;
using Framework.Domain;
using VideoRayan.Domain.MeetingAgg;

namespace VideoRayan.Domain.UserAgg
{
	public class Audience : EntityBase
	{
        public Guid CategoryId { get;private set; }
        public string? FullName { get;private set; }
        public string? Mobile { get;private set; }
        public string? Position { get;private set; }

        public Category? Category { get;private set; }

        public Audience(Guid categoryId, string? fullName, string? mobile, string? position)
        {
            CategoryId = categoryId;
            FullName = fullName;
            Mobile = mobile;
            Position = position;
        }

        public void Edit(Guid categoryId, string? fullName, string? mobile, string? position)
        {
            CategoryId = categoryId;
            FullName = fullName;
            Mobile = mobile;
            Position = position;
        }
    }
}