using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Progetto_19._07.Models;

namespace Progetto_19._07.Services
{
    public class TotalePuntiService : SqlServerServiceBase, ITotalePunti
    {
        public TotalePuntiService(IConfiguration config) : base(config)
        {
        }

        public IEnumerable<TotalePunti> GetTotalePunti()
        {
            try
            {
                //CREO COMANDO 
                var cmd = GetCommand(
                    "SELECT a.IdAnagrafica, a.Nome, a.Cognome, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati " +
                    "FROM VERBALE v " +
                    "JOIN ANAGRAFICA a ON v.IdAnagrafica = a.IdAnagrafica " +
                    "GROUP BY a.IdAnagrafica, a.Nome, a.Cognome " +
                    "ORDER BY a.Nome");
                //GESTIONE CONNESIONE 
                var conn = GetConnection();
                conn.Open();

                //UTILIZZO COMANDO 
                var reader = cmd.ExecuteReader();
                var list = new List<TotalePunti>();
                while (reader.Read())
                {
                    list.Add(new TotalePunti
                    {
                        IdAnagrafica = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        TotalePuntiDecurtati = reader.GetInt32(3)
                    });
                }

                return list;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
