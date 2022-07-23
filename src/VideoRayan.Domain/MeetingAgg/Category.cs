using Framework.Domain;
using VideoRayan.Domain.CustomerAgg;

namespace VideoRayan.Domain.MeetingAgg
{
    public class Category : EntityBase
	{
        public Guid CustomerId { get;private set; }
        public string Title { get;private set; }
        public string Description { get; private set; }

        public Customer? Customer { get; private set; }

        public Category(Guid customerId,string title, string description)
        {
            CustomerId = customerId;
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