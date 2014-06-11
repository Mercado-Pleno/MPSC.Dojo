using System.Collections.Generic;
using System;

namespace DireitoECia.Model.Dominio
{
	public class Cliente : Pessoa
	{
		public List<Processo> Processos { get; set; }

		public override void IsValid()
		{
			base.IsValid();
		}

	}
}