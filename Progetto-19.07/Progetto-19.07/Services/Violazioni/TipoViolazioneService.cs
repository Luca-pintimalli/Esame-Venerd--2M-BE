
using System.Data.Common;
using System.Data.SqlClient;
using Progetto_19._07.Models;
using Progetto_19._07.Services;

namespace Progetto_19._07.Services
{
    public class TipoViolazioneService : SqlServerServiceBase, ITipoViolazioneService
	{
		public TipoViolazioneService(IConfiguration config) : base(config)
        {
		}

        //CREAZIONE METODO CREATE PER AGGIUNGERE UN NUOVO TIPO DI VIOLAZIONE
        private TipoViolazione Create(DbDataReader reader)
        {
            return new TipoViolazione
            {
                idviolazione = reader.GetInt32(0),
                descrizione = reader["descrizione"].ToString()

            };
        }


        //METODO RECUPERO NUOVO TIPO DI VIOLAZIONE
        public IEnumerable<TipoViolazione> GetViolazioni()
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT idviolazione , descrizione FROM TIPO_VIOLAZIONE");

                //COMANDO APERTURA/CHIUSURA
                var conn = GetConnection();

                //COMANDO APERTURA
                conn.Open();

                //ESEGUO COMANDO
                var reader = cmd.ExecuteReader();

                var list = new List<TipoViolazione>();

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

        public void newTipoViolazione(TipoViolazione tipoViolazione)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("INSERT INTO TIPO_VIOLAZIONE( descrizione) VALUES(@descrizione)");

                cmd.Parameters.Add(new SqlParameter("@descrizione", tipoViolazione.descrizione));

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

      
    }
}

