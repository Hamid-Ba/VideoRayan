using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.PlanAgg;
using VideoRayan.Domain.PlanAgg;
using VideoRayan.Domain.PlanAgg.Contracts;

namespace VideoRayan.Infrastructure.EfCore.Repositories.PlanAgg
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        private readonly VideoRayanContext _context;

        public PlanRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<PlanVM> GetPlanBy(Guid id) => (await _context.Plans.Select(p => new PlanVM()
        {
            Id = p.Id,
            Title = p.Title,
            PeriodPerDay = p.PeriodPerDay,
            Cost = p.Cost,
            Description = p.Description,
            Ps = p.Ps,
            ImageName = p.ImageName,
            OrderCount = p.OrderCount
        }).FirstOrDefaultAsync(p => p.Id == id))!;

        public async Task<EditPlanVM> GetDetailForEditBy(Guid id) => (await _context.Plans.Select(p => new EditPlanVM()
        {
            Id = p.Id,
            Title = p.Title,
            PeriodPerDay = p.PeriodPerDay,
            Cost = p.Cost,
            Description = p.Description,
            Ps = p.Ps,
            ImageName = p.ImageName
        }).FirstOrDefaultAsync(p => p.Id == id))!;

        public async Task<IEnumerable<PlanVM>> GetAll() => await _context.Plans.Select(p => new PlanVM()
        {
            Id = p.Id,
            Title = p.Title,
            PeriodPerDay = p.PeriodPerDay,
            Cost = p.Cost,
            Description = p.Description,
            Ps = p.Ps,
            ImageName = p.ImageName,
            OrderCount = p.OrderCount
        }).AsNoTracking().OrderByDescending(o => o.Id).ToListAsync();

    }
}