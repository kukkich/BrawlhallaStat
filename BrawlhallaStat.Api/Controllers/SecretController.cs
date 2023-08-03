using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrawlhallaStat.Api.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
public class SecretController : ControllerBase
{
    [HttpGet]
    public ActionResult SimpleSecret()
    {
        return Ok("Авторизован");
    }

    [HttpGet]
    [Authorize("KukkichOnly")]
    public ActionResult KukkichSecret()
    {
        return Ok("Вы Kukkich!");
    }
}