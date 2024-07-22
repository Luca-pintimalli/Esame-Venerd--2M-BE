using System;
using Progetto_19._07.Models;

namespace Progetto_19._07.Services
{
	public interface ITotalePunti
	{
		IEnumerable<TotalePunti> GetTotalePunti();
	}
}

