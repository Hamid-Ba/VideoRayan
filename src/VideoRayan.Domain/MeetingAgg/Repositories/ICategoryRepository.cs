using Framework.Domain;
using VideoRayan.Application.Contract.MeetingAgg;

namespace VideoRayan.Domain.MeetingAgg.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<CategoryDto> GetBy(Guid id);
        Task<EditCategoryDto> GetDetailForEditBy(Guid id);
        Task<IEnumerable<CategoryDto>> GetAll(Guid customerId);
    }
}