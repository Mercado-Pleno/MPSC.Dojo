using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IServidor
	{
		String IP { get; }
		IList<IBancoDados> ListaBancoDados { get; }
		IBancoDados AdicionarBancoDados(IBancoDados bancoDados);

		IBancoDados Obter(String Database);
	}

	public class Servidor : IServidor
	{
		public String IP { get; private set; }
		public String Usuario { get; private set; }
		public String Senha { get; private set; }
		public IList<IBancoDados> ListaBancoDados { get; private set; }

		public Servidor(String nome_IP_DNS_Servidor, String usuario, String senha)
		{
			IP = nome_IP_DNS_Servidor;
			Usuario = usuario;
			Senha = senha;
			ListaBancoDados = new List<IBancoDados>();
		}

		public IBancoDados AdicionarBancoDados(IBancoDados bancoDados)
		{
			ListaBancoDados.Add(bancoDados);
			return bancoDados;
		}

		public IBancoDados Obter(String nomeDoBancoDeDados)
		{
			return ListaBancoDados.First(b => b.Nome.Equals(nomeDoBancoDeDados));
		}
	}
}