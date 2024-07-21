using System;
using Progetto_19._07.Models;

namespace Progetto_19._07.Services
{
	public interface IAnagraficaService
	{
		void newTrasgressore(Anagrafica anagrafica);

		//INSERIAMO TUTTI I TRASGRESSORI
		IEnumerable<Anagrafica> GetTrasgressori();

		//Recupero tramite Id  un solo trasgessore.
		Anagrafica GetTrasgressore(int id);




		//NON RICHIESTI NELLA TRACCIA , MA PER UN POSSIBILE UTILIZZO FUTURO VADO AD IMPLEMENTARE ANCHE L'AGGIORNAMENTO E L'ELIMINAZIONE DEL TRASGRESSORE.

		//Modifica dati Trasgressore
		void UpdateTrasgressore(int id ,Anagrafica anagrafica);


		//Elimina Trasgressore
		void DeleteTrasgressore(int id);

	}
}

