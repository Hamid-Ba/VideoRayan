using Framework.Domain;

namespace VideoRayan.Domain.AccountAgg
{
    public class Operator : EntityBase
    {
        public Guid RoleId { get; private set; }
        public string FullName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }

        public Role? Role { get; private set; }

        public Operator(Guid roleId, string fullName, string mobile, string password)
        {
            RoleId = roleId;
            FullName = fullName;
            Mobile = mobile;
            Password = password;
        }

        public void Edit(Guid roleId, string fullName, string mobile, string password)
        {
            RoleId = roleId;
            FullName = fullName;
            Mobile = mobile;

            if (!string.IsNullOrWhiteSpace(password))
                Password = password;

            LastUpdateDate = DateTime.Now;
        }
    }
}