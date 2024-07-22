using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Progetto_19._07.Models;

namespace Progetto_19._07.Services
{
    public class VerbaliSuperiorePuntiService : SqlServerServiceBase, IVerbaleSuperiorePunti
    {
        public VerbaliSuperiorePuntiService(IConfiguration config) : base(config)
        {
        }

        public IEnumerable<VerbaliSuperiorePunti> GetVerbaliSuperiore10Punti()
        {
            try
            {
                //creo comando 
                var cmd = GetCommand(
                    "SELECT a.IdAnagrafica, a.Nome, a.Cognome, v.DataViolazione, v.Importo, v.DecurtamentoPunti " +
                    "FROM VERBALE v " +
                    "JOIN ANAGRAFICA a ON v.IdAnagrafica = a.IdAnagrafica " +
                    "WHERE v.DecurtamentoPunti > 10");
                //apro connesione 
                var conn = GetConnection();
                conn.Open();
                //utilizzo comando 
                var reader = cmd.ExecuteReader();
                var list = new List<VerbaliSuperiorePunti>();
                while (reader.Read())
                {
                    list.Add(new VerbaliSuperiorePunti
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
