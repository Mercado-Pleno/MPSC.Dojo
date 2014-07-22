using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using LBJC.NavegadorDeDados.Dados;
using LBJC.NavegadorDeDados.Infra;

namespace LBJC.NavegadorDeDados.View
{
	public partial class Autenticacao : Form
	{
		private const String arquivoConfig = "Autenticacao.txt";
		public IDbConnection Conexao { get; private set; }

		public Autenticacao()
		{
			InitializeComponent();
		}

		private void Autenticacao_Load(object sender, EventArgs e)
		{
			cbTipoBanco.DataSource = BancoDeDados<IDbConnection>.Conexoes;
			var config = Util.FileToArray(arquivoConfig);
			cbTipoBanco.SelectedIndex = Convert.ToInt32("0" + config[0]);
			txtServidor.Text = config[1];
			txtUsuario.Text = config[2];
			cbBancoSchema.Text = config[3];
		}

		private void Autenticacao_FormClosed(object sender, FormClosedEventArgs e)
		{
			Util.ArrayToFile(arquivoConfig, cbTipoBanco.SelectedIndex.ToString(), txtServidor.Text, txtUsuario.Text, cbBancoSchema.Text);
			cbTipoBanco.DataSource = null;
		}

		private void btConectar_Click(object sender, EventArgs e)
		{
			try
			{
				var bancoDeDados = cbTipoBanco.SelectedValue as IBancoDeDados;
				if (bancoDeDados != null)
					Conexao = bancoDeDados.ObterConexao(txtServidor.Text, cbBancoSchema.Text, txtUsuario.Text, txtSenha.Text);

				if (Conexao != null)
				{
					Conexao.Open();
					Conexao.Close();
					DialogResult = DialogResult.OK;
				}
			}
			catch (Exception exception)
			{
				if (Conexao != null)
					Conexao.Dispose();
				MessageBox.Show("Houve um problema ao tentar conectar ao banco de dados. Detalhes:\n\n" + exception.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		public static IDbConnection Dialog()
		{
			IDbConnection iDbConnection = null;
			var autenticacao = new Autenticacao();
			if (autenticacao.ShowDialog() == DialogResult.OK)
				iDbConnection = autenticacao.Conexao;
			autenticacao.Close();
			autenticacao.Dispose();
			autenticacao = null;
			return iDbConnection;
		}
	}
}