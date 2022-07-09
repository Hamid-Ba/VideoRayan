namespace Framework.Application.Authentication
{
    public class VisitorAuthViewModel
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Fullname { get; set; }
        public string? Mobile { get; set; }

        public VisitorAuthViewModel()
        {
        }

        public VisitorAuthViewModel(long id, string code, string fullname, string mobile)
        {
            Id = id;
            Code = code;
            Fullname = fullname;
            Mobile = mobile;
        }
    }

    public class OperatorAuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Fullname { get; set; }
        public string? Mobile { get; set; }

        public OperatorAuthViewModel() { }

        public OperatorAuthViewModel(long id, long roleId,string roleName, string fullname, string mobile)
        {
            Id = id;
            RoleId = roleId;
            RoleName = roleName;
            Fullname = fullname;
            Mobile = mobile;
        }
    }
}