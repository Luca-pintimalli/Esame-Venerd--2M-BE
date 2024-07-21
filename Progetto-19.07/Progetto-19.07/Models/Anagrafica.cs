using System;
using System.ComponentModel.DataAnnotations;

namespace Progetto_19._07.Models
{
	public class Anagrafica
	{
        public int IdAnagrafica { get; set; }

        [Required, Display(Name ="Cognome")]
        public string Cognome { get; set; }

        [Required, Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required, Display(Name = "Indirizzo")]
        public string Indirizzo { get; set; }

        [Required, Display(Name = "Città")]
        public string Citta { get; set; }

        [Required, Display(Name = "CAP")]
        public string CAP { get; set; }

        [Required, Display(Name = "Codice Fiscale")]
        public string Cod_Fisc { get; set; }
    }
}

