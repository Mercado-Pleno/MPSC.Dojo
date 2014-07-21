using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace LBJC.NavegadorDeDados
{
	public partial class QueryResult : TabPage, IQueryResult
	{
		private static Int32 _quantidade = 1;

		private Conexao _conexao = null;
		private ClasseDinamica _classeDinamica = null;

		private Conexao Conexao { get { return (_conexao ?? (_conexao = new Conexao())); } }
		private ClasseDinamica ClasseDinamica { get { return (_classeDinamica ?? (_classeDinamica = new ClasseDinamica())); } }

		private String QueryAtiva { get { return ((txtQuery.SelectedText.Length > 1) ? txtQuery.SelectedText : txtQuery.Text); } }

		public QueryResult()
		{
			InitializeComponent();
			this.Text = "Query" + _quantidade++;
		}

		private void txtQuery_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyValue == 190) || (e.KeyValue == 194))
				AutoCompletar();
			else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.A))
				txtQuery.SelectAll();
			else if (e.KeyCode == Keys.F5)
				Executar();
			else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Y))
				Executar();
		}

		private void AutoCompletar()
		{
			try
			{
				AutoCompletarImpl();
			}
			catch (Exception) { }
		}

		private void AutoCompletarImpl()
		{
			var tokenKeys = "\r\n\t() ";
			var query = txtQuery.Text.ToUpper().Insert(txtQuery.SelectionStart, ".");
			var tokens = query.Split(tokenKeys.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

			var i = txtQuery.SelectionStart;
			while (!tokenKeys.Contains(query[--i - 1])) ;
			var token = query.Substring(i, txtQuery.SelectionStart - i);
			var index = tokens.LastIndexOf(token);
			if (tokens.Count > 1)
			{
				if (tokens[index - 1].Equals("AS"))
					token = tokens[index - 2];
				else if (tokens[index - 1].Equals("FROM") || tokens[index - 1].Equals("JOIN"))
					token = tokens[index];
				else
					token = tokens[index - 1];
			}
			query = "Select * From " + token + " Where 0=1";

			var dataReader = Conexao.Executar(query);
			var properties = "";
			var colunas = dataReader.FieldCount;
			for (i = 0; i < colunas; i++)
				properties += dataReader.GetName(i) + "\r\n";

			var lista = properties.Split(tokenKeys.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList().OrderBy(a => a).ToList();
			ListCampos.Exibir(lista, this, new Point(100, 100), OnSelecionarAutoCompletar);
		}

		private void OnSelecionarAutoCompletar(String item)
		{
			if (!String.IsNullOrWhiteSpace(item))
			{
				var start = txtQuery.SelectionStart;
				txtQuery.Text = txtQuery.Text.Insert(start, item);
				txtQuery.SelectionStart = start + item.Length;
			}
			txtQuery.Focus();
		}

		private void txtQuery_KeyUp(object sender, KeyEventArgs e)
		{
		}

		private void dgResult_Scroll(object sender, ScrollEventArgs e)
		{
			if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				var linhasVisiveis = dgResult.Height / (dgResult.RowTemplate.Height + 1);
				var primeiraLinha = dgResult.RowCount - linhasVisiveis;
				if (e.NewValue >= primeiraLinha)
					Binding();
			}
		}

		public void Executar()
		{
			var query = QueryAtiva;
			if (!String.IsNullOrWhiteSpace(query))
			{
				try
				{
					var tempQuery = txtQuery.Text + "/**/";
					var comentarios = tempQuery.Substring(tempQuery.IndexOf("/*") + 2);
					comentarios = comentarios.Substring(0, comentarios.IndexOf("*/"));
					var variaveis = comentarios.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
					foreach (String variavel in variaveis)
					{
						var param = variavel.Substring(0, variavel.IndexOf("=") + 1).Replace("=", "").Trim();
						var valor = variavel.Substring(variavel.IndexOf("=") + 1).Trim();
						query = query.Replace(param, valor);
					}

					var dataReader = Conexao.Executar(query);
					ClasseDinamica.Reset(dataReader);
					dgResult.DataSource = null;
					Binding();
				}
				catch (Exception vException)
				{
					MessageBox.Show("Houve um problema ao executar a query. Detalhes:\n" + vException.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		public void Binding()
		{
			var result = ClasseDinamica.Transformar();
			dgResult.DataSource = (dgResult.DataSource as IEnumerable<Object> ?? new List<Object>()).Union(result).ToList();
		}
	}

	public interface IQueryResult
	{
		void Executar();
		void Binding();
	}

	public class NullQueryResult : IQueryResult
	{
		public void Executar() { }
		public void Binding() { }

		private static IQueryResult _instance;
		public static IQueryResult Instance { get { return _instance ?? (_instance = new NullQueryResult()); } }
	}
}