using Framework.Application;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;
using VideoRayan.Domain.MeetingAgg;
using VideoRayan.Domain.MeetingAgg.Repositories;

namespace VideoRayan.Application
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryApplication(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<(OperationResult, CategoryDto)> Create(CreateCategoryDto command)
        {
            OperationResult result = new();

            if (_categoryRepository.Exists(c => c.Title == command.Title && c.CustomerId == command.CustomerId)) return (result.Failed(ApplicationMessage.DuplicatedModel), default)!;

            var category = new Category(command.CustomerId, command.Title!, command.Description!);
            await _categoryRepository.AddEntityAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(category.Id));
        }

        public async Task<(OperationResult, CategoryDto)> Delete(Guid id, Guid customerId)
        {
            OperationResult result = new();

            var category = await _categoryRepository.GetEntityByIdAsync(id);

            var returnObj = await GetBy(id);

            if (category is null) return (result.Failed(ApplicationMessage.NotExist), default)!;
            if (category.CustomerId != customerId) return (result.Failed(ApplicationMessage.DoNotAccessToOtherAccount), default)!;

            category.Delete();
            await _categoryRepository.SaveChangesAsync();

            return (result.Succeeded(), returnObj);
        }

        public async Task<(OperationResult, CategoryDto)> Edit(EditCategoryDto command)
        {
            OperationResult result = new();

            var category = await _categoryRepository.GetEntityByIdAsync(command.Id);

            if (category is null) return (result.Failed(ApplicationMessage.NotExist), default)!;
            if (category.CustomerId != command.CustomerId) return (result.Failed(ApplicationMessage.DoNotAccessToOtherAccount), default)!;
            if (_categoryRepository.Exists(c => (c.Title == command.Title && c.CustomerId == command.CustomerId) && c.Id != command.Id))
                return (result.Failed(ApplicationMessage.DuplicatedModel), default)!;

            category.Edit(command.Title!, command.Description!);
            await _categoryRepository.SaveChangesAsync();

            return (result.Succeeded(), await GetBy(category.Id));
        }

        public async Task<IEnumerable<CategoryDto>> GetAll(Guid customerId) => await _categoryRepository.GetAll(customerId);

        public async Task<CategoryDto> GetBy(Guid id) => await _categoryRepository.GetBy(id);

        public async Task<EditCategoryDto> GetDetailForEditBy(Guid id) => await _categoryRepository.GetDetailForEditBy(id);
    }
}