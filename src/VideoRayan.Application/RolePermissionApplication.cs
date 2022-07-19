using Framework.Application;
using VideoRayan.Application.Contract.AccountAgg.Contracts;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Domain.AccountAgg.Contracts;

namespace VideoRayan.Application
{
    public class RolePermissionApplication : IRolePermissionApplication
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionApplication(IRolePermissionRepository rolePermissionRepository) => _rolePermissionRepository = rolePermissionRepository;

        public async Task<OperationResult> AddPermissionsToRole(Guid roleId, long[] permissionsId)
        {
            OperationResult result = new();

            var perviousPermissions = await _rolePermissionRepository.GetAllEntitiesAsync();

            if (permissionsId != null && permissionsId.Count() > 0)
            {
                foreach (var permission in perviousPermissions) if (permission.RoleId == roleId) _rolePermissionRepository.DeleteEntity(permission);

                foreach (var permissionId in permissionsId)
                {
                    if (permissionId == 0) continue;
                    var rolePermission = new RolePermission(roleId, permissionId);
                    await _rolePermissionRepository.AddEntityAsync(rolePermission);
                }

                await _rolePermissionRepository.SaveChangesAsync();
            }

            return result.Succeeded();
        }
    }
}