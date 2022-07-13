using System;
namespace VideoRayan.Application.Contract.UserAgg
{
    public class AudienceDto : DtoBase
    {
        public Guid CategoryId { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? Position { get; set; }
    }

    public class CreateAudienceDto
    {
        public Guid CategoryId { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? Position { get; set; }
    }

    public class EditAudienceDto : CreateAudienceDto
    {
        public Guid Id { get; set; }
    }
}