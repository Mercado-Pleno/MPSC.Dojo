using System;
using System.Collections.Generic;
using MP.SVNControl.MockData.DataBaseInterface;

namespace MP.SVNControl.Test
{
	public static class Test
	{
		public static void Main(string[] args)
		{
			var recurso = new Recurso();
			var servidor = recurso.AdicionarServidor(new Servidor("192.168.0.1"));
			var database = servidor.AdicionarBancoDados(new PlenoSMS("PlenoSMS"));
			var tabela1 = database.AdicionarTabela(new Tabela<Cliente>());
			var tabela2 = database.AdicionarTabela(new Tabela<Documento>());

			tabela1.Adicionar(new Cliente());
		}
	}


	public class Recurso : IRecurso
	{
		public IList<IServidor> ListaServidor { get; private set; }
		public Recurso()
		{
			ListaServidor = new List<IServidor>();
		}

		public IServidor AdicionarServidor(IServidor servidor)
		{
			ListaServidor.Add(servidor);
			return servidor;
		}
	}

	public class Servidor : IServidor
	{
		public String IP { get; private set; }
		public IList<IBancoDados> ListaBancoDados { get; private set; }

		public Servidor(String nomeIP)
		{
			IP = nomeIP;
			ListaBancoDados = new List<IBancoDados>();
		}


		public IBancoDados AdicionarBancoDados(IBancoDados bancoDados)
		{
			ListaBancoDados.Add(bancoDados);
			return bancoDados;
		}
	}

	public class PlenoSMS : IBancoDados
	{
		public String Nome { get; private set; }
		public IList<ITabela> ListaTabela { get; private set; }

		public PlenoSMS(String nome)
		{
			Nome = nome;
			ListaTabela = new List<ITabela>();
		}

		public ITabela<T> AdicionarTabela<T>(ITabela<T> tabela)
		{
			ListaTabela.Add(tabela);
			return tabela;
		}
	}

	public class Tabela<T> : ITabela<T>
	{
		public String Nome { get; private set; }
		public IList<T> ListaDados { get; private set; }

		public Tabela() : this(typeof(T).Name) { }
		public Tabela(String nome)
		{
			Nome = nome;
		}

		public T Adicionar(T dados)
		{
			ListaDados.Add(dados);
			return dados;
		}
	}




	public class Cliente
	{
		public String Nome { get; set; }
		public int Idade { get; set; }
	}

	public class Documento
	{
		public String Numero { get; set; }
	}
}