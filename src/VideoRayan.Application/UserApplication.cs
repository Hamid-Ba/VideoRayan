using System;
using Framework.Api.Jwt;
using Framework.Application;
using Framework.Application.Hashing;
using Framework.Application.Sms;
using VideoRayan.Application.Contract.UserAgg;
using VideoRayan.Domain.UserAgg;

namespace VideoRayan.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IJwtHelper _jwtHelper;
        private readonly ISmsService _smsService;
        private readonly IUserRepository _userRepository;

        public UserApplication(IJwtHelper jwtHelper, ISmsService smsService, IUserRepository userRepository)
        {
            _jwtHelper = jwtHelper;
            _smsService = smsService;
            _userRepository = userRepository;
        }

        public async Task<OperationResult> LoginFirstStep(LoginUserDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetBy(command.Phone!);
            if (user is null) return await Register(new RegisterUserDto { Phone = command.Phone });
            if (!user.IsActive) return result.Failed(ApplicationMessage.UserNotActive);

            user.SetAccessToLoginDate(DateTime.Now.AddMinutes(5));

            var phoneCode = Guid.NewGuid().ToString().Substring(0, 6);

            //ToDo : Send Phone Code
            var smsMessage = $"کاربر گرامی با شماره موبایل {command.Phone},\nکد تایید شما : {phoneCode} می باشد";
            _smsService.SendSms(command.Phone!, smsMessage);

            user.ChangePhoneCode(phoneCode);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Register(RegisterUserDto command)
        {
            OperationResult result = new();

            if (_userRepository.Exists(u => u.Mobile == command.Phone)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            var phoneCode = Guid.NewGuid().ToString().Substring(0, 6);
            var user = User.Register(command.Phone!, phoneCode);

            //ToDo : Send Phone Code
            var smsMessage = $"کاربر گرامی با شماره موبایل {command.Phone},\nکد تایید شما : {phoneCode} می باشد";
            _smsService.SendSms(command.Phone!, smsMessage);

            await _userRepository.AddEntityAsync(user);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<(OperationResult, string)> LoginSecondStep(AccessTokenDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetBy(command.Phone!);

            if (user.LoginExpireDate <= DateTime.Now) return (result.Failed("کد وارد شده نامعتبر می باشد"), "");
            if (user.PhoneCode != command.Token) return (result.Failed(ApplicationMessage.InvalidAccessToken), "");

            var loginDto = new JwtDto
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Mobile = user.Mobile,
                Email = user.Email,
                PhoneCode = user.PhoneCode,
                LoginExpireDate = user.LoginExpireDate
            };

            var token = _jwtHelper.SignIn(loginDto);
            return (result.Succeeded(), token);
        }

        public async Task<(OperationResult, string)> VerifyRegister(AccessTokenDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetBy(command.Phone!);
            if (user.PhoneCode != command.Token) return (result.Failed(ApplicationMessage.InvalidAccessToken), "");

            user.ControlActivation(isActive: true);
            user.SetAccessToLoginDate(DateTime.Now.AddMinutes(5));
            await _userRepository.SaveChangesAsync();

            return await LoginSecondStep(command);
        }
    }
}