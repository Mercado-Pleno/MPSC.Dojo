using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.SVNControl.MockData.DataBaseInterface
{
	public interface IRecurso
	{
		String Nome { get; }
		IList<IServidor> ListaServidor { get; }
		IServidor AdicionarServidor(IServidor servidor);

		IServidor Obter(String nome_IP_DNS_Servidor);
	}

	public class Recurso : IRecurso
	{
		private static IRecurso _instancia;
		public static IRecurso Instancia { get { return (_instancia ?? (_instancia = new Recurso())); } }
		public string Nome { get; private set; }
		public IList<IServidor> ListaServidor { get; private set; }
		private Recurso()
		{
			Nome = "Mock Database";
			ListaServidor = new List<IServidor>();
		}

		public IServidor AdicionarServidor(IServidor servidor)
		{
			ListaServidor.Add(servidor);
			return servidor;
		}

		public IServidor Obter(String nome_IP_DNS_Servidor)
		{
			return ListaServidor.First(s => s.IP.Equals(nome_IP_DNS_Servidor));
		}
	}
}