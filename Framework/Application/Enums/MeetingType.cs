using System.ComponentModel.DataAnnotations;

namespace Framework.Application.Enums
{
    public enum MeetingType
	{
        [Display(Name = "خصوصی")]
		PRIVATE = 0,

		[Display(Name = "عمومی")]
		PUBLIC = 1
	}
}