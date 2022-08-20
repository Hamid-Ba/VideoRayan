using Framework.Application;
using Framework.Application.Enums;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.CustomerAgg.Contracts;

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
                Logo = c.Logo
            }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

            if (!string.IsNullOrWhiteSpace(result.Logo)) result.Logo = $"{_current.Request.Scheme}://{_current.Request.Host}{_current.Request.PathBase}/Pictures//{result.Logo}";

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
            Title = c.Title,
            Type = c.Type
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

        public async Task<EditLogoCustomerDto> GetDetailForEditLogoBy(Guid id) => (await _context.Customers.Select(c => new EditLogoCustomerDto
        {
            Id = c.Id,
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

        public async Task<string> GetPhone(Guid id) => (await _context.Customers.FindAsync(id))!.Mobile!;

        public async Task<CustomerType> GetTypeBy(Guid id) => (await _context.Customers.FindAsync(id))!.Type;
    }
}