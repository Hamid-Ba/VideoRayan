using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Framework.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace VideoRayan.Application.Contract.CustomerAgg
{
    public class CustomerDto : DtoBase
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? PhoneCode { get; set; }
        public string? Logo { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public CustomerType Type { get; set; }
        public DateTime LoginExpireDate { get; set; }
        public string? PersianCreationDate { get; set; }
    }

    public class CreateCustomerDto
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|5|6|7|8|9|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string? Mobile { get; set; }
        public IFormFile? Logo { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }

        [Range(0,1,ErrorMessage = ValidationMessage.IsRequired)]
        public CustomerType Type { get; set; }
    }

    public class EditCustomerDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
    }

    public class EditLogoCustomerDto
    {
        public Guid Id { get; set; }
        public string? Logo { get; set; }
        public IFormFile? LogoFile { get; set; }
    }

    public class EditByAdminCustomerDto 
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Title { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|5|6|7|8|9|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string? Mobile { get; set; }
        public string? Logo { get; set; }
        public IFormFile? LogoFile { get; set; }
        public string? Email { get; set; }
        public CustomerType Type { get; set; }
    }

    public class RegisterCustomerDto
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|5|6|7|8|9|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string? Phone { get; set; }
    }

    public class LoginCustomerDto : RegisterCustomerDto { }

    public class AccessTokenDto : RegisterCustomerDto
    {
        [Display(Name = "کد فعال سازی")]
        [MaxLength(6)]
        public string? Token { get; set; }
    }

    public class SendSmsCustomerDto
    {
        public Guid Id { get; set; }
        
        [Display(Name = "پیام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(500, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string? Message { get; set; }
    }
}