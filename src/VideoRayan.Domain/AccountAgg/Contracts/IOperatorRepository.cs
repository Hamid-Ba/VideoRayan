using Framework.Domain;
using VideoRayan.Application.Contract.AccountAgg;

namespace VideoRayan.Domain.AccountAgg.Contracts
{
    public interface IOperatorRepository : IRepository<Operator>
    {
        Task<Operator> GetBy(string mobile);
        Task<IEnumerable<OperatorVM>> GetAll();
        Task<EditOperatorVM> GetDetailForEditBy(Guid id);
    }
}