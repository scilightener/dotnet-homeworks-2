using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Models.Dtos;

namespace UI.Pages;

public class Game : PageModel
{
    private const string DbUrl = "https://localhost:7277/monster/getRandom";
    private const string BlUrl = "https://localhost:7231/game/fight";

    [BindProperty] public Player Player { get; set; } = new();
    public ResultDto? Result;
    
    public void OnGet() { }

    public async Task OnPostAsync()
    {
        if (!ModelState.IsValid) return;
        using var client = new HttpClient();
        var monster = await client.GetFromJsonAsync<Monster>(DbUrl);
        var result = await client.PostAsJsonAsync(BlUrl, new OpponentsDto(Player, monster!));
        Result = await result.Content.ReadFromJsonAsync<ResultDto>();
    }
}