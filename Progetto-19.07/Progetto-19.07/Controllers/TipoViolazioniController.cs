using System;
using Microsoft.AspNetCore.Mvc;
using Progetto_19._07.Models;
using Progetto_19._07.Services;

namespace Progetto_19._07.Controllers
{
	public class TipoViolazioniController : Controller
	{
		private readonly ILogger<AnagraficaController> _logger;
		private readonly ITipoViolazioneService _tipoViolazione;

		public TipoViolazioniController(ILogger<AnagraficaController> logger, ITipoViolazioneService tipoViolazione)
		{
			_logger = logger;
			_tipoViolazione = tipoViolazione;
		}

		//PAGINA CON TUTTI I TIPI DI VIOLAZIONI
		[HttpGet]
		public IActionResult TipoViolazione()
		{
			var tipoViolazione = _tipoViolazione.GetViolazioni();
			return View(tipoViolazione);
		}

        //AGGIUNTA NUOVA VIOLAZIONE
        [HttpGet]
        public IActionResult NewViolazione()
        {
            return View(new TipoViolazione());
        }

        [HttpPost]
        public IActionResult NewViolazione(TipoViolazione model)
        {
            if (ModelState.IsValid)
            {
                _tipoViolazione.newTipoViolazione(model);
                return RedirectToAction("TipoViolazione");
            }
            return View(model);
        }





    }
}

