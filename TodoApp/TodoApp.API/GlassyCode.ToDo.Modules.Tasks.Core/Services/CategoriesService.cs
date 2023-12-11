using GlassyCode.ToDo.Modules.Tasks.Core.DTOs;
using GlassyCode.ToDo.Modules.Tasks.Core.Entities;
using GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums;

namespace GlassyCode.ToDo.Modules.Tasks.Core.Services;

public class CategoriesService : ICategoriesService
{
    public CategoriesService(IMapper mapper, ICategoryRepository repository)
    {
            _mapper = mapper;
            _repository = repository;
        }
        
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _repository;

    private async Task<bool> CreateCategoryAsync(CategoryDto categoryDto)
    {
            var category = _mapper.Map<Category>(categoryDto);
            category.Id = Guid.NewGuid();

            return await _repository.CreateCategoryAsync(category);
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
            await _repository.ClearTableAsync();
            
            return new ServiceResponse();
        }
}