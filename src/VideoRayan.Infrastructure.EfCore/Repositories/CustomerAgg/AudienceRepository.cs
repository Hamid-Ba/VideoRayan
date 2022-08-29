using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.CustomerAgg.Contracts;

namespace VideoRayan.Infrastructure.EfCore.Repositories.CustomerAgg
{
    public class AudienceRepository : Repository<Audience>, IAudienceRepository
    {
        private readonly VideoRayanContext _context;

        public AudienceRepository(VideoRayanContext context) : base(context) => _context = context;

        public async Task<IEnumerable<AudienceDto>> GetAll(SearchAudienceDto filter)
        {
            var result = _context.Audiences.Include(c => c.Category).Include(c => c.User)
             .Where(c => c.UserId == filter.CustomerId).Select(c => new AudienceDto
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
             }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Category)) result = result.Where(a => a.CategoryTitle!.Contains(filter.Category));

            return await result.ToListAsync();
        }

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

        public async Task<IEnumerable<AudienceDto>> GetAllByFaceToFace(Guid meetingId)
        {
            var meeting = await _context.FaceToFaces.Include(m => m.Audiences).FirstOrDefaultAsync(m => m.Id == meetingId);

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

        public async Task<GetAllAudienceDto> GetAllPaginated(SearchAudienceDto filter)
        {
            var data = _context.Audiences.Include(c => c.Category).Include(c => c.User)
            .Where(c => c.UserId == filter.CustomerId).Select(c => new AudienceDto
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
            }).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Category)) data = data.Where(a => a.CategoryTitle!.Contains(filter.Category));

            var result = new GetAllAudienceDto()
            {
                FilterParams = filter,
                Data = await data.Skip((filter.PageId - 1) * filter.Take).Take(filter.Take).ToListAsync()
            };
            result.GeneratePaging(data, filter.Take, filter.PageId);

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

        public async Task<SendStatusMeetingDto> GetForSendingSms(Guid id, bool isMeeting)
        {
            if (isMeeting)
            {
                var audiences = await _context.AudienceMeetings.Include(a => a.Audience).Where(a => a.MeetingId == id).Select(_ => _.Audience!.Mobile).ToArrayAsync();
                var hostInfo = (await _context.Meetings.Where(a => a.Id == id).Select(_ => new { HostId = _.HostId, MasterPinCode = _.MasterPinCode }).FirstOrDefaultAsync())!;
                var host = await _context.Audiences.FirstOrDefaultAsync(a => a.Id == hostInfo.HostId); 

                return (await _context.Meetings.Select(m => new SendStatusMeetingDto
                {
                    Id = m.Id,
                    HostId = m.HostId,
                    HostMobile = host!.Mobile,
                    Title = m.Title,
                    PinCode = m.UserPinCode,
                    MasterPinCode = m.MasterPinCode,
                    URLOrAddress = "https://test.rayan.com",
                    StartTime = m.StartDateTime.GetTimeRightFormat(),
                    StartDate = m.StartDateTime.ToFarsi(),
                    AudienceMobile = audiences!,
                }).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id))!;
            }
            else
            {
                var audiences = await _context.AudienceFaceToFaces.Include(a => a.Audience).Where(a => a.FaceToFaceId == id).Select(_ => _.Audience!.Mobile).ToArrayAsync();

                return (await _context.FaceToFaces.Select(m => new SendStatusMeetingDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    URLOrAddress = m.Address,
                    StartTime = m.StartDateTime.GetTimeRightFormat(),
                    StartDate = m.StartDateTime.ToFarsi(),
                    AudienceMobile = audiences!,
                }).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id))!;
            }
        }
    }
}