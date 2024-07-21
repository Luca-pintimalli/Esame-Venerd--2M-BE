using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var anagrafiche = _anagraficaService.GetTrasgressori()?.ToList();
            var tipoViolazioni = _tipoViolazioneService.GetViolazioni()?.ToList();

            if (anagrafiche == null || !anagrafiche.Any())
            {
                _logger.LogError("Anagrafiche not found or empty.");
                return NotFound("Anagrafiche not found.");
            }

            if (tipoViolazioni == null || !tipoViolazioni.Any())
            {
                _logger.LogError("TipoViolazioni not found or empty.");
                return NotFound("TipoViolazioni not found.");
            }

            var model = new NewVerbaleViewModel
            {
                Anagrafiche = anagrafiche.Select(a => new SelectListItem { Value = a.IdAnagrafica.ToString(), Text = $"{a.Nome} {a.Cognome}" }),
                TipoViolazioni = tipoViolazioni.Select(t => new SelectListItem { Value = t.idviolazione.ToString(), Text = t.descrizione }),
                DataViolazione = DateTime.Now.Date, // Imposta la data corrente come predefinita se necessario
                DataTrascrizioneVerbale = DateTime.Now.Date // Imposta la data corrente come predefinita se necessario
            };

            return View(model);
        }



        [HttpPost]
        public IActionResult NewVerbale(NewVerbaleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Mappa il ViewModel al modello
                    var verbale = new Verbale
                    {
                        DataViolazione = model.DataViolazione,
                        DataTrascrizioneVerbale = model.DataTrascrizioneVerbale,
                        IndirizzoViolazione = model.IndirizzoViolazione,
                        NominativoAgente = model.NominativoAgente,
                        Importo = model.Importo,
                        DecurtamentoPunti = model.DecurtamentoPunti,
                        IdAnagrafica = int.Parse(model.IdAnagrafica), // Assicurati di gestire correttamente l'ID
                        IdViolazione = int.Parse(model.IdViolazione) // Assicurati di gestire correttamente l'ID
                    };

                    _verbaleService.newVerbale(verbale);

                    // Reindirizza a una vista di conferma o alla lista dei verbali
                    return RedirectToAction("Verbale");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error while creating new verbale");
                    ModelState.AddModelError("", "An error occurred while creating the verbale. Please try again.");
                }
            }

            // Se ModelState non è valido o si verifica un errore, mostra il form con i dati inseriti
            var anagrafiche = _anagraficaService.GetTrasgressori()?.ToList();
            var tipoViolazioni = _tipoViolazioneService.GetViolazioni()?.ToList();

            model.Anagrafiche = anagrafiche?.Select(a => new SelectListItem { Value = a.IdAnagrafica.ToString(), Text = $"{a.Nome} {a.Cognome}" });
            model.TipoViolazioni = tipoViolazioni?.Select(t => new SelectListItem { Value = t.idviolazione.ToString(), Text = t.descrizione });

            return View(model);
        }



    }
}

