using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.AccountAgg;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Domain.AccountAgg.Contracts;

namespace VideoRayan.Infrastructure.EfCore.Repositories.AccountAgg
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly VideoRayanContext _context;

        public RoleRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<RoleVM>> GetAll() => await _context.Roles.Select(u => new RoleVM()
        {
            Id = u.Id,
            Name = u.Name
        }).AsNoTracking().ToListAsync();

        public Role GetBy(Guid roleId) => _context.Roles.Find(roleId)!;

        public async Task<EditRoleVM> GetDetailForEditBy(Guid id) => (await _context.Roles.Select(r => new EditRoleVM
        {
            Id = r.Id,
            Name = r.Name,
            PermissionsId = r.Permissions!.Select(p => p.PermissionId).ToArray(),
            Description = r.Description
        }).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id))!;
        
        
    }
}