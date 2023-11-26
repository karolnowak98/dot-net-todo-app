namespace TodoApp.API.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task<ServiceResponse> CreateAllCategoriesByTypesAsync();
        public Task<ServiceResponse> DeleteAllCategoriesAsync();
    }
}