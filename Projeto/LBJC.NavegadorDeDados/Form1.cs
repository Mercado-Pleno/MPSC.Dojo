using System;
using System.Windows.Forms;

namespace LBJC.NavegadorDeDados
{
	public partial class Form1 : Form
	{
		private IQueryResult ActiveTab { get { return (tabQueryResult.TabPages.Count > 0) ? tabQueryResult.TabPages[tabQueryResult.TabIndex] as IQueryResult : NullQueryResult.Instance; } }

		public Form1()
		{
			InitializeComponent();
		}

		private void btNovoDocumento_Click(object sender, EventArgs e)
		{
			tabQueryResult.Controls.Add(new QueryResult());
		}

		private void btExecutar_Click(object sender, EventArgs e)
		{
			ActiveTab.Executar();
		}
	}
}