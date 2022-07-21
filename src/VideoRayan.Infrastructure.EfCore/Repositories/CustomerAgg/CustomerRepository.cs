using Framework.Application;
using Framework.Application.Enums;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Domain.CustomerAgg;
using VideoRayan.Domain.CustomerAgg.Contracts;

namespace VideoRayan.Infrastructure.EfCore.Repositories.CustomerAgg
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly VideoRayanContext _context;

        public CustomerRepository(VideoRayanContext context) : base(context) => _context = context;

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

        public async Task<CustomerDto> GetBy(Guid id) => (await _context.Customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Mobile = c.Mobile,
            Email = c.Email,
            Title = c.Title,
            Type = c.Type,
            PersianCreationDate = c.CreationDate.ToFarsi(),
        }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id))!;

        public async Task<EditCustomerDto> GetDetailForEditBy(Guid id) => (await _context.Customers.Select(c => new EditCustomerDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Mobile = c.Mobile,
            Logo = c.Logo
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

    }
}