using System;
using System.Data.Common;

namespace Progetto_19._07.Services
{
	public abstract  class ServiceBase
	{
		//UN METODO CHE RESTITUISCA LA CONNESSIONE AL DATABASE. 
		protected abstract DbConnection GetConnection();


		//METODO CHE CRA UN COMANDO.
		protected abstract DbCommand GetCommand(string commandText);
    }
}

