using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LBJC.NavegadorDeDados.Dados;

namespace LBJC.NavegadorDeDados.View
{
	public partial class Autenticacao : Form
	{
		private IBancoDeDados BancoDeDados { get { return cbTipoBanco.SelectedValue as IBancoDeDados; } }
		public IDbConnection Conexao { get; private set; }

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
			try
			{
				var bancoDeDados = BancoDeDados;
				if (bancoDeDados != null)
					Conexao = bancoDeDados.ObterConexao(txtServidor.Text, cbBancoSchema.Text, txtUsuario.Text, txtSenha.Text);

				if (Conexao != null)
				{
					Conexao.Open();
					Conexao.Close();
					DialogResult = DialogResult.OK;
				}
			}
			catch (Exception)
			{

			}
		}

		public static IDbConnection Dialog()
		{
			var autenticacao = new Autenticacao();
			if (autenticacao.ShowDialog() == DialogResult.OK)
				return autenticacao.Conexao;
			return null;
		}
	}
}