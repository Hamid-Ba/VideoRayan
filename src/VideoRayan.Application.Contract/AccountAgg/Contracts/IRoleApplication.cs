using Framework.Application;

namespace VideoRayan.Application.Contract.AccountAgg.Contracts
{
    public interface IRoleApplication
    {
        Task<OperationResult> Delete(Guid id);
        Task<IEnumerable<RoleVM>> GetAll();
        Task<EditRoleVM> GetDetailForEditBy(Guid id);
        Task<OperationResult> Edit(EditRoleVM command);
        Task<OperationResult> Create(CreateRoleVM command);
        bool IsRoleHasThePermission(Guid roleId, long permissionId);
    }
}