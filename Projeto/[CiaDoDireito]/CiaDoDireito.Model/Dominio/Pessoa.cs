using System;

namespace DireitoECia.Model.Dominio
{
	public abstract class Pessoa: Entidade
	{
		public String Nome { get; set; }

		public override void IsValid()
		{
			if (String.IsNullOrWhiteSpace(Nome))
				throw new Exception("Nome não pode ser nulo ou vazio ou espaços em branco");
		}
	}
}