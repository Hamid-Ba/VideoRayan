using Framework.Application.Enums;

namespace VideoRayan.Application.Contract.CustomerAgg
{
    public class AudienceDto : DtoBase
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string? CreatorName { get; set; }
        public string? CategoryTitle { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? Position { get; set; }
        public string? PersianCreationDate { get; set; }
    }

    public class CreateAudienceDto
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? Position { get; set; }
    }

    public class EditAudienceDto : CreateAudienceDto
    {
        public Guid Id { get; set; }
    }

    public class SearchAudienceDto
    {
        public Guid CustomerId { get; set; }
        public string? Category { get; set; }
    }
}