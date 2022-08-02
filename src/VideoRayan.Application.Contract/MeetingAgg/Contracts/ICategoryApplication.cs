using Framework.Application;

namespace VideoRayan.Application.Contract.MeetingAgg.Contracts
{
    public interface ICategoryApplication
	{
		Task<CategoryDto> GetBy(Guid id);
		Task<EditCategoryDto> GetDetailForEditBy(Guid id);
		Task<IEnumerable<CategoryDto>> GetAll(Guid customerId);
		Task<(OperationResult,CategoryDto)> Edit(EditCategoryDto command);
		Task<(OperationResult,CategoryDto)> Delete(Guid id,Guid customerId);
		Task<(OperationResult,CategoryDto)> Create(CreateCategoryDto command);
	}
}