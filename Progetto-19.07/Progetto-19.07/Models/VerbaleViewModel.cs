public class VerbaleViewModel
{
    public int IdVerbale { get; set; }
    public DateTime DataViolazione { get; set; }
    public string IndirizzoViolazione { get; set; }
    public string NominativoAgente { get; set; }
    public DateTime DataTrascrizioneVerbale { get; set; }
    public decimal Importo { get; set; }
    public int DecurtamentoPunti { get; set; }
    public string NomeAnagrafica { get; set; } // Nome dell'anagrafica
    public string NomeViolazione { get; set; } // Nome della violazione
}
