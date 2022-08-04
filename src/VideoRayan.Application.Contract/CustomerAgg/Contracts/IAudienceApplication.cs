using Framework.Application;

namespace VideoRayan.Application.Contract.CustomerAgg.Contracts
{
    public interface IAudienceApplication
	{
		Task<AudienceDto> GetBy(Guid id);
		Task<EditAudienceDto> GetDetailForEditBy(Guid id);
		Task<IEnumerable<AudienceDto>> GetAll(SearchAudienceDto filter);
		Task<(OperationResult, AudienceDto)> Edit(EditAudienceDto command);
		Task<(OperationResult, AudienceDto)> Delete(Guid id,Guid customerId);
		Task<(OperationResult,AudienceDto)> Create(CreateAudienceDto command);
	}
}