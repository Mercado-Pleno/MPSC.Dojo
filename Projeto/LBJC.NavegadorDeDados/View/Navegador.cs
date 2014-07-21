using System;
using System.Windows.Forms;

namespace LBJC.NavegadorDeDados
{
	public partial class Navegador : Form
	{
		private IQueryResult ActiveTab { get { return (tabQueryResult.TabPages.Count > 0) ? tabQueryResult.TabPages[tabQueryResult.SelectedIndex] as IQueryResult : NullQueryResult.Instance; } }

		public Navegador()
		{
			InitializeComponent();
		}

		private void btNovoDocumento_Click(object sender, EventArgs e)
		{
			tabQueryResult.Controls.Add(new QueryResult(null));
			tabQueryResult.SelectedIndex = tabQueryResult.TabCount - 1;
		}

		private void btAbrirDocumento_Click(object sender, EventArgs e)
		{
			var arquivos = Extensions.GetFilesToOpen("Arquivos de Banco de Dados|*.sql;*.qry");
			foreach (var arquivo in arquivos)
				tabQueryResult.Controls.Add(new QueryResult(arquivo));
			
			tabQueryResult.SelectedIndex = tabQueryResult.TabCount - 1;
		}

		private void btSalvarDocumento_Click(object sender, EventArgs e)
		{
			ActiveTab.Salvar();
		}

		private void btSalvarTodos_Click(object sender, EventArgs e)
		{
			Boolean salvouTodos = true;
			foreach (IQueryResult queryResult in tabQueryResult.Controls)
				salvouTodos = salvouTodos && queryResult.Salvar();
		}

		private void btExecutar_Click(object sender, EventArgs e)
		{
			ActiveTab.Executar();
		}

		private void btFechar_Click(object sender, EventArgs e)
		{
			ActiveTab.Fechar();
		}
	}
}