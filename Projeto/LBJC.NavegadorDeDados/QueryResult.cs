using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using IBM.Data.DB2.iSeries;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Text;
using System.Drawing;

namespace LBJC.NavegadorDeDados
{
	public partial class QueryResult : TabPage
	{
		IDataReader dataReader = null;
		Type tipo = null;
		private static Int32 _quantidade = 1;
		private Conexao _conexao = null;

		public QueryResult()
		{
			InitializeComponent();
			this.Text = "Query" + _quantidade++;
			this.txtQuery.Text = "Select * From Pessoa Order By NomePessoa";
		}

		private String QueryAtiva
		{
			get
			{
				return ((txtQuery.SelectedText.Length > 1) ? txtQuery.SelectedText : txtQuery.Text);
			}
		}

		private Conexao Conexao
		{
			get
			{
				return (_conexao ?? (_conexao = new Conexao()));
			}
		}

		public void Executar()
		{
			if (!String.IsNullOrWhiteSpace(QueryAtiva))
			{
				dataReader = Conexao.Executar(QueryAtiva);
				tipo = CreateAnonimousType(dataReader);
				dgResult.DataSource = null;
				Binding();
			}
		}

		private void dgResult_Scroll(object sender, ScrollEventArgs e)
		{
			if (e.NewValue >= dgResult.RowCount - (dgResult.Height / (dgResult.RowTemplate.Height + 1)))
				Binding();
		}

		private void Binding()
		{
			if ((dataReader != null) && (tipo != null))
			{
				var result = Transformar(dataReader, tipo);
				dgResult.DataSource = (dgResult.DataSource as IEnumerable<Object> ?? new List<Object>()).Union(result).ToList();
				//dgResult.RowTemplate.Index
			}
		}

		private IEnumerable<Object> Transformar(IDataReader dataReader, Type tipo)
		{
			var page = 0;
			while (dataReader.Read() && ++page < 100)
				yield return CreateAnonimousObject(dataReader, tipo);
		}

		private object CreateAnonimousObject(IDataReader dataReader, Type tipo)
		{
			var obj = Activator.CreateInstance(tipo);
			var colunas = dataReader.FieldCount;
			for (int i = 0; i < colunas; i++)
			{
				var prop = tipo.GetProperty(dataReader.GetName(i));
				prop.SetValue(obj, dataReader.IsDBNull(i) ? null : dataReader.GetValue(i), null);
			}
			Application.DoEvents();
			return obj;
		}

		private Type CreateAnonimousType(IDataReader dataReader)
		{
			var props = String.Empty;
			var colunas = dataReader.FieldCount;
			for (int i = 0; i < colunas; i++)
			{
				var prop = String.Format("\t\tpublic {0} {1} {{ get; set; }}\r\n", dataReader.GetFieldType(i).Name, dataReader.GetName(i));
				props += prop;
			}
			return CriarClasseVirtual("DadosDinamicos", props);
		}

		public static Type CriarClasseVirtual(String nomeClasse, String codigo)
		{
#pragma warning disable 0618
			Type vType = null;
			//ICodeCompiler vCodeCompiler = (new CSharpCodeProvider() as CodeDomProvider).CreateCompiler();
			CodeDomProvider vCodeCompiler = new CSharpCodeProvider();
			CompilerParameters vCompilerParameters = new CompilerParameters();
			vCompilerParameters.GenerateInMemory = true;
			vCompilerParameters.GenerateExecutable = false;
			vCompilerParameters.IncludeDebugInformation = true;
			var classe = @"using System;
namespace LBJC.Virtual
{{
	public class {0}
	{{
{1}
	}}
}}";

			try
			{
				CompilerResults vResults = vCodeCompiler.CompileAssemblyFromSource(vCompilerParameters, String.Format(classe, nomeClasse, codigo));
				vType = vResults.CompiledAssembly.GetType("LBJC.Virtual." + nomeClasse, true);
			}
			catch (Exception)
			{
			}

#pragma warning restore 0618
			return vType;
		}
	}

	public class Conexao
	{
		public IDataReader Executar(String query)
		{
			IDbConnection iDbConnection = ObterConexao();
			IDbCommand iDbCommand = CriarComando(iDbConnection, query);
			return iDbCommand.ExecuteReader();
		}

		private static IDbCommand CriarComando(IDbConnection iDbConnection, String query)
		{
			if (iDbConnection.State != ConnectionState.Closed)
				iDbConnection.Close();
			if (iDbConnection.State == ConnectionState.Closed)
				iDbConnection.Open();
			IDbCommand iDbCommand = iDbConnection.CreateCommand();
			iDbCommand.CommandText = query;
			iDbCommand.CommandType = CommandType.Text;
			iDbCommand.CommandTimeout = 600;
			return iDbCommand;
		}

		private IDbConnection ObterConexao()
		{
			try
			{
				String vStringConexao = ObterStringConexao(false);
				return new iDB2Connection(vStringConexao);
			}
			catch (Exception)
			{
				String vStringConexao = ObterStringConexao(true);
				return new OleDbConnection(vStringConexao);
			}
		}

		private string ObterStringConexao(Boolean oleDB)
		{
			var server = "10.21.4.52";
			var dataBase = "eSim";
			var usuario = "UsrBen";
			var senha = "@poiuy";
			var strTemplate = (oleDB ? "Provider=IBMDA400;Data Source={0};Default Collection={1};User ID={2};Password={3}" : "Server={0};Database={1};User ID={2};Password={3}");
			return String.Format(strTemplate, server, dataBase, usuario, senha);
		}
	}
}