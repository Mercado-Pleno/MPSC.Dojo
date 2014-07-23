﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LBJC.NavegadorDeDados
{
	public partial class QueryResult : TabPage, IQueryResult
	{
		private static Int32 _quantidade = 0;
		private String originalQuery = String.Empty;
		private ClasseDinamica _classeDinamica = null;

		private ClasseDinamica ClasseDinamica { get { return (_classeDinamica ?? (_classeDinamica = new ClasseDinamica())); } }
		public String NomeDoArquivo { get; private set; }
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
				txtQuery.Text = File.ReadAllText(NomeDoArquivo = nomeDoArquivo);
				originalQuery = txtQuery.Text;
			}
			else
				NomeDoArquivo = String.Format("Query{0}.sql", ++_quantidade);
			UpdateDisplay();
		}

		public Boolean Salvar()
		{
			if (String.IsNullOrWhiteSpace(NomeDoArquivo) || NomeDoArquivo.StartsWith("Query") || !File.Exists(NomeDoArquivo))
				NomeDoArquivo = Extensions.GetFileToSave("Arquivos de Banco de Dados|*.sql") ?? NomeDoArquivo;

			if (!String.IsNullOrWhiteSpace(NomeDoArquivo) && !NomeDoArquivo.StartsWith("Query"))
			{
				File.WriteAllText(NomeDoArquivo, txtQuery.Text);
				originalQuery = txtQuery.Text;
			}
			UpdateDisplay();

			return !String.IsNullOrWhiteSpace(NomeDoArquivo) && !NomeDoArquivo.StartsWith("Query") && File.Exists(NomeDoArquivo);
		}

		private void txtQuery_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.KeyValue == 190) || (e.KeyValue == 194))
				AutoCompletar();
			else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.A))
				txtQuery.SelectAll();
			else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.S))
				Salvar();
			else if ((e.Modifiers == (Keys.Control | Keys.Shift)) && (e.KeyCode == Keys.T))
				ListarTabelas();
			else if (e.KeyCode == Keys.F5)
				Executar();
			else if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Y))
				Executar();
		}

		private void ListarTabelas()
		{
			//Banc
		}

		private void txtQuery_KeyUp(object sender, KeyEventArgs e)
		{
			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			Text = Path.GetFileName(NomeDoArquivo) + (txtQuery.Text != originalQuery ? " *" : "");
		}

		private void btBinding_Click(object sender, EventArgs e)
		{
			Binding();
		}

		public void Executar()
		{
			var query = QueryAtiva;
			if (!String.IsNullOrWhiteSpace(query))
			{
				try
				{
					query = Extensions.ConverterParametrosEmConstantes(txtQuery.Text, query);
					dgResult.DataSource = ClasseDinamica.Executar(query);
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
			if (dgResult.DataSource == null)
				dgResult.DataSource = result.ToList();
			else
			{
				var linha = dgResult.FirstDisplayedScrollingRowIndex;
				dgResult.DataSource = (dgResult.DataSource as IEnumerable<Object>).Union(result).ToList();
				if (linha >= 0)
					dgResult.FirstDisplayedScrollingRowIndex = linha;
			}
		}

		private void AutoCompletar()
		{
			try
			{
				var apelido = Extensions.ObterApelidoAntesDoPonto(txtQuery.Text, txtQuery.SelectionStart);
				var tabela = Extensions.ObterNomeTabelaPorApelido(txtQuery.Text, txtQuery.SelectionStart, apelido);
				var campos = Extensions.ListarColunasDasTabelas(ClasseDinamica.Conexao, tabela);
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

		public Boolean PodeFechar()
		{
			Boolean fechar = true;

			if (txtQuery.Text != originalQuery)
			{
				var dialogResult = MessageBox.Show(String.Format("O arquivo '{0}' foi alterado. Deseja Salvá-lo?", NomeDoArquivo), "Confirmação", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
					fechar = Salvar();
				else
					fechar = (dialogResult == DialogResult.No);
			}

			return fechar;
		}

		public void Fechar()
		{
			if (_classeDinamica != null)
			{
				_classeDinamica.Dispose();
				_classeDinamica = null;
			}

			originalQuery = null;

			txtQuery.Clear();
			txtQuery.Dispose();

			dgResult.DataSource = null;
			dgResult.Dispose();

			base.Dispose();
			GC.Collect();
		}

		public new Boolean Focus()
		{
			return txtQuery.Focus();
		}

	}

	public interface IQueryResult
	{
		String NomeDoArquivo { get; }
		void Executar();
		Boolean Salvar();
		Boolean PodeFechar();
		void Fechar();
		Boolean Focus();
	}

	public class NullQueryResult : IQueryResult
	{
		public String NomeDoArquivo { get { return null; } }
		public void Executar() { }
		public Boolean Focus() { return false; }
		public Boolean Salvar() { return false; }
		public Boolean PodeFechar() { return true; }
		public void Fechar() { _instance = null; }

		private static IQueryResult _instance;
		public static IQueryResult Instance { get { return _instance ?? (_instance = new NullQueryResult()); } }
	}
}