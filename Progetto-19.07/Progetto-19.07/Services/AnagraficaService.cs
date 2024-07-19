using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Progetto_19._07.Models;

public class AnagraficaService
{
    private readonly string _connectionString;

    public AnagraficaService(IConfiguration conf)
    {
        var connection = new System.Data.SqlClient.SqlConnection(conf.GetConnectionString("AppDb"));
    }

    public IEnumerable<Anagrafica> GetAll()
    {
        var anagrafiche = new List<Anagrafica>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("SELECT * FROM Anagrafica", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        anagrafiche.Add(new Anagrafica
                        {
                            IdAnagrafica = reader.GetInt32(reader.GetOrdinal("idanagrafica")),
                            Cognome = reader.GetString(reader.GetOrdinal("Cognome")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Indirizzo = reader.GetString(reader.GetOrdinal("Indirizzo")),
                            Città = reader.GetString(reader.GetOrdinal("Città")),
                            CAP = reader.GetString(reader.GetOrdinal("CAP")),
                            Cod_Fisc = reader.GetString(reader.GetOrdinal("Cod_Fiscale"))
                        });
                    }
                }
            }
        }

        return anagrafiche;
    }

    public Anagrafica GetById(int id)
    {
        Anagrafica anagrafica = null;

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("SELECT * FROM Anagrafica WHERE idanagrafica = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        anagrafica = new Anagrafica
                        {
                            IdAnagrafica = reader.GetInt32(reader.GetOrdinal("idanagrafica")),
                            Cognome = reader.GetString(reader.GetOrdinal("Cognome")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Indirizzo = reader.GetString(reader.GetOrdinal("Indirizzo")),
                            Città = reader.GetString(reader.GetOrdinal("Città")),
                            CAP = reader.GetString(reader.GetOrdinal("CAP")),
                            Cod_Fisc = reader.GetString(reader.GetOrdinal("Cod_Fiscale"))
                        };
                    }
                }
            }
        }

        return anagrafica;
    }

    public void Add(Anagrafica anagrafica)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("INSERT INTO Anagrafica (Cognome, Nome, Indirizzo, Città, CAP, Cod_Fiscale) VALUES (@Cognome, @Nome, @Indirizzo, @Città, @CAP, @Cod_Fiscale)", connection))
            {
                command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                command.Parameters.AddWithValue("@Città", anagrafica.Città);
                command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                command.Parameters.AddWithValue("@Cod_Fiscale", anagrafica.Cod_Fisc);

                command.ExecuteNonQuery();
            }
        }
    }

    public void Update(Anagrafica anagrafica)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("UPDATE Anagrafica SET Cognome = @Cognome, Nome = @Nome, Indirizzo = @Indirizzo, Città = @Città, CAP = @CAP, Cod_Fiscale = @Cod_Fiscale WHERE idanagrafica = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", anagrafica.IdAnagrafica);
                command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                command.Parameters.AddWithValue("@Città", anagrafica.Città);
                command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                command.Parameters.AddWithValue("@Cod_Fiscale", anagrafica.Cod_Fisc);

                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("DELETE FROM Anagrafica WHERE idanagrafica = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}