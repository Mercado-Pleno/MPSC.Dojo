﻿using IBM.Data.DB2.iSeries;
using MPSC.LBJC.Persistencia.Base.Repositorio;
using MPSC.Library.Exemplos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using Numero = System.Int64;


namespace MPSC.Library.Exemplos.Medidas
{
	public class TestandoPerformanceDeGeracaoDeIdsViaSequenceVsTabela : IExecutavel
	{
		public void Executar()
		{
			Console.WriteLine("Pressione alguma Tecla para Iniciar a contagem de BancoExecutandoSequence");
			Console.ReadKey();
			Executar(new BancoExecutandoSequence());

			Console.WriteLine("Pressione alguma Tecla para Iniciar a contagem de BancoAtualizandoTabela");
			Console.ReadKey();
			Executar(new BancoAtualizandoTabela());

			Console.ReadKey();
		}

		private void Executar(IRepositorioGeradorDeId iRepositorioGeradorDeId)
		{
			try
			{
				var stopwatch = new Stopwatch();
				stopwatch.Start();
				Varias_Threads_Simultaneas(iRepositorioGeradorDeId, 300, 3000);
				stopwatch.Stop();
				Console.WriteLine(stopwatch.Elapsed.TotalSeconds.ToString());
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}

		}

		private void Varias_Threads_Simultaneas(IRepositorioGeradorDeId iRepositorioGeradorDeId, Int32 threads, Int32 itens)
		{
			var listaIds = new Dictionary<List<Numero>, GeradorDeId>();
			var listaThread = new List<Thread>();

			for (var i = 0; i < threads; i++)
				listaIds[new List<Numero>()] = new GeradorDeId(iRepositorioGeradorDeId, 1000);

			foreach (var p in listaIds)
			{
				var thread = new Thread(Processar);
				listaThread.Add(thread);
				thread.Start(new Object[] { p.Key, p.Value, itens });
			}

			foreach (var thread in listaThread)
				thread.Join();




		}

		private void Processar(Object obj)
		{
			var array = obj as Object[];
			Processar(array[0] as List<Numero>, array[1] as GeradorDeId, Convert.ToInt32(array[2]));
		}

		private void Processar(List<Numero> lista, GeradorDeId geradorDeId, Int32 quantidade)
		{
			for (var i = 0; i < quantidade; i++)
				lista.Add(geradorDeId.ObterProximo("TabelaA"));
		}

	}
	public class BancoFake : IRepositorioGeradorDeId
	{
		private readonly Dictionary<String, Numero> _gerador = new Dictionary<String, Numero>();
		public BancoFake() { }
		public Numero GerarProximoIdentificador(String chavePesquisa, Numero pool)
		{
			if ((!_gerador.ContainsKey(chavePesquisa)))
				_gerador[chavePesquisa] = 1;

			return (_gerador[chavePesquisa] += pool) - pool;
		}
	}

	public class BancoExecutandoSequence : BancoAbstrato
	{
		public override Numero GerarProximoIdentificador(String chavePesquisa, Numero pool)
		{
			iDbCommand.CommandText = String.Format(@"Select (Next Value For eSim.tmpSQ_{0}) As ProximoId From DUAL", chavePesquisa);
			return Convert.ToInt64(iDbCommand.ExecuteScalar());
		}
	}

	public class BancoAtualizandoTabela : BancoAbstrato
	{
		//private const String cCmdSqlCreate = @"Create Table eSim.tmp_ControleId (Tabela Char(20) Not Null Primary Key, NextId BigInt Not Null)";
		private const String cCmdSqlUpdate = @"Update eSim.tmp_ControleId Set NextId = NextId + @Pool Where (Tabela = @Tabela)";
		private const String cCmdSqlSelect = @"Select NextId From eSim.tmp_ControleId Where (Tabela = @Tabela) For Update Of NextId With RR";

