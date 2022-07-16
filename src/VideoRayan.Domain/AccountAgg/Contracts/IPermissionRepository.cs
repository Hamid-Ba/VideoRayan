using Framework.Domain;

namespace VideoRayan.Domain.AccountAgg.Contracts
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        //Task<IEnumerable<PermissionVM>> GetAll();
    }
}