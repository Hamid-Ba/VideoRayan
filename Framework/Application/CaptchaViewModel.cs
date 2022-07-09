using System.ComponentModel.DataAnnotations;

namespace Framework.Application
{
    public class CaptchaViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Captcha { get; set; }
    }
}
