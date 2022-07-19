using Framework.Application;
using Framework.Application.Authentication;
using Framework.Application.Hashing;
using VideoRayan.Application.Contract.AccountAgg;
using VideoRayan.Application.Contract.AccountAgg.Contracts;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Domain.AccountAgg.Contracts;

namespace VideoRayan.Application
{
    public class OperatorApplication : IOperatorApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleApplication _roleApplication;
        private readonly IOperatorRepository _operatorRepository;

        public OperatorApplication(IAuthHelper authHelper, IPasswordHasher passwordHasher, IRoleApplication roleRepository, IOperatorRepository operatorRepository)
        {
            _authHelper = authHelper;
            _passwordHasher = passwordHasher;
            _roleApplication = roleRepository;
            _operatorRepository = operatorRepository;
        }

        public async Task<OperationResult> Create(CreateOperatorVM command)
        {
            OperationResult result = new();

            if (_operatorRepository.Exists(o => o.Mobile == command.Mobile)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            var password = _passwordHasher.Hash(command.Password!);

            var user = new Operator(command.RoleId, command.FullName!, command.Mobile!, password);

            await _operatorRepository.AddEntityAsync(user);
            await _operatorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            OperationResult result = new();

            var user = await _operatorRepository.GetEntityByIdAsync(id);
            if (user is null) result.Failed(ApplicationMessage.UserNotExist);

            user!.Delete();
            await _operatorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditOperatorVM command)
        {
            OperationResult result = new();

            var user = await _operatorRepository.GetEntityByIdAsync(command.Id);

            if (user is null) result.Failed(ApplicationMessage.UserNotExist);
            if (_operatorRepository.Exists(o => o.Mobile == command.Mobile && o.Id != command.Id)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            string newPassword = "";

            if (!string.IsNullOrWhiteSpace(command.Password)) newPassword = _passwordHasher.Hash(command.Password);

            user!.Edit(command.RoleId, command.FullName!, command.Mobile!, newPassword);
            await _operatorRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<OperatorVM>> GetAll() => await _operatorRepository.GetAll();

        public async Task<EditOperatorVM> GetDetailForEditBy(Guid id) => await _operatorRepository.GetDetailForEditBy(id);

        public bool IsOperatorHasPermissions(long permissionId, Guid operatorId)
        {
            var user = _operatorRepository.GetEntityById(operatorId);

            if (_roleApplication.IsRoleHasThePermission(user.RoleId, permissionId)) return true;

            return false;
        }

        public async Task<OperationResult> Login(LoginOperatorVM command)
        {
            OperationResult result = new();

            var user = await _operatorRepository.GetBy(command.Mobile!);

            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            if (!_passwordHasher.Check(user.Password, command.Password!).Verified) return result.Failed(ApplicationMessage.WrongPassword);

            //ToDo : Login Section
            var userVM = new OperatorAuthViewModel
            {
                Id = user.Id,
                RoleId = user.RoleId,
                RoleName = user.Role!.Name,
                Fullname = user.FullName,
                Mobile = user.Mobile
            };

            _authHelper.SignInAsync(userVM);

            return result.Succeeded();
        }
    }
}