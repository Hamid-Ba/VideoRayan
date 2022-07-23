using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Infrastructure.EfCore.Repositories.MeetingAgg
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly VideoRayanContext _context;

        public CategoryRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<CategoryDto>> GetAll(Guid customerId) => await _context.Categories.Where(c => c.CustomerId == customerId)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                CustomerId = c.CustomerId,
                Title = c.Title,
                Description = c.Description,
                PersianCreationDate = c.CreationDate.ToFarsi()
            }).AsNoTracking().ToListAsync();

        public async Task<CategoryDto> GetBy(Guid id) => (await _context.Categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            CustomerId = c.CustomerId,
            Title = c.Title,
            Description = c.Description,
            PersianCreationDate = c.CreationDate.ToFarsi()
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;


        public async Task<EditCategoryDto> GetDetailForEditBy(Guid id) => (await _context.Categories.Select(c => new EditCategoryDto
        {
            Id = c.Id,
            CustomerId = c.CustomerId,
            Title = c.Title,
            Description = c.Description
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;
    }
}