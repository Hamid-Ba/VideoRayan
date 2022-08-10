using Framework.Api.Jwt;
using Framework.Application;
using Framework.Application.Enums;
using Framework.Application.Sms;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.CustomerAgg.Contracts;

namespace VideoRayan.Application
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly IJwtHelper _jwtHelper;
        private readonly ISmsService _smsService;
        private readonly ICustomerRepository _userRepository;

        public CustomerApplication(IJwtHelper jwtHelper, ISmsService smsService, ICustomerRepository userRepository)
        {
            _jwtHelper = jwtHelper;
            _smsService = smsService;
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Create(CreateCustomerDto command)
        {
            OperationResult result = new();

            if (_userRepository.Exists(u => u.Mobile == command.Mobile || u.Email == command.Email))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var imageName = Uploader.ImageUploader(command.Logo!, "Customer", null!);

            var customer = new Customer(command.Title!, command.Mobile!, "", imageName, command.FirstName!, command.LastName!, command.Email!, command.Type);
            await _userRepository.AddEntityAsync(customer);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<(OperationResult, CustomerDto)> Edit(EditCustomerDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetEntityByIdAsync(command.Id);

            if (user is null) return (result.Failed(ApplicationMessage.UserNotExist), default)!;
            if (_userRepository.Exists(u => u.Mobile == command.Mobile && u.Id != command.Id))
                return (result.Failed(ApplicationMessage.DuplicatedModel), default)!;

            user.Edit(command.Mobile!, command.FirstName!, command.LastName!, command.Email!);
            await _userRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(command.Id));
        }

        public async Task<OperationResult> Edit(EditByAdminCustomerDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetEntityByIdAsync(command.Id);

            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);
            if (_userRepository.Exists(u => u.Mobile == command.Mobile && u.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var logo = Uploader.ImageUploader(command.LogoFile!, "Customer", command.Logo!);

            user.Edit(command.Title!, command.Mobile!, logo, command.FirstName!, command.LastName!, command.Email!, command.Type);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<CustomerDto> GetBy(Guid id) => await _userRepository.GetBy(id);

        public async Task<IEnumerable<CustomerDto>> GetAll(CustomerType type) => await _userRepository.GetAll(type);

        public async Task<EditCustomerDto> GetDetailForEditBy(Guid id) => await _userRepository.GetDetailForEditBy(id);

        public async Task<EditByAdminCustomerDto> GetDetailForEditByAdmin(Guid id) => await _userRepository.GetDetailForEditByAdmin(id);

        public async Task<OperationResult> LoginFirstStep(LoginCustomerDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetBy(command.Phone!);
            if (user is null) return await Register(new RegisterCustomerDto { Phone = command.Phone });
            if (!user.IsActive) return result.Failed(ApplicationMessage.UserNotActive);

            user.SetAccessToLoginDate(DateTime.Now.AddMinutes(5));

            var phoneCode = new Random().Next(100000, 999999).ToString();//Guid.NewGuid().ToString().Substring(0, 6);

            //ToDo : Send Phone Code
            //var smsMessage = $"کاربر گرامی با شماره موبایل {command.Phone},\nکد تایید شما : {phoneCode} می باشد";
            await _smsService.SendVerifySms(command.Phone!, phoneCode);

            user.ChangePhoneCode(phoneCode);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Register(RegisterCustomerDto command)
        {
            OperationResult result = new();

            if (_userRepository.Exists(u => u.Mobile == command.Phone)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            var phoneCode = new Random().Next(100000, 999999).ToString();//Guid.NewGuid().ToString().Substring(0, 6);
            var user = Customer.Register(command.Phone!, phoneCode);

            //ToDo : Send Phone Code
            //var smsMessage = $"کاربر گرامی با شماره موبایل {command.Phone},\nکد تایید شما : {phoneCode} می باشد";
            await _smsService.SendVerifySms(command.Phone!, phoneCode);

            await _userRepository.AddEntityAsync(user);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<(OperationResult, string, string)> VerifyLoginRegister(AccessTokenDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetBy(command.Phone!);

            if (user.LoginExpireDate <= DateTime.Now) return (result.Failed("کد وارد شده نامعتبر می باشد، مجدداً لاگین کنید"), "", "");
            if (user.PhoneCode != command.Token) return (result.Failed(ApplicationMessage.InvalidAccessToken), "", "");

            var phoneCode = new Random().Next(100000, 999999).ToString();//Guid.NewGuid().ToString().Substring(0, 6);
            user.ChangePhoneCode(phoneCode);
            await _userRepository.SaveChangesAsync();

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
            return (result.Succeeded(), user.Id.ToString(), token);
        }

        public async Task<OperationResult> ActiveOrDeactive(Guid id)
        {
            OperationResult result = new();

            var user = await _userRepository.GetEntityByIdAsync(id);
            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            user.ControlActivation(!user.IsActive);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> SendMessage(SendSmsCustomerDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetEntityByIdAsync(command.Id);
            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            //ToDo : Send Phone Code
            _smsService.SendVerifySms(user.Mobile!, command.Message!);

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            OperationResult result = new();

            var user = await _userRepository.GetEntityByIdAsync(id);
            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            user.Delete();
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<string> GetPhone(Guid id) => await _userRepository.GetPhone(id);

        public async Task<CustomerType> GetTypeBy(Guid id) => await _userRepository.GetTypeBy(id);

        public async Task<OperationResult> EditLogo(EditLogoCustomerDto command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetEntityByIdAsync(command.Id);

            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            var logo = Uploader.ImageUploader(command.LogoFile!, "Customer", user.Logo!);

            user.EditLogo(logo);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }


        //public async Task<(OperationResult, string)> VerifyRegister(AccessTokenDto command)
        //{
        //    OperationResult result = new();

        //    var user = await _userRepository.GetBy(command.Phone!);
        //    if (user.PhoneCode != command.Token) return (result.Failed(ApplicationMessage.InvalidAccessToken), "");

        //    user.ControlActivation(isActive: true);
        //    user.SetAccessToLoginDate(DateTime.Now.AddMinutes(5));
        //    await _userRepository.SaveChangesAsync();

        //    return await LoginSecondStep(command);
        //}
    }
}