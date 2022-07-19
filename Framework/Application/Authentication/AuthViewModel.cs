namespace Framework.Application.Authentication
{
    public class OperatorAuthViewModel
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? Fullname { get; set; }
        public string? Mobile { get; set; }

        public OperatorAuthViewModel() { }

        public OperatorAuthViewModel(Guid id, Guid roleId, string roleName, string fullname, string mobile)
        {
            Id = id;
            RoleId = roleId;
            RoleName = roleName;
            Fullname = fullname;
            Mobile = mobile;
        }
    }
}