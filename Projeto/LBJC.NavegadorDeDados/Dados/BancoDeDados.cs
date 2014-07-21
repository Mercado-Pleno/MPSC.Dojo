using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using IBM.Data.DB2.iSeries;

namespace LBJC.NavegadorDeDados.Dados
{
	public interface IBanco
	{
		//Type Conexao { get; }
		String Descricao { get; }
		String StringConexaoTemplate { get; }
	}

	public abstract class BancoDeDados<TConexao> : IBanco where TConexao : IDbConnection
	{
		public static IEnumerable<IBanco> Conexoes = new List<IBanco>(new IBanco[]{
			new SQLServer(),
			new OleDb(),
			new IBMDB2()
		});

		public Type Tipo { get { return typeof(TConexao); } }
		public abstract String Descricao { get; }
		public abstract String StringConexaoTemplate { get; }

		public String Server { get; private set; }
		public String DataBase { get; private set; }
		public String Usuario { get; private set; }
		public String Senha { get; private set; }

		public BancoDeDados<TConexao> Configurar(String server, String dataBase, String usuario, String senha)
		{
			Server = server;
			DataBase = dataBase;
			Usuario = usuario;
			Senha = senha;
			return this;
		}

		public IDbConnection ObterConexao()
		{
			return CriarNovaConexao(Tipo);
		}

		protected virtual IDbConnection CriarNovaConexao(Type tipo)
		{
			var stringConexao = String.Format(StringConexaoTemplate, Server, DataBase, Usuario, Senha);
			return Activator.CreateInstance(tipo, stringConexao) as IDbConnection;
		}
	}

	public class IBMDB2 : BancoDeDados<iDB2Connection>
	{
		public override String Descricao { get { return "IBM DB2"; } }
		public override String StringConexaoTemplate { get { return "DataSource={0};UserID={2};Password={3};DataCompression=True;SortSequence=SharedWeight;SortLanguageId=PTG;DefaultCollection={1};"; } }
	}

	public class OleDb : BancoDeDados<OleDbConnection>
	{
		public override String Descricao { get { return "Ole DB"; } }
		public override String StringConexaoTemplate { get { return "Provider=IBMDA400;Data Source={0};Default Collection={1};User ID={2};Password={3}"; } }
	}

	public class SQLServer : BancoDeDados<SqlConnection>
	{
		public override String Descricao { get { return "Sql Server"; } }
		public override String StringConexaoTemplate { get { return "Persist Security Info=True;Data Source={0};Initial Catalog={1};User ID={2};Password={3};MultipleActiveResultSets=True;"; } }
	}
}