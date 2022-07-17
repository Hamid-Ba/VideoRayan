using System;
using Framework.Application;

namespace VideoRayan.Application.Contract.CustomerAgg.Contracts
{
	public interface IAudienceApplication
	{
		Task<AudienceDto> GetBy(Guid id);
		Task<IEnumerable<AudienceDto>> GetAll(string categoryName);
		Task<EditAudienceDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditAudienceDto command);
		Task<OperationResult> Create(CreateAudienceDto command);
	}
}