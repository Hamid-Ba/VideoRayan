using Framework.Application;
using Framework.Application.Enums;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.CustomerAgg.Contracts;
using VideoRayan.Domain.MeetingAgg;

namespace VideoRayan.Infrastructure.EfCore.Repositories.CustomerAgg
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly HttpContext _current;
        private readonly VideoRayanContext _context;

        public CustomerRepository(VideoRayanContext context, IHttpContextAccessor accessor) : base(context)
        {
            _context = context;
            _current = accessor.HttpContext!;
        }

        public async Task<IEnumerable<CustomerDto>> GetAll(CustomerType type) => await _context.Customers.Where(c => c.Type == type).Select(c => new CustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Logo = c.Logo,
            Image = c.Image,
            Mobile = c.Mobile,
            Type = c.Type,
            PersianCreationDate = c.CreationDate.ToFarsi(),
            IsActive = c.IsActive
        }).AsNoTracking().ToListAsync();

        public async Task<Customer> GetBy(string mobile) => (await _context.Customers.FirstOrDefaultAsync(c => c.Mobile == mobile))!;

        public async Task<CustomerDto> GetBy(Guid id)
        {
            var result = (await _context.Customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Mobile = c.Mobile,
                Email = c.Email,
                Title = c.Title,
                Type = c.Type,
                PhoneCode = c.PhoneCode,
                PersianCreationDate = c.CreationDate.ToFarsi(),
                Logo = c.Logo,
                Image = c.Image
            }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

            if (!string.IsNullOrWhiteSpace(result.Logo)) result.Logo = $"{_current.Request.Scheme}://{_current.Request.Host}{_current.Request.PathBase}/Pictures//{result.Logo}";
            if (!string.IsNullOrWhiteSpace(result.Image)) result.Image = $"{_current.Request.Scheme}://{_current.Request.Host}{_current.Request.PathBase}/Pictures//{result.Image}";

            return result;
        }

        public async Task<EditCustomerDto> GetDetailForEditBy(Guid id) => (await _context.Customers.Select(c => new EditCustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Mobile = c.Mobile,
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

        public async Task<EditByAdminCustomerDto> GetDetailForEditByAdmin(Guid id) => (await _context.Customers.Select(c => new EditByAdminCustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Mobile = c.Mobile,
            Logo = c.Logo,
            ImageName = c.Image,
            Title = c.Title,
            Type = c.Type
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

        public async Task<EditLogoCustomerDto> GetDetailForEditLogoBy(Guid id) => (await _context.Customers.Select(c => new EditLogoCustomerDto
        {
            Id = c.Id,
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

        public async Task<IEnumerable<CustomerMeetsListDto>> GetMeetingList(Guid id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                
                var audiencesWhoThisCustomerIs = await _context.Audiences.Where(a => a.Mobile == customer!.Mobile)
                    .Include(m => m.Meetings!)
                    .ThenInclude(m => m.Meeting)
                    .Include(m => m.Meetings!)
                    .ThenInclude(m => m.Audience)
                    .Include(f => f.FaceToFaces!)
                    .ThenInclude(a => a.FaceToFace)
                    .Include(f => f.FaceToFaces!)
                    .ThenInclude(a => a.Audience)
                    .Select(_ => new
                    {
                        _.Meetings,
                        _.FaceToFaces,
                    }).ToListAsync();

                var result = new List<CustomerMeetsListDto>();

                foreach(var audience in audiencesWhoThisCustomerIs)
                {
                    foreach(var meeting in audience.Meetings!)
                    {
                        var item = new CustomerMeetsListDto()
                        {
                            MeetId = meeting.MeetingId,
                            CustomerId = meeting.AudienceId,
                            MeetingType = "مجازی",
                            Phone = meeting.Audience!.Mobile,
                            Title = meeting.Meeting!.Title,
                        };

                        if (meeting.Meeting.HostId == item.CustomerId) item.Password = meeting.Meeting.MasterPinCode;
                        else item.Password = meeting.Meeting.UserPinCode;

                        result.Add(item);
                    }

                    foreach(var faceToFace in audience.FaceToFaces!)
                    {
                        var item = new CustomerMeetsListDto()
                        {
                            MeetId = faceToFace.FaceToFaceId,
                            CustomerId = faceToFace.AudienceId,
                            MeetingType = "حضوری",
                            Phone = faceToFace.Audience!.Mobile,
                            Title = faceToFace.FaceToFace!.Title,
                        };

                        result.Add(item);
                    }
                }

                return result;
            }
            catch { return Enumerable.Empty<CustomerMeetsListDto>(); }
        }

        public async Task<string> GetPhone(Guid id) => (await _context.Customers.FindAsync(id))!.Mobile!;

        public async Task<CustomerType> GetTypeBy(Guid id) => (await _context.Customers.FindAsync(id))!.Type;
    }
}