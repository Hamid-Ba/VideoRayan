using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.AccountAgg;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Domain.AccountAgg.Contracts;

namespace VideoRayan.Infrastructure.EfCore.Repositories.AccountAgg
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        private readonly VideoRayanContext _context;
        public PermissionRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<PermissionVM>> GetAll() => await _context.Permissions.Select(p => new PermissionVM
        {
            Id = p.Id,
            ParentId = p.ParentId,
            Title = p.Title,
        }).AsNoTracking().ToListAsync();

    }
}