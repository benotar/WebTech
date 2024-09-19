using Microsoft.AspNetCore.Mvc;

namespace WebTech.WebApi.Controllers;

[Host("bg-local.com")]
public class UsersController : BaseController
{
    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        return Ok("Hello from bg-local.com");
    }
}