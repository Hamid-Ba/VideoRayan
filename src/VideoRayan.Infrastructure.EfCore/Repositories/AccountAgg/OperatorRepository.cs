using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.AccountAgg;
using VideoRayan.Domain.AccountAgg;
using VideoRayan.Domain.AccountAgg.Contracts;

namespace VideoRayan.Infrastructure.EfCore.Repositories.AccountAgg
{
    public class OperatorRepository : Repository<Operator>, IOperatorRepository
    {
        private readonly VideoRayanContext _context;

        public OperatorRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<OperatorVM>> GetAll() => await _context.Operators.Include(r => r.Role).Select(o => new OperatorVM
        {
            Id = o.Id,
            FullName = o.FullName,
            Mobile = o.Mobile,
            RoleId = o.RoleId,
            RoleName = o.Role!.Name
        }).AsNoTracking().ToListAsync();

        public async Task<Operator> GetBy(string mobile) => (await _context.Operators.Include(r => r.Role).FirstOrDefaultAsync(o => o.Mobile == mobile))!;

        public async Task<EditOperatorVM> GetDetailForEditBy(Guid id) => (await _context.Operators.Select(o => new EditOperatorVM
        {
            Id = o.Id,
            FullName = o.FullName,
            Mobile = o.Mobile,
            RoleId = o.RoleId,
        }).AsNoTracking().FirstOrDefaultAsync(o => o.Id == id))!;

    }
}