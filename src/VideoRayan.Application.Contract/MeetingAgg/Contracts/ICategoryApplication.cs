using System;
using Framework.Application;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
	public interface ICategoryApplication
	{
		Task<CategoryDto> GetBy(Guid id);
		Task<EditCategoryDto> GetDetailForEditBy(Guid id);
		Task<OperationResult> Edit(EditCategoryDto command);
		Task<OperationResult> Delete(Guid id,Guid customerId);
		Task<IEnumerable<CategoryDto>> GetAll(Guid customerId);
		Task<OperationResult> Create(CreateCategoryDto command);
	}
}