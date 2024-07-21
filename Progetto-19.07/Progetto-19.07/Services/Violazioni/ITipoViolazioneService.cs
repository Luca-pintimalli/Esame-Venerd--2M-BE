using System;
using Progetto_19._07.Models;

namespace Progetto_19._07.Services
{
	public interface ITipoViolazioneService
	{
		void newTipoViolazione(TipoViolazione tipoViolazione);

		//INSERIMENTO DI TUTTI I TIPI DI VIOLAZIONE
		IEnumerable<TipoViolazione> GetViolazioni();


	}
}

