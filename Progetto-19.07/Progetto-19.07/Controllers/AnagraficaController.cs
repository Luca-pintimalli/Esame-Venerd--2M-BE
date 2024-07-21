using System;
using Microsoft.AspNetCore.Mvc;
using Progetto_19._07.Models;
using Progetto_19._07.Services;

namespace Progetto_19._07.Controllers
{
	public class AnagraficaController : Controller
    {
		private readonly ILogger<AnagraficaController> _logger;
		private readonly IAnagraficaService _anagraficaService;

		public AnagraficaController(ILogger<AnagraficaController> logger, IAnagraficaService anagraficaService)
		{
			_logger = logger;
			_anagraficaService = anagraficaService;
		}

		//Pagina con tutti i trasgressori
        [HttpGet]
        public IActionResult Anagrafica()
		{
            var trasgressori = _anagraficaService.GetTrasgressori();
            return View(trasgressori);
        }

        //Aggiunta nuovo trasgressore
        [HttpGet]
        public IActionResult NewAnagrafica()
        {
            return View(new Anagrafica());
        }

        [HttpPost]
        public IActionResult NewAnagrafica(Anagrafica model)
        {
            if (ModelState.IsValid)
            {
                _anagraficaService.newTrasgressore(model);
                return RedirectToAction("Anagrafica");
            }
            return View(model);
        }
    }
}

