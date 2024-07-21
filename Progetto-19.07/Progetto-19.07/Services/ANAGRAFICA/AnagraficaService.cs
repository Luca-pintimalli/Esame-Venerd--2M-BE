
using System.Data.Common;
using System.Data.SqlClient;
using Progetto_19._07.Models;
using Progetto_19._07.Services;

public class AnagraficaService : SqlServerServiceBase, IAnagraficaService
{

    public AnagraficaService(IConfiguration config) : base(config)
    {
    }


    //METODO PER L'ELIMINAZIONE DI UN TRASGRESSORE , TRAMITE ID 
    public void DeleteTrasgressore(int id)
    {
        try
        {
            //Comando Delete
            var query = "DELETE From ANAGRAFICA WHERE idanagrafica = @idanagrafica";

            //recupero comando e inserisco il comando delete come parametro
            var cmd = GetCommand(query);

            //Aggiungo valore per parametro
            cmd.Parameters.Add(new SqlParameter("@idanagrafica", id));

            //GESTIONE APERTURA/CHIUSURA CONNESIONE
            var conn = GetConnection();

            //Apertura connesione 
            conn.Open();

            //Eseguo il comando
            int result = cmd.ExecuteNonQuery(); //ExecuteNonQuery è un comando di modifica , e restituisce il numero(int) totale di righe convolte nel operazioneù

            //Chiuura connesione
            conn.Close();

        }
        catch(Exception e)
        {
            throw e;

        }

    }


    //CREZIONE METODO CREATE PER AGGIUNGERE UN NUOVO TRASGRESSORE
    private Anagrafica Create(DbDataReader reader)
    {
        return new Anagrafica
        {
            IdAnagrafica = reader.GetInt32(0),
            Nome = reader["Nome"].ToString(),
            Cognome = reader["Cognome"].ToString(),
            Cod_Fisc = reader["Cod_Fisc"].ToString(),
            Indirizzo = reader["Indirizzo"].ToString(),
            Citta = reader["Citta"].ToString(),
            CAP = reader["CAP"].ToString()


        };
    }


    //METODO PER RICEVERE IL SINGOLO TRASGRESSORE TRAMITE ID 
    public Anagrafica GetTrasgressore(int id)
    {
        try{

         //CREO COMANDO
         var cmd = GetCommand("SELECT IdAnagrafica, Nome, Cognome, Cod_Fisc, Indirizzo, Citta , CAP FROM ANAGRAFICA WHERE idanagrafica = @idanagrafica");

         //COMANDO APERTURA/CHIUSURA CONNESIONE
         var conn = GetConnection();

         //APRO CONNESIONE
         conn.Open();

            //ESEGUO COMANDO
            var reader = cmd.ExecuteReader();
                if (reader.Read())
                return Create(reader);
            throw new Exception("Non trovato");


        }
        catch(Exception e)
        {
            throw e;

        }
    }


    //METODO RECUPERO TRASGRESSORI
    public IEnumerable<Anagrafica> GetTrasgressori()
    {
        try
        {
            //CREO COMANDO
            var cmd = GetCommand("SELECT IdAnagrafica, Nome, Cognome, Cod_Fisc, Indirizzo, Citta , CAP FROM ANAGRAFICA");

            //COMANDO APERTURA/CHIUSURA CONNESIONE
            var conn = GetConnection();

            //APRO CONNESIONE
            conn.Open();

            //ESEGUO COMANDO
            var reader = cmd.ExecuteReader();

            var list = new List<Anagrafica>();

            while (reader.Read())
                list.Add(Create(reader));

            //CHIUSURA CONNESIONE
            conn.Close();

            return list;

        }
        catch(Exception e)
        {
            throw e;

        }
    }


    //METODO PER LA CREAZIONE DI UN NUOVO TRASGRESSORE
    public void newTrasgressore(Anagrafica anagrafica)
    {
        try
        {
            //CREO COMANDO
            var cmd = GetCommand("INSERT INTO ANAGRAFICA( Nome, Cognome, Cod_Fisc, Indirizzo, Citta, CAP)  VALUES(@Nome, @Cognome, @Cod_Fisc, @Indirizzo, @Citta , @CAP) ");
            cmd.Parameters.Add(new SqlParameter("@Nome", anagrafica.Nome));
            cmd.Parameters.Add(new SqlParameter("@Cognome", anagrafica.Cognome));
            cmd.Parameters.Add(new SqlParameter("@Cod_Fisc", anagrafica.Cod_Fisc));
            cmd.Parameters.Add(new SqlParameter("@Indirizzo", anagrafica.Indirizzo));
            cmd.Parameters.Add(new SqlParameter("@Citta", anagrafica.Citta));
            cmd.Parameters.Add(new SqlParameter("@CAP", anagrafica.CAP));

            //GESTIONE CONNESIONE
            var conn = GetConnection();

            //APRO CONNESIONE
            conn.Open();

            var result = cmd.ExecuteNonQuery();

            //CHIUSURA CONNESIONE
            conn.Close();

        }
        catch(Exception e)
        {
            throw e;

        }
    }

    public void UpdateTrasgressore(int id, Anagrafica anagrafica)
    {
        throw new NotImplementedException();
    }
}