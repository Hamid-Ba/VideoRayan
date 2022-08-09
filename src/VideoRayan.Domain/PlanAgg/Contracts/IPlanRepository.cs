using Framework.Domain;
using VideoRayan.Application.Contract.PlanAgg;

namespace VideoRayan.Domain.PlanAgg.Contracts
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<PlanVM> GetPlanBy(Guid id);
        Task<IEnumerable<PlanVM>> GetAll();
        Task<EditPlanVM> GetDetailForEditBy(Guid id);
    }
}