using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;

namespace UI.Pages;

public class Game : PageModel
{
    private const string DbUrl = "https://localhost:7277/monster/getRandom";
    private const string BlUrl = "https://localhost:.../...";
    
    [BindProperty] public Player Player { get; set; }
    
    public void OnGet() { }

    public async Task OnPostAsync()
    {
        if (!ModelState.IsValid) return;
        using var client = new HttpClient();
        var monster = await client.GetFromJsonAsync<Monster>(DbUrl);
        
    }
}