using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LBJC.NavegadorDeDados
{
	public partial class QueryResult : TabPage, IQueryResult
	{
		private static Int32 _quantidade = 1;
		public String NomeDoArquivo { get; private set; }

		private Conexao _conexao = null;
		private ClasseDinamica _classeDinamica = null;

		private Conexao Conexao { get { return (_conexao ?? (_conexao = new Conexao())); } }
		private ClasseDinamica ClasseDinamica { get { return (_classeDinamica ?? (_classeDinamica = new ClasseDinamica())); } }

		private String QueryAtiva { get { return ((txtQuery.SelectedText.Length > 1) ? txtQuery.SelectedText : txtQuery.Text); } }

		public QueryResult(String nomeDoArquivo)
		{
			InitializeComponent();
			Abrir(nomeDoArquivo);
		}

		public void Abrir(String nomeDoArquivo)
		{
			if (!String.IsNullOrWhiteSpace(nomeDoArquivo) && File.Exists(nomeDoArquivo))
			{
				txtQuery.Modified = false;
				txtQuery.Text = File.ReadAllText(NomeDoArquivo = nomeDoArquivo);
			}
			else
				NomeDoArquivo = "Query" + (_quantidade++) + ".sql";
			UpdateDisplay();
		}

		public Boolean Salvar()
		{
			if (String.IsNullOrWhiteSpace(NomeDoArquivo) || !File.Exists(NomeDoArquivo))
				NomeDoArquivo = Extensions.GetFileToSave("Arquivos de Banco de Dados|*.sql") ?? NomeDoArquivo;

			if (!String.IsNullOrWhiteSpace(NomeDoArquivo))
			{
				txtQuery.Modified = false;
				File.WriteAllText(NomeDoArquivo, txtQuery.Text);
			}
			UpdateDisplay();

			return !String.IsNullOrWhiteSpace(NomeDoArquivo);
		}

		private void txtQuery_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyValue == 190) || (e.KeyValue == 194))
				AutoCompletar();
			else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.A))
				txtQuery.SelectAll();
			else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.S))
				Salvar();
			else if (e.KeyCode == Keys.F5)
				Executar();
			else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Y))
				Executar();
		}

		private void txtQuery_KeyUp(object sender, KeyEventArgs e)
		{
			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			Text = Path.GetFileName(NomeDoArquivo) + (txtQuery.Modified ? " *" : "");
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
					query = Extensions.ConverterParametrosEmConstantes(txtQuery.Text, query);
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

		private void Binding()
		{
			var result = ClasseDinamica.Transformar();
			dgResult.DataSource = (dgResult.DataSource as IEnumerable<Object> ?? new List<Object>()).Union(result).ToList();
		}

		private void AutoCompletar()
		{
			try
			{
				var apelido = Extensions.ObterApelidoAntesDoPonto(txtQuery.Text, txtQuery.SelectionStart);
				var tabela = Extensions.ObterNomeTabelaPorApelido(txtQuery.Text, txtQuery.SelectionStart, apelido);
				var campos = Extensions.ListarColunasDasTabelas(Conexao, tabela);
				ListaDeCampos.Exibir(campos, this, txtQuery.CurrentCharacterPosition(), OnSelecionarAutoCompletar);
			}
			catch (Exception) { }
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

		public void Dispose()
		{
			if (_conexao != null)
			{
				_conexao.Dispose();
				_conexao = null;
			}

			if (_classeDinamica != null)
			{
				_classeDinamica.Dispose();
				_classeDinamica = null;
			}

			txtQuery.Clear();
			txtQuery.Dispose();

			dgResult.DataSource = null;
			dgResult.Dispose();

			base.Dispose();
		}
	}

	public interface IQueryResult : IDisposable
	{
		void Executar();
		Boolean Salvar();
	}

	public class NullQueryResult : IQueryResult
	{
		public void Executar() { }
		public Boolean Salvar() { return false; }
		public void Dispose() { }

		private static IQueryResult _instance;
		public static IQueryResult Instance { get { return _instance ?? (_instance = new NullQueryResult()); } }
	}
}