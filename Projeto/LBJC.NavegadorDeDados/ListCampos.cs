using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LBJC.NavegadorDeDados
{
	public partial class ListCampos : Form
	{
		public ListCampos() { InitializeComponent(); }

		public ListCampos(IList<String> lista)
		{
			InitializeComponent();
			listBox.DataSource = lista;
			listBox.Focus();
		}

		private void listBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				DialogResult = DialogResult.Cancel;
			else if (e.KeyCode == Keys.Enter)
				DialogResult = DialogResult.OK;
		}

		private void Selecionar(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private String ObterCampoSelecionado()
		{
			String retorno = Convert.ToString(listBox.SelectedItem);
			Close();
			return retorno;
		}

		private string Fechar()
		{
			Close();
			return String.Empty;
		}

		public static String Exibir(IList<String> lista)
		{
			var listaCampos = new ListCampos(lista);
			return (listaCampos.ShowDialog() == DialogResult.OK) ? listaCampos.ObterCampoSelecionado() : listaCampos.Fechar();
		}
	}
}