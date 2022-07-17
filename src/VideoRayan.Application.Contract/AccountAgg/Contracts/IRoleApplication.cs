using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoRayan.Application.Contract.AccountAgg.Contracts
{
    public interface IRoleApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<RoleVM>> GetAll();
        Task<EditRoleVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditRoleVM command);
        Task<OperationResult> Create(CreateRoleVM command);
        bool IsRoleHasThePermission(long roleId, long permissionId);
    }
}
