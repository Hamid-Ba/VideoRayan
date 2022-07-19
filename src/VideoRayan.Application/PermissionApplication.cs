using VideoRayan.Application.Contract.AccountAgg;
using VideoRayan.Application.Contract.AccountAgg.Contracts;
using VideoRayan.Domain.AccountAgg.Contracts;

namespace VideoRayan.Application
{
    public class PermissionApplication : IPermissionApplication
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionApplication(IPermissionRepository permissionRepository) => _permissionRepository = permissionRepository;

        public async Task<IEnumerable<PermissionVM>> GetAll() => await _permissionRepository.GetAll();

    }
}