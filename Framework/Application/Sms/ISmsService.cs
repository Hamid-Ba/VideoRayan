namespace Framework.Application.Sms
{
    public interface ISmsService
    {
        Task SendSms(string mobile, string message);
        Task SendVerifySms(string mobile, string token);
        Task SendMeetingSms(string mobile, string template, string[] param);
    }
}