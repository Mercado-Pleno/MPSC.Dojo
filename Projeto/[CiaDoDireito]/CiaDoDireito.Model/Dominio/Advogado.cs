using System;
using System.Collections.Generic;

namespace DireitoECia.Model.Dominio
{
	public class Advogado : Pessoa
	{
		public List<Cliente> Clientes { get; set; }
		public List<Processo> Processos { get; set; }

		public override void IsValid()
		{
			base.IsValid();
		}
	}
}