using Framework.Application;

namespace VideoRayan.Application.Contract.AccountAgg.Contracts
{
    public interface IOperatorApplication
    {
        Task<OperationResult> Delete(Guid id);
        Task<IEnumerable<OperatorVM>> GetAll();
        Task<EditOperatorVM> GetDetailForEditBy(Guid id);
        Task<OperationResult> Edit(EditOperatorVM command);
        Task<OperationResult> Login(LoginOperatorVM command);
        Task<OperationResult> Create(CreateOperatorVM command);
        bool IsOperatorHasPermissions(long permissionId, Guid operatorId);
    }
}