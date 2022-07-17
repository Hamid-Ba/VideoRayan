using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoRayan.Application.Contract.AccountAgg;

namespace VideoRayan.Application.Contract.AccountAgg.Contracts
{
    public interface IOperatorApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<OperatorVM>> GetAll();
        Task<EditOperatorVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditOperatorVM command);
        Task<OperationResult> Login(LoginOperatorVM command);
        Task<OperationResult> Create(CreateOperatorVM command);
        bool IsOperatorHasPermissions(long permissionId, long operatorId);
    }
}