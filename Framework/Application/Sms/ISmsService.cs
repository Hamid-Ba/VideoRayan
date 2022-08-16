using System;
namespace Framework.Application.Sms
{
	public interface ISmsService
	{
		Task SendSms(string mobile, string message);
		Task SendVerifySms(string mobile, string token);
	}
}