using BL.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;

namespace BL.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController
{
    private IBattle _battle;

    public GameController(IBattle battle) => _battle = battle;

    [HttpPost]
    [Route("fight")]
    public JsonResult Fight([FromBody] OpponentsDto opponents)
    {
        _battle = new Battle(opponents.Player, opponents.Monster);
        var result = _battle.GetResult();
        return new JsonResult(result);
    }
}