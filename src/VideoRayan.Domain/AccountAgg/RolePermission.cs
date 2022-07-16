namespace VideoRayan.Domain.AccountAgg
{
    public class RolePermission
    {
        public Guid RoleId { get; private set; }
        public long PermissionId { get; private set; }

        public Role? Role { get; private set; }
        public Permission? Permission { get; private set; }

        public RolePermission(Guid roleId, long permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }
}