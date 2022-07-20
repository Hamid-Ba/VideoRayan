using Framework.Application;

namespace VideoRayan.Application.Contract.CustomerAgg.Contracts
{
    public interface ICustomerApplication
	{
		Task<EditCustomerDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditCustomerDto command);
		Task<OperationResult> Create(CreateCustomerDto command);
		Task<OperationResult> Edit(EditByAdminCustomerDto command);
		Task<OperationResult> Register(RegisterCustomerDto command);
		Task<EditByAdminCustomerDto> GetDetailForEditByAdmin(Guid id);
		Task<OperationResult> LoginFirstStep(LoginCustomerDto command);
		//Task<(OperationResult, string)> VerifyRegister(AccessTokenDto command);
		Task<(OperationResult, string)> VerifyLoginRegister(AccessTokenDto command);
	}
}