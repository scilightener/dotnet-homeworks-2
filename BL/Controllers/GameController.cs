using BL.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;

namespace BL.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController
{
    [HttpPost]
    [Route("fight")]
    public JsonResult Fight([FromBody] OpponentsDto opponents)
    {
        var battle = new Battle(opponents.Player, opponents.Monster);
        var result = battle.GetResult();
        return new JsonResult(result);
    }
}