using System;
using System.ComponentModel.DataAnnotations;

namespace Progetto_19._07.Models
{
    public class Verbale
    {
        public int IdVerbale { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataViolazione { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataTrascrizioneVerbale { get; set; }

        [Required]
        public string IndirizzoViolazione { get; set; }

        [Required]
        public string NominativoAgente { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Importo must be a positive number.")]
        public decimal Importo { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "DecurtamentoPunti must be a non-negative number.")]
        public int DecurtamentoPunti { get; set; }

        [Required]
        public int IdAnagrafica { get; set; }

        public Anagrafica Anagrafica { get; set; }

        [Required]
        public int IdViolazione { get; set; }

        public TipoViolazione TipoViolazione { get; set; }
    }
}
