namespace GlassyCode.ToDo.Modules.Tasks.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _service;

    public CategoriesController(ICategoriesService service)
    {
            _service = service;
        }

    [HttpPost("create-categories")]
    public async Task<IActionResult> CreateCategories()
    {
            var response = await _service.CreateAllCategoriesByTypesAsync();

            return response.Success ? Ok(response) : BadRequest(response);
        }

    [HttpDelete("delete-categories")]
    public async Task<IActionResult> DeleteCategories()
    {
            var response = await _service.DeleteAllCategoriesAsync();

            return response.Success ? Ok(response) : BadRequest(response);
        }
}