		public override Numero GerarProximoIdentificador(String chavePesquisa, Numero pool)
		{
			iDbCommand.Parameters.Clear();
			var parameterPool = AdicionarParametro("@Pool", pool, DbType.Int64, ParameterDirection.Input);
			var parameterTabela1 = AdicionarParametro("@Tabela", chavePesquisa, DbType.String, ParameterDirection.Input);
			iDbCommand.Transaction = iDbCommand.Connection.BeginTransaction();

			iDbCommand.CommandText = cCmdSqlSelect;
			var result = Convert.ToInt64(iDbCommand.ExecuteScalar());

			iDbCommand.CommandText = cCmdSqlUpdate;
			var retorno = iDbCommand.ExecuteNonQuery();

			iDbCommand.Transaction.Commit();
			return result;
		}
	}

	public abstract class BancoAbstrato : IRepositorioGeradorDeId, IDisposable
	{
		private readonly static IDbConnection conexao = new iDB2Connection("DataSource=mtzsrva2;UserID=usrben;Password=@poiuy;DataCompression=True;SortSequence=SharedWeight;SortLanguageId=PTG;DefaultCollection=eSim;");
		protected readonly IDbCommand iDbCommand;
		static BancoAbstrato() { conexao.Open(); }
		protected BancoAbstrato()
		{
			iDbCommand = conexao.CreateCommand();
			iDbCommand.CommandType = CommandType.Text;
		}
		~BancoAbstrato() { Dispose(); }
		public void Dispose()
		{
			iDbCommand.Dispose();
			conexao.Close();
			conexao.Dispose();
		}

		public abstract Numero GerarProximoIdentificador(String chavePesquisa, Numero pool);

		protected IDbDataParameter AdicionarParametro(String parameterName, Object value, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input)
		{
			var iDbDataParameter = iDbCommand.CreateParameter();

			iDbDataParameter.ParameterName = parameterName;
			iDbDataParameter.Direction = parameterDirection;
			iDbDataParameter.DbType = dbType;
			iDbDataParameter.Value = value;

			if ((dbType == DbType.String) && (value != null) && (value is String))
				iDbDataParameter.Size = ((value as String) ?? String.Empty).Length;

			iDbCommand.Parameters.Add(iDbDataParameter);
			return iDbDataParameter;
		}
	}

}



namespace MPSC.LBJC.Persistencia.Base.Repositorio
{
	public interface IRepositorioGeradorDeId
	{
		Numero GerarProximoIdentificador(String chavePesquisa, Numero pool);
	}

	public class GeradorDeId
	{
		private readonly static Object _lock = new Object();
		private readonly Dictionary<String, GeradorDeIdentidade> _dic;
		private readonly IRepositorioGeradorDeId _repositorioGeradorDeId;
		private readonly Numero _pool;

		public GeradorDeId(IRepositorioGeradorDeId repositorioGeradorDeId, Numero pool)
		{
			_dic = new Dictionary<String, GeradorDeIdentidade>();
			_repositorioGeradorDeId = repositorioGeradorDeId;
			_pool = pool;
		}

		public Numero ObterProximo(String chavePesquisa)
		{
			lock (_lock)
			{
				if ((!_dic.ContainsKey(chavePesquisa)))
					_dic[chavePesquisa] = new GeradorDeIdentidade(this, chavePesquisa);

				return _dic[chavePesquisa].ObterProximo();
			}
		}

		private class GeradorDeIdentidade
		{
			private readonly String _chavePesquisa;
			private readonly GeradorDeId _geradorDeId;
			private Numero _maxId;
			private Numero _nextId;

			internal GeradorDeIdentidade(GeradorDeId geradorId, String chavePesquisa)
			{
				_geradorDeId = geradorId;
				_chavePesquisa = chavePesquisa;
				_nextId = _maxId = 0;
			}

			internal Numero ObterProximo()
			{
				if (_nextId >= _maxId)
				{
					var retorno = _nextId = _geradorDeId._repositorioGeradorDeId.GerarProximoIdentificador(_chavePesquisa, _geradorDeId._pool);
					_maxId = _nextId + _geradorDeId._pool - 1;
					return retorno;
				}

				return ++_nextId;
			}
		}
	}
}