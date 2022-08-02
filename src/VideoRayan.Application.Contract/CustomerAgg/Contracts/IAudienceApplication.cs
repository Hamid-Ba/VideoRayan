using Framework.Application;

namespace VideoRayan.Application.Contract.CustomerAgg.Contracts
{
    public interface IAudienceApplication
	{
		Task<AudienceDto> GetBy(Guid id);
		Task<EditAudienceDto> GetDetailForEditBy(Guid id);
		Task<(OperationResult, AudienceDto)> Edit(EditAudienceDto command);
		Task<(OperationResult, AudienceDto)> Delete(Guid id,Guid customerId);
		Task<(OperationResult,AudienceDto)> Create(CreateAudienceDto command);
		Task<IEnumerable<AudienceDto>> GetAll(Guid customerId,string categoryName);
	}
}