namespace Framework.Application.Sms
{
    public interface ISmsService
    {
        Task SendSms(string mobile, string message);
        Task SendVerifySms(string mobile, string token);
        Task SendConfrimMeetingSms(string mobile, string template, string[] param);
        Task SendConfrimFaceToFaceSms(string mobile, string template, string[] param);
        Task SendDisConfrimMeetingSms(string mobile, string template, string[] param);
    }
}