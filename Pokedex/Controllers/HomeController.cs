using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;


namespace Pokedex.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {   List<Pokemon> Pokemons = new List<Pokemon>();
    using (StreamReader leitor = new("Data\\pokemons.json"))
        {
            string dados = leitor.ReadToEnd();
            Pokemons = JsonSerializer.Deserialize<List<Pokemon>>(dados);
        }
        List<Tipo> tipos = new();
        using (StreamReader leitor = new("Data\\tipos.json"))
        {
            string dados = leitor.ReadToEnd();
            tipos = JsonSerializer.Deserialize<List<Tipo>>(dados);
        }
        ViewData ["Tipos"] = tipos;
        return View(Pokemons);
    }

        public IActionResult Details (int Id)
        {   
            List<Pokemon> pokemons = new ();
            using (StreamReader leitor = new ("Data\\pokemons.json"))
            {
                string dados = leitor.ReadToEnd();
                pokemons = JsonSerializer.Deserialize<List<Pokemon>>(dados);
            }
            List<Tipo> tipos = new();
            using (StreamReader leitor = new ("Data\\tipos.json"))
            {
                string dados = leitor.ReadToEnd();
                tipos = JsonSerializer.Deserialize<List<Tipo>>(dados);
            }
            DetailsVM details = new()
            {
                Tipos = tipos,
                Atual = pokemons.FirstOrDefault(p => p.Numero == Id),
                Anterior = pokemons.OrderByDescending(p => p.Numero).FirstOrDefault(p => p.Numero == Id),
                Proximo = pokemons.OrderBy(p => p.Numero).FirstOrDefault(p => p.Numero == Id),
            };
            var pokemon = pokemons
                .Where (p => p.Numero == Id)
                .FirstOrDefault();
            return View (details);
        }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
