using System;
using System.ComponentModel.DataAnnotations;
using Framework.Application;

namespace VideoRayan.Application.Contract.UserAgg
{
	public class UserDto : DtoBase
	{
        public string? FirstName { get;  set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Mobile { get; set; }
        public string? PhoneCode { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime LoginExpireDate { get;  set; }
    }

    public class RegisterUserDto
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|5|6|7|8|9|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string? Phone { get; set; }
    }

    public class LoginUserDto : RegisterUserDto
    {

    }

    public class AccessTokenDto : RegisterUserDto
    {
        [Display(Name = "کد فعال سازی")]
        [MaxLength(6)]
        public string? Token { get; set; }
    }
}