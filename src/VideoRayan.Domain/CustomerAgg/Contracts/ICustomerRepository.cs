using Framework.Application.Enums;
using Framework.Domain;
using VideoRayan.Application.Contract.CustomerAgg;

namespace VideoRayan.Domain.CustomerAgg.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
	{
		Task<Customer> GetBy(string mobile);
        Task<EditCustomerDto> GetDetailForEditBy(Guid id);
        Task<EditLogoCustomerDto> GetDetailForEditLogoBy(Guid id);
        Task<EditByAdminCustomerDto> GetDetailForEditByAdmin(Guid id);
        Task<IEnumerable<CustomerDto>> GetAll(CustomerType type);
        Task<CustomerDto> GetBy(Guid id);
        Task<string> GetPhone(Guid id);
        Task<CustomerType> GetTypeBy(Guid id);
    }
}