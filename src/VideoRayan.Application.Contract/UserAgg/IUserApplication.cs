using Framework.Application;

namespace VideoRayan.Application.Contract.UserAgg
{
    public interface IUserApplication
	{
		Task<EditUserDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditUserDto command);
		Task<OperationResult> Register(RegisterUserDto command);
		Task<OperationResult> LoginFirstStep(LoginUserDto command);
		//Task<(OperationResult, string)> VerifyRegister(AccessTokenDto command);
		Task<(OperationResult, string)> VerifyLoginRegister(AccessTokenDto command);
	}
}