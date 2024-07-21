using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class NewVerbaleViewModel
{

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime DataViolazione { get; set; }

    public string IndirizzoViolazione { get; set; }
    public string NominativoAgente { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime DataTrascrizioneVerbale { get; set; }

    public decimal Importo { get; set; }
    public int DecurtamentoPunti { get; set; }

    public IEnumerable<SelectListItem> Anagrafiche { get; set; }
    public IEnumerable<SelectListItem> TipoViolazioni { get; set; }

    public string IdAnagrafica { get; set; }
    public string IdViolazione { get; set; }
}

