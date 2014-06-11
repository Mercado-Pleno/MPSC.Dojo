using System;

namespace DireitoECia.Model.Dominio
{
	public class Processo : Entidade
	{
		public DateTime Abertura { get; set; }
		public String Descricao { get; set; }
		public Cliente Cliente { get; set; }
		public Advogado Advogado { get; set; }

		public override void IsValid()
		{
			if (String.IsNullOrWhiteSpace(Descricao))
				throw new Exception("Descrição não pode ser nulo ou vazio ou espaços em branco");
		}
	}
}
