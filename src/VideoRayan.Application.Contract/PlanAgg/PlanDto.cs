using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VideoRayan.Application.Contract.PlanAgg
{
    public class PlanVM : DtoBase
    {
        public string? Title { get; set; }
        public int PeriodPerDay { get; set; }
        public string? ImageName { get; set; }
        public double Cost { get; set; }
        public string? Description { get; set; }
        public string? Ps { get; set; }
        public long OrderCount { get; set; }
    }

    public class CreatePlanVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Title { get; set; }

        [Display(Name = "مدت بر حسب روز")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, 1825, ErrorMessage = "حداقل مقدار {1} و حداکثر مقدار {2} می باشد")]
        public int PeriodPerDay { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, double.MaxValue, ErrorMessage = "حداقل مقدار {1} و حداکثر مقدار {2} می باشد")]
        public double Cost { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Description { get; set; }

        [Display(Name = "پی نوشت")]
        public string? Ps { get; set; }
    }

    public class EditPlanVM : CreatePlanVM
    {
        public Guid Id { get; set; }
        public string? ImageName { get; set; }
    }
}