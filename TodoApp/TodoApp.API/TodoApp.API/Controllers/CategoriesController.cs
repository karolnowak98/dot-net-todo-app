using TodoApp.API.Interfaces;

namespace TodoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoriesService service) : ControllerBase
{
    [HttpPost("create-categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCategories()
    {
        var response = await service.CreateAllCategoriesByTypesAsync();

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("delete-categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteCategories()
    {
        var response = await service.DeleteAllCategoriesAsync();

        return response.Success ? Ok(response) : BadRequest(response);
    }
}