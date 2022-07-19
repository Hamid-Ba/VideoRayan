using Framework.Infrastructure;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Domain.AccountAgg.Contracts;

namespace VideoRayan.Infrastructure.EfCore.Repositories.AccountAgg
{
    public class RolePermissionRepository : Repository<RolePermission>, IRolePermissionRepository
    {
        private readonly VideoRayanContext _context;
        public RolePermissionRepository(VideoRayanContext context) : base(context) => _context = context;
    }
}