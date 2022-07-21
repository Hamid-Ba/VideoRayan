using Framework.Application;
using Framework.Application.Enums;

namespace VideoRayan.Application.Contract.CustomerAgg.Contracts
{
    public interface ICustomerApplication
	{
		Task<CustomerDto> GetBy(Guid id);
        Task<OperationResult> ActiveOrDeactive(Guid id);
		Task<EditCustomerDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditCustomerDto command);
		Task<OperationResult> Create(CreateCustomerDto command);
		Task<IEnumerable<CustomerDto>> GetAll(CustomerType type);
		Task<OperationResult> Edit(EditByAdminCustomerDto command);
		Task<OperationResult> Register(RegisterCustomerDto command);
		Task<OperationResult> SendMessage(SendSmsCustomerDto command);
		Task<EditByAdminCustomerDto> GetDetailForEditByAdmin(Guid id);
		Task<OperationResult> LoginFirstStep(LoginCustomerDto command);
		//Task<(OperationResult, string)> VerifyRegister(AccessTokenDto command);
		Task<(OperationResult, string)> VerifyLoginRegister(AccessTokenDto command);
    }
}