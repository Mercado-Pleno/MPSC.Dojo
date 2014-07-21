using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LBJC.NavegadorDeDados.Dados;

namespace LBJC.NavegadorDeDados.View
{
	public partial class Autenticacao : Form
	{
		public BancoDeDados<IDbConnection> BancoDeDados
		{
			get
			{
				return (cbTipoBanco.SelectedValue as BancoDeDados<IDbConnection>)
					.Configurar(txtServidor.Text, cbBancoSchema.Text, txtUsuario.Text, txtSenha.Text);
			}
		}

		public Autenticacao()
		{
			InitializeComponent();
		}

		private void Autenticacao_Load(object sender, EventArgs e)
		{
			cbTipoBanco.DataSource = BancoDeDados<IDbConnection>.Conexoes.ToList();
		}

		private void btConectar_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		public static IDbConnection Dialog()
		{
			var autenticacao = new Autenticacao();
			if (autenticacao.ShowDialog() == DialogResult.OK)
				return autenticacao.BancoDeDados.ObterConexao();
			return null;
		}
	}
}