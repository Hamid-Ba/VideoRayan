namespace VideoRayan.Application.Contract.AccountAgg
{
    public class PermissionVM
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public long? ParentId { get; set; }
    }
}