using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Progetto_19._07.Models;
using Progetto_19._07.Services;

namespace Progetto_19._07.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private object _verbaleService;
    private readonly ITotalePunti _totalePuntiService;
    private readonly IVerbaleSuperiorePunti _verbaleSuperiorePuntiService;
    private readonly IVerbaliSuperiore400EuroService _verbaliSuperiore400EuroService;




    public HomeController(ILogger<HomeController> logger, ITotalePunti totalePuntiService, IVerbaleSuperiorePunti verbaleSuperiorePuntiService, IVerbaliSuperiore400EuroService verbaliSuperiore400EuroService)
    {
        _logger = logger;
        _totalePuntiService = totalePuntiService;
        _verbaleSuperiorePuntiService = verbaleSuperiorePuntiService;
        _verbaliSuperiore400EuroService = verbaliSuperiore400EuroService; 

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }


    //TOTALE PUNTI DECURTATI 
    [HttpGet]
    public IActionResult TotalePuntiDecurtati()
    {
        var totalePunti = _totalePuntiService.GetTotalePunti();
        return View(totalePunti);
    }

    //violazioni che superano i 10 punti. IN QUESTO MOMENTO NON CE NESSUN VERBALE CHE TOGLIE 10 PUNTI 
    [HttpGet]
    public IActionResult VerbaliSuperiore10Punti()
    {
        var verbali = _verbaleSuperiorePuntiService.GetVerbaliSuperiore10Punti();
        return View(verbali);
    }


    //VIOLAZIONI > 400€ IN QUESTO MOMENTO NON HO VERBALI SUPERIRORI A 400€
    [HttpGet]
    public IActionResult VerbaliSuperiore400Euro()
    {
        var verbali = _verbaliSuperiore400EuroService.GetVerbaliSuperiore400Euro();
        return View(verbali);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

