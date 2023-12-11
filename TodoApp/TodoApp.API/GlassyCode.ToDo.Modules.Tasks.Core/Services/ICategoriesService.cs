namespace GlassyCode.ToDo.Modules.Tasks.Core.Services;

public interface ICategoriesService
{
    public Task<ServiceResponse> CreateAllCategoriesByTypesAsync();
    public Task<ServiceResponse> DeleteAllCategoriesAsync();
}