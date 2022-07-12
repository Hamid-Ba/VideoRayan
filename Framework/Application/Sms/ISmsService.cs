using System;
namespace Framework.Application.Sms
{
	public interface ISmsService
	{
		void SendSms(string mobile, string message);
	}
}