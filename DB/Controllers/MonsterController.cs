using Microsoft.AspNetCore.Mvc;

namespace DB.Controllers;

[ApiController]
[Route("[controller]")]
public class MonsterController
{
    private readonly AppDbContext _db;
    
    public MonsterController(AppDbContext db) => _db = db;
    
    [HttpGet]
    [Route("getRandom")]
    public JsonResult GetRandom()
    {
        var total = _db.Monsters.Count();
        var rnd = new Random();
        var toSkip = rnd.Next(total);
        return new JsonResult(_db.Monsters.ElementAt(toSkip));
    }
}