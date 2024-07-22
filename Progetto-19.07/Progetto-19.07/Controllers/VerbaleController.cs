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



        //CREAZIONE NUOVO VERBALE
        //HO AVUTO DEI PROBLEMI CON LA CREAZIONE DEL NUOVO VERBALE , PROBABILMENTE PERCHE NELLA MIA TABELLA VERBALE HO SOLO LA COLONNA IDANAGRAFICA E ID VIOLAZIONE , E NON NOME ANAGRAFICA O TIPO VIOLAZIONE ? 
        //HO PROVATO LO STESSO A CREARE UN NUOVO VERBALE SCRIVENDO IL NOME DEL TRASGRESSORE E NON L'ID (CON L'ID PROBABILMENTE SAREI RIUSCITO , MA NON MI PIACEVA FARE UN NUOVO VERBALE INSERENDO SOLO L'ID ANAGRAFICA )NON CI  SONO RIUSCITO , HO PROVATO ANCHE USANDO CHATGPT MA NON SONO RIUSCITO A CREARE UN NUOVO VERBALE :( 
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
                DataViolazione = DateTime.Now.Date, // Imposta la data corrente 
                DataTrascrizioneVerbale = DateTime.Now.Date // Imposta la data corrente come predefinita 
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
                        IdAnagrafica = int.Parse(model.IdAnagrafica),
                        IdViolazione = int.Parse(model.IdViolazione) 
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

