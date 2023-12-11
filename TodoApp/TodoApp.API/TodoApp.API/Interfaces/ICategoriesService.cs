using TodoApp.API.DTOs;

namespace TodoApp.API.Interfaces;

public interface ICategoriesService
{
    public Task<ServiceResponse> CreateAllCategoriesByTypesAsync();
    public Task<ServiceResponse> DeleteAllCategoriesAsync();
}