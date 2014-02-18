using System;
using System.Collections.Generic;

namespace VendasModel
{
	public class Cliente
	{
		public int Codigo { get; set; }
		public String Nome { get; set; }
		public DateTime Nascimento { get; set; }
		public Boolean Ativo { get; set; }

		public int Idade
		{
			get
			{
				return DateTime.Now.Year - Nascimento.Year;
			}
		}
	}

	public class ClienteRepository
	{
		private static readonly Object _ = new Object();
		private static ClienteRepository instancia = null;
		private static int codigo = 0;

		public ClienteRepository()
		{
			ListaCliente = new List<Cliente>();
		}

		public static ClienteRepository Instancia
		{
			get
			{
				if (instancia == null) lock (_) if (instancia == null) instancia = new ClienteRepository();
				return instancia;
			}
		}

		public int ObterNovoCodigo()
		{
			lock (_)
				return ++codigo;
		}

		public IList<Cliente> ListaCliente { get; set; }
	}
}
