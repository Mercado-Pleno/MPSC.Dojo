using System;
using System.Windows.Forms;
using DireitoECia.Controller;
using DireitoECia.Model.Dominio;

namespace DireitoECia
{
	public partial class frmAdvogado : Form
	{
		public frmAdvogado()
		{
			InitializeComponent();
		}

		private void btnIncluir_Click(object sender, EventArgs e)
		{
			var advogado = new Advogado() { Nome = txtNome.Text };
			try
			{
				ApplicationController.Instancia.Incluir(advogado);
				txtId.Text = advogado.Id.ToString();
				var listaAdvogados = ApplicationController.Instancia.ListarTodosAdvogados();

				dgAdvogados.DataSource = null;
				dgAdvogados.DataSource = listaAdvogados;

			}
			catch (Exception exception)
			{
				MessageBox.Show("Erro: Não foi possível incluir o advogado. Detalhes:\n" + exception.Message);
			}
		}
	}
}
