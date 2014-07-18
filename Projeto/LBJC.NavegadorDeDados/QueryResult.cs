using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
			var query = txtQuery.Text.ToUpper();
			var i = txtQuery.SelectionStart;
			while (!" \n\r\t".Contains(query[i - 1]))
				i--;
			var token = query.Substring(i, txtQuery.SelectionStart - i);

			i = query.IndexOf(" " + token + " ");
			while (!" \n\r\t".Contains(query[i - 1]))
				i--;
			token = query.Substring(i, query.IndexOf(" " + token + " ") - i);

			query = "Select * From " + token + " Where 0=1";

			var dataReader = Conexao.Executar(query);
			var props = "";
			var colunas = dataReader.FieldCount;
			for (i = 0; i < colunas; i++)
			{
				var prop = dataReader.GetName(i) + "\r\n";
				props += prop;
			}

			MessageBox.Show(props);
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
					var comentarios = txtQuery.Text.Substring(txtQuery.Text.IndexOf("/*") + 2);
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