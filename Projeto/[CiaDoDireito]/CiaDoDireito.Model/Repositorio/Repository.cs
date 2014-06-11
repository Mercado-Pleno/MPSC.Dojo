using System.Linq;
using System.Collections.Generic;
using DireitoECia.Model.Dominio;

namespace DireitoECia.Model.Repositorio
{
	public class Repository
	{
		private static Repository _instancia;
		public static Repository Instancia
		{
			get {
				if (_instancia == null)
					_instancia = new Repository();
				return _instancia;
				//return _instancia ?? (_instancia = new Repository()); 
			}
			set { _instancia = value; }
		}

		private List<Cliente> Clientes { get; set; }
		private List<Advogado> Advogados { get; set; }
		private List<Processo> Processos { get; set; }

		private Repository()
		{
			Clientes = new List<Cliente>();
			Advogados = new List<Advogado>();
			Processos = new List<Processo>();
		}

		public void Incluir(Advogado advogado)
		{
			advogado.IsValid();
			advogado.Id = Advogados.Count + 1;
			Advogados.Add(advogado);
		}

		public Advogado Obter(Advogado advogado)
		{
			return Advogados.FirstOrDefault(a => a.Id == advogado.Id) ?? Advogados.FirstOrDefault(a => a.Nome == advogado.Nome);
		}

		public IEnumerable<Advogado> ListarTodosAdvogados()
		{
			return Advogados;
		}
	}
}