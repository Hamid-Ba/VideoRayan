using Framework.Application;
using System.Threading.Tasks;

namespace VideoRayan.Application.Contract.AccountAgg.Contracts
{
    public interface IRolePermissionApplication
    {
        Task<OperationResult> AddPermissionsToRole(long roleId, long[] permissionsId);
    }
}