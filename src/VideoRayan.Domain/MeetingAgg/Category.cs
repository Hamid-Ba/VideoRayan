using System;
using Framework.Domain;

namespace VideoRayan.Domain.MeetingAgg
{
	public class Category : EntityBase
	{
        public string Title { get;private set; }
        public string Description { get; private set; }

        public Category(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public void Edit(string title, string description)
        {
            Title = title;
            Description = description;

            LastUpdateDate = DateTime.Now;
        }
    }
}