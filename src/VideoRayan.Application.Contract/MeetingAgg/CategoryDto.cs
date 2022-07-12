using System;
namespace VideoRayan.Application.Contract.MeetingAgg
{
	public class CategoryDto : DtoBase
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
	}

	public class CreateCategoryDto
    {
		public string? Title { get; set; }
		public string? Description { get; set; }
	}

	public class EditCategoryDto : CreateCategoryDto
    {
        public Guid Id { get; set; }
    }
}