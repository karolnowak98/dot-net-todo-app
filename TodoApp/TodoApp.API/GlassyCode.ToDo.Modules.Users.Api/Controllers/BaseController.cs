using Microsoft.AspNetCore.Mvc;

namespace GlassyCode.ToDo.Modules.Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
internal abstract class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is not null)
        {
            return Ok(model);
        }

        return NotFound();
    }
}