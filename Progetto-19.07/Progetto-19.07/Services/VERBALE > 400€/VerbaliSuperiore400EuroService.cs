using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Progetto_19._07.Models;

namespace Progetto_19._07.Services
{
    public class VerbaliSuperiore400EuroService : SqlServerServiceBase, IVerbaliSuperiore400EuroService
    {
        public VerbaliSuperiore400EuroService(IConfiguration config) : base(config)
        {
        }

        public IEnumerable<VerbaliSuperiore400Euro> GetVerbaliSuperiore400Euro()
        {
            try
            {
                //CREO COMANDO 
                var cmd = GetCommand(
                    "SELECT a.IdAnagrafica, a.Nome, a.Cognome, v.DataViolazione, v.Importo, v.DecurtamentoPunti " +
                    "FROM VERBALE v " +
                    "JOIN ANAGRAFICA a ON v.IdAnagrafica = a.IdAnagrafica " +
                    "WHERE v.Importo > 400");
                //GESTIONE CONNESIONE 
                var conn = GetConnection();
                conn.Open();
                //UTILIZZO COMANDO 
                var reader = cmd.ExecuteReader();
                var list = new List<VerbaliSuperiore400Euro>();
                while (reader.Read())
                {
                    list.Add(new VerbaliSuperiore400Euro
                    {
                        IdAnagrafica = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        DataViolazione = reader.GetDateTime(3),
                        Importo = reader.GetDecimal(4),
                        DecurtamentoPunti = reader.GetInt32(5)
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
    }
}
