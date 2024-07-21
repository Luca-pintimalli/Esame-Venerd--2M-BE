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



        public IEnumerable<VerbaleViewModel> GetVerbali()
        {
            try
            {
                var cmd = GetCommand(@"
            SELECT v.IdVerbale, v.DataViolazione, v.IndirizzoViolazione, v.NominativoAgente, 
                   v.DataTrascrizioneVerbale, v.Importo, v.DecurtamentoPunti, 
                   a.Nome AS NomeAnagrafica, 
                   t.Descrizione AS NomeViolazione
            FROM VERBALE v
            JOIN ANAGRAFICA a ON v.IdAnagrafica = a.IdAnagrafica
            JOIN TIPO_VIOLAZIONE t ON v.IdViolazione = t.IdViolazione");

                var conn = GetConnection();
                conn.Open();

                var reader = cmd.ExecuteReader();
                var list = new List<VerbaleViewModel>();

                while (reader.Read())
                {
                    list.Add(new VerbaleViewModel
                    {
                        IdVerbale = reader.GetInt32(0),
                        DataViolazione = reader.GetDateTime(1),
                        IndirizzoViolazione = reader.GetString(2),
                        NominativoAgente = reader.GetString(3),
                        DataTrascrizioneVerbale = reader.GetDateTime(4),
                        Importo = reader.GetDecimal(5),
                        DecurtamentoPunti = reader.GetInt32(6),
                        NomeAnagrafica = reader.GetString(7),
                        NomeViolazione = reader.GetString(8)
                    });
                }

                conn.Close();
                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public void newVerbale(Verbale verbale)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("INSERT INTO VERBALE (DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IdAnagrafica, IdViolazione) VALUES (@DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IdAnagrafica, @IdViolazione)");

                cmd.Parameters.Add(new SqlParameter("@DataViolazione", SqlDbType.Date) { Value = verbale.DataViolazione });
                cmd.Parameters.Add(new SqlParameter("@IndirizzoViolazione", verbale.IndirizzoViolazione ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@NominativoAgente", verbale.NominativoAgente ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@DataTrascrizioneVerbale", SqlDbType.Date) { Value = verbale.DataTrascrizioneVerbale });
                cmd.Parameters.Add(new SqlParameter("@Importo", verbale.Importo));
                cmd.Parameters.Add(new SqlParameter("@DecurtamentoPunti", verbale.DecurtamentoPunti));
                cmd.Parameters.Add(new SqlParameter("@IdAnagrafica", verbale.IdAnagrafica));
                cmd.Parameters.Add(new SqlParameter("@IdViolazione", verbale.IdViolazione));

                //GESTIONE CONNESIONE
                var conn = GetConnection();

                //APRO CONNESIONE
                conn.Open();

                var result = cmd.ExecuteNonQuery();

                //CHIUSURA CONNESIONE
                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}

