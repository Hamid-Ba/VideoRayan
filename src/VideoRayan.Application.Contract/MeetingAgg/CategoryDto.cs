namespace VideoRayan.Application.Contract.MeetingAgg
{
    public class CategoryDto : DtoBase
	{
        public Guid CustomerId { get; set; }
        public string? Title { get; set; }
		public string? Description { get; set; }
        public string? PersianCreationDate { get; set; }
    }

	public class CreateCategoryDto
    {
        public Guid CustomerId { get; set; }
        public string? Title { get; set; }
		public string? Description { get; set; }
	}

	public class EditCategoryDto : CreateCategoryDto
    {
        public Guid Id { get; set; }
    }
}