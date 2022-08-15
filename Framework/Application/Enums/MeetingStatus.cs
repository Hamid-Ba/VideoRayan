using System.ComponentModel.DataAnnotations;

namespace Framework.Application.Enums
{
    public enum MeetingStatus
    {
        [Display(Name = "فرا نرسیده")]
        HasNotArrived = 0,
        [Display(Name = "شروع شده")]
        HasArrived = 1,
        [Display(Name = "به پایان رسیده")]
        Done = 2
    }
}