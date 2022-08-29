using Microsoft.Extensions.Configuration;

namespace Framework.Application.Sms
{
    public class SmsService : ISmsService
    {
        private IConfiguration _configuration;

        public SmsService(IConfiguration configuration) => _configuration = configuration;

        public async Task SendConfrimFaceToFaceSms(string mobile, string template, string[] param)
        {
            var smsConfig = _configuration.GetSection("SmsService");
            var sms = new Ghasedak.Core.Api(smsConfig.GetSection("ApiKey").Value);
            await sms.VerifyAsync(1, "ConfirmMeeting", new string[] { mobile }, param[0], param[1], param[2], param[3]);
        }

        public async Task SendConfrimMeetingSms(string mobile, string template, string[] param)
        {
            var smsConfig = _configuration.GetSection("SmsService");
            var sms = new Ghasedak.Core.Api(smsConfig.GetSection("ApiKey").Value);
            await sms.VerifyAsync(1, "ConfirmMeeting", new string[] { mobile }, param[0], param[1], param[2], param[3], param[4]);
        }

        public async Task SendDisConfrimMeetingSms(string mobile, string template, string[] param)
        {
            var smsConfig = _configuration.GetSection("SmsService");
            var sms = new Ghasedak.Core.Api(smsConfig.GetSection("ApiKey").Value);
            await sms.VerifyAsync(1, "DisConfrimMeeting", new string[] { mobile }, param[0], param[1], param[2]);
        }

        public async Task SendSms(string mobile, string message)
        {
            try
            {
                var smsConfig = _configuration.GetSection("SmsService");
                var sms = new Ghasedak.Core.Api(smsConfig.GetSection("ApiKey").Value);
                await sms.SendSMSAsync(message, mobile, "300002525");
            }
            catch (Ghasedak.Core.Exceptions.ApiException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Ghasedak.Core.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task SendVerifySms(string mobile, string token)
        {
            var smsConfig = _configuration.GetSection("SmsService");
            var sms = new Ghasedak.Core.Api(smsConfig.GetSection("ApiKey").Value);
            await sms.VerifyAsync(1, "videorayan", new string[] { mobile }, token);
        }
    }
}