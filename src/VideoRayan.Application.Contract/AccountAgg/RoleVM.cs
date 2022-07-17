using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace VideoRayan.Application.Contract.AccountAgg
{
    public class RoleVM : DtoBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class CreateRoleVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? Name { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "دسترسی ها")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long[]? PermissionsId { get; set; }
    }

    public class EditRoleVM : CreateRoleVM
    {
        public Guid Id { get; set; }
    }
}
