using Framework.Application;
using Framework.Application.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

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

    public class SearchAudienceDto : BaseFilterParam
    {
        public Guid CustomerId { get; set; }
        public string? Category { get; set; }
    }

    public class GetAllAudienceDto : BaseFilter<AudienceDto, SearchAudienceDto>
    {

    } 
}