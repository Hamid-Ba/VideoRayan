using System;
namespace Framework.Application.Sms
{
	public interface ISmsService
	{
		Task SendVerifySms(string mobile, string token);
	}
}