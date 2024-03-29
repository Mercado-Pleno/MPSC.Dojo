﻿using IBM.Data.DB2.iSeries;
using MPSC.LBJC.Persistencia.Base.Repositorio;
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
			Console.Write("\r\nPressione alguma Tecla para Iniciar a contagem de BancoExecutandoSequence");
			Console.ReadLine();
			Console.Write("BancoExecutandoSequence: ");
			Executar(new BancoExecutandoSequence());

			Console.Write("\r\nPressione alguma Tecla para Iniciar a contagem de BancoAtualizandoTabela");
			Console.ReadLine();
			Console.Write("BancoAtualizandoTabela: ");
			Executar(new BancoAtualizandoTabela());

			Console.Write("\r\nAcabou");
		}

		private void Executar(IRepositorioGeradorDeIdentidade iRepositorioGeradorDeId)
		{
			try
			{
				var stopwatch = new Stopwatch();
				stopwatch.Start();
				Varias_Threads_Simultaneas(iRepositorioGeradorDeId, 500, 20000);
				stopwatch.Stop();
				Console.WriteLine(stopwatch.Elapsed.TotalSeconds.ToString());
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
		}

		private void Varias_Threads_Simultaneas(IRepositorioGeradorDeIdentidade iRepositorioGeradorDeId, Int32 threads, Int32 itens)
		{
			var listaIds = new Dictionary<List<Numero>, ContainerGeradorDeIdentidade>();
			var listaThread = new List<Thread>();

			for (var i = 0; i < threads; i++)
				listaIds[new List<Numero>()] = new ContainerGeradorDeIdentidade(iRepositorioGeradorDeId);

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
			Processar(null, array[1] as ContainerGeradorDeIdentidade, Convert.ToInt32(array[2]));
		}

		private void Processar(List<Numero> lista, ContainerGeradorDeIdentidade geradorDeId, Int32 quantidade)
		{
			for (var i = 0; i < quantidade; i++)
				geradorDeId.ObterProximo("TabelaA");
		}
	}
	public class BancoFake : IRepositorioGeradorDeIdentidade
	{
		private readonly Dictionary<String, Numero> _sequence = new Dictionary<String, Numero>();
		private const long cQuantidadeDeIdentificadoresEmCache = 3000L;

		public Numero GerarProximoIdentificador(String nomeDaTabela)
		{
			if (!_sequence.ContainsKey(nomeDaTabela))
				_sequence[nomeDaTabela] = 1;

			var retorno = _sequence[nomeDaTabela];
			_sequence[nomeDaTabela] += cQuantidadeDeIdentificadoresEmCache;
			return retorno;
		}

		public Numero ObterQuantidadeDeIdentificadoresEmCache(String nomeDaTabela)
		{
			return cQuantidadeDeIdentificadoresEmCache;
		}
	}

	internal class BancoAtualizandoTabela : BancoAbstrato
	{
		protected override long gerarProximoIdentificador(string nomeDaTabela, long quantidadeDeIdentificadoresEmCache)
		{
			//private const String cCmdSqlCreate = @"Create Table eSim.tmp_ControleId (Tabela Char(20) Not Null Primary Key, NextId BigInt Not Null)";
			const String cCmdSqlUpdate = @"Update eSim.tmp_ControleId Set NextId = NextId + @Pool Where (Tabela = @Tabela)";
			const String cCmdSqlSelect = @"Select NextId From eSim.tmp_ControleId Where (Tabela = @Tabela) For Update Of NextId With RR";


			iDbCommand.Parameters.Clear();
			var parameterPool = AdicionarParametro("@Pool", quantidadeDeIdentificadoresEmCache, DbType.Int64, ParameterDirection.Input);
			var parameterTabela1 = AdicionarParametro("@Tabela", nomeDaTabela, DbType.String, ParameterDirection.Input);
			iDbCommand.Transaction = iDbCommand.Connection.BeginTransaction();

			iDbCommand.CommandText = cCmdSqlSelect;
			var result = Convert.ToInt64(iDbCommand.ExecuteScalar());

			iDbCommand.CommandText = cCmdSqlUpdate;
			var retorno = iDbCommand.ExecuteNonQuery();

			iDbCommand.Transaction.Commit();
			return result;
		}
	}

	internal class BancoExecutandoSequence : BancoAbstrato
	{
		protected override long gerarProximoIdentificador(string nomeDaTabela, long quantidadeDeIdentificadoresEmCache)
		{
			iDbCommand.CommandText = String.Format(@"Select (Next Value For eSim.tmpSQ_{0}) As ProximoId From DUAL", nomeDaTabela);
			return Convert.ToInt64(iDbCommand.ExecuteScalar());
		}
	}

	internal abstract class BancoAbstrato : IRepositorioGeradorDeIdentidade, IDisposable
	{
		private readonly static IDbConnection conexao = null;//new iDB2Connection("DataSource=mtzsrva2;UserID=usrben;Password=@poiuy;DataCompression=True;SortSequence=SharedWeight;SortLanguageId=PTG;DefaultCollection=eSim;");
		private const long cQuantidadeDeIdentificadoresEmCache = 3000L;
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

		long IRepositorioGeradorDeIdentidade.GerarProximoIdentificador(String nomeDaTabela)
		{
			return gerarProximoIdentificador(nomeDaTabela, cQuantidadeDeIdentificadoresEmCache);
		}

		long IRepositorioGeradorDeIdentidade.ObterQuantidadeDeIdentificadoresEmCache(string nomeDaTabela)
		{
			return cQuantidadeDeIdentificadoresEmCache;
		}

		protected abstract long gerarProximoIdentificador(String nomeDaTabela, long quantidadeDeIdentificadoresEmCache);

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