﻿using System;
using Framework.Application;

namespace VideoRayan.Application.Contract.UserAgg
{
	public interface IUserApplication
	{
		Task<OperationResult> Register(RegisterUserDto command);
		Task<OperationResult> LoginFirstStep(LoginUserDto command);
		Task<(OperationResult, string)> VerifyRegister(AccessTokenDto command);
		Task<(OperationResult, string)> LoginSecondStep(AccessTokenDto command);
	}
}