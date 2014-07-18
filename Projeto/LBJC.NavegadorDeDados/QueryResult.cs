using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LBJC.NavegadorDeDados
{
	public partial class QueryResult : TabPage
	{
		private static Int32 _quantidade = 1;

		private Conexao _conexao = null;
		private ClasseDinamica _classeDinamica = null;

		private Conexao Conexao { get { return (_conexao ?? (_conexao = new Conexao())); } }
		private ClasseDinamica ClasseDinamica { get { return (_classeDinamica ?? (_classeDinamica = new ClasseDinamica(_dataReader))); } }

		private String QueryAtiva { get { return ((txtQuery.SelectedText.Length > 1) ? txtQuery.SelectedText : txtQuery.Text); } }

		public QueryResult()
		{
			InitializeComponent();
			this.Text = "Query" + _quantidade++;
			this.txtQuery.Text = "Select * From Pessoa Order By NomePessoa";
		}

		public void Executar()
		{
			if (!String.IsNullOrWhiteSpace(QueryAtiva))
			{
				_dataReader = Conexao.Executar(QueryAtiva);
				ClasseDinamica.Reset(_dataReader);
				dgResult.DataSource = null;
				Binding();
			}
		}

		private void txtQuery_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
				Executar();
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

		private void Binding()
		{
			var result = ClasseDinamica.Transformar();
			dgResult.DataSource = (dgResult.DataSource as IEnumerable<Object> ?? new List<Object>()).Union(result).ToList();
		}
	}
}