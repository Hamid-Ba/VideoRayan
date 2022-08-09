//using Framework.Domain;
//using VideoRayan.Domain.CustomerAgg;

//namespace VideoRayan.Domain.PlanAgg
//{
//    public class Order : EntityBase
//    {
//        public Guid CustomerId { get; private set; }
//        public Guid PlanId { get; private set; }
//        public double PayAmount { get; private set; }
//        public string MobileNumber { get; set; }
//        public long RefId { get; private set; }
//        public bool IsPayed { get; private set; }

//        public Plan Plan { get; private set; }
//        public Customer Customer { get; private set; }

//        public Order(Guid customerId, Guid planId, double payAmount, string mobileNumber)
//        {
//            CustomerId = customerId;
//            PlanId = planId;
//            PayAmount = payAmount;
//            MobileNumber = mobileNumber;
//            IsPayed = false;
//        }

//        public void PaymentSucceeded(long refId)
//        {
//            if (refId > 0) RefId = refId;
//            IsPayed = true;
//        }
//    }
//}