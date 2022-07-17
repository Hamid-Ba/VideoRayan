using Framework.Application;
using VideoRayan.Application.Contract.CustomerAgg;

namespace VideoRayan.Application.Contract.CustomerAgg.Contracts
{
    public interface ICustomerApplication
	{
		Task<EditCustomerDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditCustomerDto command);
		Task<OperationResult> Register(RegisterCustomerDto command);
		Task<OperationResult> LoginFirstStep(LoginCustomerDto command);
		//Task<(OperationResult, string)> VerifyRegister(AccessTokenDto command);
		Task<(OperationResult, string)> VerifyLoginRegister(AccessTokenDto command);
	}
}