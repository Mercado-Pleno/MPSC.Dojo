using System;
using System.Collections.Generic;
using MP.SVNControl.MockData.DataBaseInterface;

namespace MP.SVNControl.Test
{
	public static class PrincipalTest
	{
		public static void Main(string[] args)
		{
			var recurso = Recurso.Instancia;
			var servidor = recurso.AdicionarServidor(new Servidor("192.168.0.1"));
			var database = servidor.AdicionarBancoDados(new BancoDados("PlenoSMS"));
			var tabela1 = database.AdicionarTabela(new Tabela<Cliente>());
			var tabela2 = database.AdicionarTabela(new Tabela<Documento>());

			tabela1.Adicionar(new Cliente());
			tabela1.Adicionar(new Cliente());
			tabela1.Adicionar(new Cliente());
			tabela2.Adicionar(new Documento());
			tabela2.Adicionar(new Documento());
			tabela2.Adicionar(new Documento());
		}
	}


	public class Recurso : IRecurso
	{
		private static IRecurso _instancia;
		public static IRecurso Instancia { get { return (_instancia ?? (_instancia = new Recurso())); } }
		public IList<IServidor> ListaServidor { get; private set; }
		private Recurso()
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

	public class BancoDados : IBancoDados
	{
		public String Nome { get; private set; }
		public IList<ITabela> ListaTabela { get; private set; }

		public BancoDados(String nome)
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