using Framework.Application;
using Framework.Application.Enums;

namespace VideoRayan.Application.Contract.MeetingAgg
{
    public class FaceToFaceDto : DtoBase
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public MeetingType Type { get; set; }
        public MeetingStatus Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public string? PersianCreationDate { get; set; }
        public string? PersianStartDate { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
        public int AudienceCount { get; set; }
    }
    public class CreateFaceToFaceDto
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public MeetingType Type { get; set; }
        public string? StartDate { get; set; }
        public string? StartTime { get; set; }
    }

    public class EditFaceToFaceDto : CreateFaceToFaceDto
    {
        public Guid Id { get; set; }
    }

    public class FilterFaceToFace : BaseFilterParam
    {
        public Guid CustomerId { get; set; }
    }

    public class GetAllFaceToFaceDto : BaseFilter<FaceToFaceDto, FilterFaceToFace> { }
}