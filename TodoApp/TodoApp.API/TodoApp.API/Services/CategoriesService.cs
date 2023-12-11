using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Interfaces;
using TodoApp.API.Models.Category;

namespace TodoApp.API.Services;

public class CategoriesService(IMapper mapper, ICategoryRepository repository) : ICategoriesService
{
    private async Task<bool> CreateCategoryAsync(CategoryDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);
        category.Id = Guid.NewGuid();

        return await repository.CreateCategoryAsync(category);
    }

    public async Task<ServiceResponse> CreateAllCategoriesByTypesAsync()
    {
        foreach (CategoryType type in Enum.GetValues(typeof(CategoryType)))
        {
            var categoryDto = new CategoryDto
            {
                Type = type
            };

            var successfullyAdd = await CreateCategoryAsync(categoryDto);
                
            if (!successfullyAdd)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Couldn't add category!"
                };
            }
        }

        return new ServiceResponse();
    }

    public async Task<ServiceResponse> DeleteAllCategoriesAsync()
    {
        await repository.ClearTableAsync();
            
        return new ServiceResponse();
    }
}