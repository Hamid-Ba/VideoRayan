using Framework.Domain;

namespace VideoRayan.Domain.AccountAgg
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public List<Operator>? Operators { get; private set; }
        public List<RolePermission>? Permissions { get; private set; }

        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Edit(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}