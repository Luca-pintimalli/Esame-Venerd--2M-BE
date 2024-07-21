using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Progetto_19._07.Models;

namespace Progetto_19._07.Services
{
	public class VerbaleService : SqlServerServiceBase, IVerbaleService
	{
		public VerbaleService(IConfiguration config) : base(config)
        {
		}

        //CREAZIONE METODO PER AGGIUNGERE UN NUOVO VERBALE
        private Verbale Create(DbDataReader reader)
        {
            return new Verbale
            {
                IdVerbale = reader.GetInt32(0),
                DataViolazione = reader.GetDateTime(1), 
                IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                NominativoAgente = reader["NominativoAgente"].ToString(),
                DataTrascrizioneVerbale = reader.GetDateTime(4), 
                Importo = reader.GetDecimal(5), 
                DecurtamentoPunti = reader.GetInt32(6), 
                IdAnagrafica = reader.GetInt32(7), 
                IdViolazione = reader.GetInt32(8) 
            };
        }



        //Lista verbali
        public IEnumerable<Verbale> GetVerbali()
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT idverbale, DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, idanagrafica, idviolazione FROM VERBALE");

                //GESTIONE APERTURA/CHIUSURA CONNESIONE
                var conn = GetConnection();

                //APERTURA CONNESIONE
                conn.Open();

                //UTILIZZO COMANDO
                var reader = cmd.ExecuteReader();

                var list = new List<Verbale>();

                while (reader.Read())
                    list.Add(Create(reader));

                //CHIUSURA CONNESIONE
                conn.Close();

                return list;

            }catch(Exception e)
            {
                throw e;
            }
           
        }

        public void newVerbale(Verbale verbale)
        {
            if (verbale == null)
            {
                throw new ArgumentNullException(nameof(verbale));
            }

            try
            {
                // CREO COMANDO
                var cmd = GetCommand("INSERT INTO VERBALE (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IdAnagrafica, IdViolazione) VALUES (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IdAnagrafica, @IdViolazione)");

                cmd.Parameters.Add(new SqlParameter("@DataViolazione", SqlDbType.DateTime) { Value = verbale.DataViolazione });
                cmd.Parameters.Add(new SqlParameter("@IndirizzoViolazione", SqlDbType.NVarChar) { Value = verbale.IndirizzoViolazione ?? (object)DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@NominativoAgente", SqlDbType.NVarChar) { Value = verbale.NominativoAgente ?? (object)DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@DataTrascrizioneVerbale", SqlDbType.DateTime) { Value = verbale.DataTrascrizioneVerbale });
                cmd.Parameters.Add(new SqlParameter("@Importo", SqlDbType.Decimal) { Value = verbale.Importo });
                cmd.Parameters.Add(new SqlParameter("@DecurtamentoPunti", SqlDbType.Int) { Value = verbale.DecurtamentoPunti });
                cmd.Parameters.Add(new SqlParameter("@IdAnagrafica", SqlDbType.Int) { Value = verbale.IdAnagrafica });
                cmd.Parameters.Add(new SqlParameter("@IdViolazione", SqlDbType.Int) { Value = verbale.IdViolazione });

                // GESTIONE CONNESSIONE
                using (var conn = GetConnection())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error while inserting new verbale", e);
            }
        }

    }
}

