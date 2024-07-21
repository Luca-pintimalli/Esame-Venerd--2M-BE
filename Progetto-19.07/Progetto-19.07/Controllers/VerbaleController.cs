using System;
using Microsoft.AspNetCore.Mvc;
using Progetto_19._07.Models;
using Progetto_19._07.Services;

namespace Progetto_19._07.Controllers
{
	public class VerbaleController : Controller
    {
        private readonly ILogger<VerbaleController> _logger;
        private readonly IVerbaleService _verbaleService;
        private readonly IAnagraficaService _anagraficaService;
        private readonly ITipoViolazioneService _tipoViolazioneService;

        public VerbaleController(ILogger<VerbaleController> logger, IVerbaleService verbaleService, IAnagraficaService anagraficaService, ITipoViolazioneService tipoViolazioneService)
        {
            _logger = logger;
            _verbaleService = verbaleService;
            _anagraficaService = anagraficaService;
            _tipoViolazioneService = tipoViolazioneService;
        }


        //PAGINA CON TUTTI I VERBALI 
        [HttpGet]
        public IActionResult Verbale()
        {
            var verbali = _verbaleService.GetVerbali();
            return View(verbali);
        }



        [HttpGet]
        public IActionResult NewVerbale()
        {
            var model = new Verbale
            {
                DataViolazione = DateTime.Now,
                DataTrascrizioneVerbale = DateTime.Now
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult NewVerbale(Verbale model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _verbaleService.newVerbale(model);
                    return RedirectToAction("Verbale");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while creating new verbale");
                    ModelState.AddModelError("", "An error occurred while creating the verbale. Please try again.");
                }
            }
            return View(model);
        }


    }

}

