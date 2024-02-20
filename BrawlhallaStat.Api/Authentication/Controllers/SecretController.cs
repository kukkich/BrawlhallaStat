using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Authentication.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
public class SecretController : ControllerBase
{
    [HttpGet]
    public ActionResult SimpleSecret()
    {
        return Ok("Авторизован");
    }
}