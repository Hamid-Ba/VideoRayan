using Microsoft.Extensions.Configuration;

namespace Framework.Application.Sms
{
    public class SmsService : ISmsService
	{
        private IConfiguration _configuration;

        public SmsService(IConfiguration configuration) => _configuration = configuration;

        public async Task SendVerifySms(string mobile, string token)
        {
            var smsConfig = _configuration.GetSection("SmsService");
            var api = new Ghasedak.Core.Api(smsConfig.GetSection("ApiKey").Value);
            await api.VerifyAsync(1, "pedpo", new string[] { mobile }, token);
        }
    }
}