using Framework.Domain;

namespace VideoRayan.Domain.PlanAgg
{
    public class Plan : EntityBase
    {
        public string Title { get; private set; }
        public int PeriodPerDay { get; private set; }
        public string ImageName { get; private set; }
        public double Cost { get; private set; }
        public string Description { get; private set; }
        public string Ps { get; private set; }
        public long OrderCount { get; private set; }

        public List<Order> Orders { get; private set; }

        public Plan(string title, int periodPerDay, string imageName, double cost, string description, string ps)
        {
            Title = title;
            PeriodPerDay = periodPerDay;
            ImageName = imageName;
            Cost = cost;
            Description = description;
            Ps = ps;
            OrderCount = 0;
        }

        public void Edit(string title, int periodPerDay, string imageName, double cost, string description, string ps)
        {
            Title = title;
            PeriodPerDay = periodPerDay;

            if (!string.IsNullOrWhiteSpace(imageName))
                ImageName = imageName;

            Cost = cost;
            Description = description;
            Ps = ps;

            LastUpdateDate = DateTime.Now;
        }

        public long AddOrder() => ++OrderCount;

    }
}