using Framework.Domain;
using VideoRayan.Application.Contract.AccountAgg;

namespace VideoRayan.Domain.AccountAgg.Contracts
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<IEnumerable<PermissionVM>> GetAll();
    }
}