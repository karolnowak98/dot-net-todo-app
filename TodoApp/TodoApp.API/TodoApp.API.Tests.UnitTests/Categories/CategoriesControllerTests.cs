using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using TodoApp.API.Controllers;
using TodoApp.API.DTOs;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Tests.UnitTests.Categories;

public class CategoriesControllerTests
{
    [Fact]
    public async Task CreateCategories_Success()
    {
        var categoriesService = new Mock<ICategoriesService>();
        categoriesService.Setup(service => service.CreateAllCategoriesByTypesAsync())
            .ReturnsAsync(new ServiceResponse { Success = true });

        var controller = new CategoriesController(categoriesService.Object);
        var result = await controller.CreateCategories();
        var okResult = Assert.IsType<OkObjectResult>(result);
        var serviceResponse = Assert.IsType<ServiceResponse>(okResult.Value);
        
        Assert.True(serviceResponse.Success);
    }

    [Fact]
    public async Task CreateCategories_Failed()
    {
        var categoriesService = new Mock<ICategoriesService>();
        categoriesService.Setup(service => service.CreateAllCategoriesByTypesAsync())
            .ReturnsAsync(new ServiceResponse { Success = false });

        var controller = new CategoriesController(categoriesService.Object);
        var result = await controller.CreateCategories();
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var serviceResponse = Assert.IsType<ServiceResponse>(badRequestResult.Value);
        
        Assert.False(serviceResponse.Success);
    }
}