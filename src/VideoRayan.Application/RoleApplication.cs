using Framework.Application;
using VideoRayan.Application.Contract.AccountAgg;
using VideoRayan.Application.Contract.AccountAgg.Contracts;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Domain.AccountAgg.Contracts;

namespace VideoRayan.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRolePermissionApplication _rolePermissionApplication;

        public RoleApplication(IRoleRepository roleRepository, IRolePermissionApplication rolePermissionApplication)
        {
            _roleRepository = roleRepository;
            _rolePermissionApplication = rolePermissionApplication;
        }

        public async Task<OperationResult> Create(CreateRoleVM command)
        {
            OperationResult result = new();

            if (_roleRepository.Exists(a => a.Name == command.Name))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var role = new Role(command.Name!, command.Description!);

            await _roleRepository.AddEntityAsync(role);
            await _roleRepository.SaveChangesAsync();

            await _rolePermissionApplication.AddPermissionsToRole(role.Id, command.PermissionsId!);

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            OperationResult result = new();

            var role = await _roleRepository.GetEntityByIdAsync(id);

            if (role is null) return result.Failed(ApplicationMessage.NotExist);

            role.Delete();
            await _roleRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditRoleVM command)
        {
            OperationResult result = new();

            var role = await _roleRepository.GetEntityByIdAsync(command.Id);

            if (role is null) return result.Failed(ApplicationMessage.NotExist);
            if (_roleRepository.Exists(a => a.Name == command.Name && a.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            role.Edit(command.Name!, command.Description!);
            await _roleRepository.SaveChangesAsync();

            await _rolePermissionApplication.AddPermissionsToRole(role.Id, command.PermissionsId!);

            return result.Succeeded();
        }

        public async Task<IEnumerable<RoleVM>> GetAll() => await _roleRepository.GetAll();

        public async Task<EditRoleVM> GetDetailForEditBy(Guid id) => await _roleRepository.GetDetailForEditBy(id);

        public bool IsRoleHasThePermission(Guid roleId, long permissionId)
        {
            var role = _roleRepository.GetBy(roleId);

            var permissions = role.Permissions?.Select(p => p.PermissionId).ToList();

            foreach (var perId in permissions!) if (perId == permissionId) return true;

            return false;
        }
    }
}