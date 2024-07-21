using System;
using Progetto_19._07.Models;

namespace Progetto_19._07.Services
{
	public interface IVerbaleService
	{
		void newVerbale(Verbale verbale);

		//LISTA CON TUTTI I VERBALI
		IEnumerable<Verbale> GetVerbali();


	}
}

