using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.CustomerAgg.Contracts;

namespace VideoRayan.Infrastructure.EfCore.Repositories.CustomerAgg
{
    public class AudienceRepository : Repository<Audience>, IAudienceRepository
    {
        private readonly VideoRayanContext _context;

        public AudienceRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<AudienceDto>> GetAll(Guid customerId, string categoryName) => await _context.Audiences.Include(c => c.Category).Include(c => c.User)
            .Where(c => c.UserId == customerId && c.Category!.Title.Contains(categoryName)).Select(c => new AudienceDto
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                CategoryTitle = c.Category!.Title,
                PersianCreationDate = c.CreationDate.ToFarsi(),
                FullName = c.FullName,
                UserId = c.UserId,
                Mobile = c.Mobile,
                Position = c.Position,
                CreatorName = $"{c.User!.FirstName} {c.User!.LastName}"
            }).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<AudienceDto>> GetAllBy(Guid meetingId)
        {
            var meeting = await _context.Meetings.Include(m => m.Audiences).FirstOrDefaultAsync(m => m.Id == meetingId);

            var audiences = meeting!.Audiences!.Select(a => a.AudienceId).ToList();

            List<AudienceDto> result = new();

            foreach (var audience in audiences)
            {
                var audi = (await _context.Audiences.Include(c => c.Category).Select(c => new AudienceDto
                {
                    Id = c.Id,
                    CategoryId = c.CategoryId,
                    CategoryTitle = c.Category!.Title,
                    PersianCreationDate = c.CreationDate.ToFarsi(),
                    FullName = c.FullName,
                    UserId = c.UserId,
                    Mobile = c.Mobile,
                    Position = c.Position,
                }).FirstOrDefaultAsync(u => u.Id == audience));

                result.Add(audi!);
            }

            return result;
        }

        public async Task<AudienceDto> GetBy(Guid id) => (await _context.Audiences.Include(c => c.Category).Include(c => c.User)
            .Select(c => new AudienceDto
            {
                Id = c.Id,
                CategoryId = c.CategoryId,
                CategoryTitle = c.Category!.Title,
                PersianCreationDate = c.CreationDate.ToFarsi(),
                FullName = c.FullName,
                UserId = c.UserId,
                Mobile = c.Mobile,
                Position = c.Position,
                CreatorName = $"{c.User!.FirstName} {c.User!.LastName}"
            }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

        public async Task<EditAudienceDto> GetDetailForEditBy(Guid id) => (await _context.Audiences.Select(c => new EditAudienceDto
        {
            Id = c.Id,
            UserId = c.UserId,
            CategoryId = c.CategoryId,
            FullName = c.FullName,
            Mobile = c.Mobile,
            Position = c.Position
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

    }
}