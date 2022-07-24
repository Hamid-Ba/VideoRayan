using System;
using Framework.Application;

namespace VideoRayan.Application.Contract.CustomerAgg.Contracts
{
	public interface IAudienceApplication
	{
		Task<AudienceDto> GetBy(Guid id);
		Task<EditAudienceDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditAudienceDto command);
		Task<OperationResult> Create(CreateAudienceDto command);
		Task<IEnumerable<AudienceDto>> GetAll(Guid customerId,string categoryName);
	}
}