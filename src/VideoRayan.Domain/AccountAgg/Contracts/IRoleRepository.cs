using Framework.Domain;
using VideoRayan.Application.Contract.AccountAgg;

namespace VideoRayan.Domain.AccountAgg.Contracts
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetBy(long roleId);
        Task<IEnumerable<RoleVM>> GetAll();
        Task<EditRoleVM> GetDetailForEditBy(long id);
    }
}