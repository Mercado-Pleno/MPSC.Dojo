using System.Linq;
using DireitoECia.Model.Dominio;
using DireitoECia.Model.Repositorio;
using System;
using System.Collections.Generic;

namespace DireitoECia.Controller
{
	public class ApplicationController
	{
		private ApplicationController() { }
		private static ApplicationController _instancia;
		public static ApplicationController Instancia
		{
			get
			{
				if (_instancia == null)
					_instancia = new ApplicationController();
				return _instancia;
				//return _instancia ?? (_instancia = new Repository()); 
			}
			set { _instancia = value; }
		}

		public void Incluir(Advogado advogado)
		{
			var adv = Repository.Instancia.Obter(advogado);
			if (adv != null)
				throw new Exception(String.Format("Já existe um advogado com este nome '{0}', id={1};", adv.Nome, adv.Id));
			Repository.Instancia.Incluir(advogado);
		}

		public IEnumerable<Advogado> ListarTodosAdvogados()
		{
			return Repository.Instancia.ListarTodosAdvogados();
		}
	}
}
