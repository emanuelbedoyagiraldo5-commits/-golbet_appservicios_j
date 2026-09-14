using GolBet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GolBet.Web.Controllers;

public class MatchesController : Controller
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    // Cartelera: todos los partidos, sin filtro de estado
    public async Task<IActionResult> Index()
    {
        var matches = await _matchService.GetAllAsync();
        return View(matches);
    }
}
