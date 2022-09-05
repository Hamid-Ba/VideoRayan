using Framework.Application.Enums;
using Framework.Domain;
using VideoRayan.Application.Contract.CustomerAgg;

namespace VideoRayan.Domain.CustomerAgg.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
	{
        Task<string> GetPhone(Guid id);
        Task<CustomerDto> GetBy(Guid id);
		Task<Customer> GetBy(string mobile);
        Task<CustomerType> GetTypeBy(Guid id);
        Task<EditCustomerDto> GetDetailForEditBy(Guid id);
        Task<IEnumerable<CustomerDto>> GetAll(CustomerType type);
        Task<EditLogoCustomerDto> GetDetailForEditLogoBy(Guid id);
        Task<EditByAdminCustomerDto> GetDetailForEditByAdmin(Guid id);
        Task<IEnumerable<CustomerMeetsListDto>> GetMeetingList(Guid id);
    }
}