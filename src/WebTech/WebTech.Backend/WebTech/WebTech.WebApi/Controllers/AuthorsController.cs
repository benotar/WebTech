using Microsoft.AspNetCore.Mvc;

namespace WebTech.WebApi.Controllers;

[Host("api.bg-local.net")]
public class AuthorsController: BaseController
{
    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        return Ok("Hello from api.bg-local.net");
    }
} 