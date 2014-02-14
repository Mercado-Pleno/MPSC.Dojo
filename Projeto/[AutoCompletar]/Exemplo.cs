using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCompletar
{
	public partial class Exemplo : Form
	{
		private String _connectionString = "Persist Security Info=True;Data Source=127.0.0.1;Initial Catalog=BancoDeDados;User ID=Usuario;Password=SenhaDeAcessoSuperSecreta;MultipleActiveResultSets=True;";
		private DbConnection _dbConnection;
		private String _ultimaPesquisaExecutada = "";

		public Exemplo()
		{
			InitializeComponent();
			cbNome.ValueMember = "Id";
			cbNome.DisplayMember = "Nome";

			_dbConnection = new SqlConnection(_connectionString);
			_dbConnection.Open();
		}

		private String ObterNomeDigitado()
		{
			String vNomeParcial = String.Empty;
			try { vNomeParcial = cbNome.Text.Trim(); }
			catch (Exception) { vNomeParcial = _ultimaPesquisaExecutada; }
			return vNomeParcial;
		}

		private void cbNome_KeyUp(object sender, KeyEventArgs e)
		{
			String vNomeParcial = ObterNomeDigitado();

			if ((vNomeParcial.Length > 0) && (vNomeParcial != _ultimaPesquisaExecutada) && TeclasValidas(e.KeyCode))
			{
				_ultimaPesquisaExecutada = vNomeParcial;
				var vLista = PesquisarNoRepositorio(vNomeParcial);
				ProcessarPesquisa(vLista);
			}

			cbNome.DroppedDown = (vNomeParcial.Length > 0) && (cbNome.Items.Count > 0) && (e.KeyCode != Keys.Enter) && (e.KeyCode != Keys.Escape);
			if (!cbNome.DroppedDown)
				cbNome.Items.Clear();
		}

		private bool TeclasValidas(Keys keys)
		{
			return (keys != Keys.Escape)
				&& (keys != Keys.Home)
				&& (keys != Keys.End)
				&& (keys != Keys.PageUp)
				&& (keys != Keys.PageDown)
				&& (keys != Keys.Up)
				&& (keys != Keys.Down)
				&& (keys != Keys.Left)
				&& (keys != Keys.Right)
				;
		}

		private void ProcessarPesquisa(List<Object> lista)
		{
			cbNome.DroppedDown = true;
			cbNome.Items.Clear();
			foreach (var pessoa in lista)
				cbNome.Items.Add(pessoa);
			cbNome.SelectionStart = cbNome.Text.Length;
			cbNome.SelectionLength = 0;
		}

		private List<Object> PesquisarNoRepositorio(String nomeParcial)
		{
			var lista = new List<Object>();
			var vDbCommand = CriarComando(_dbConnection, "Select Top 10 Id, Nome From Pessoa Where (Nome LIKE @Nome) Order By Nome;");
			var vDbParameter = AdicionarParametro(vDbCommand, ParameterDirection.Input, DbType.String, "Nome", "%" + nomeParcial + "%");
			var vDbDataReader = vDbCommand.ExecuteReader();

			while (vDbDataReader.Read())
				lista.Add(
					new
					{
						Id = Convert.ToInt32(vDbDataReader["Id"]),
						Nome = Convert.ToString(vDbDataReader["Nome"])
					}
				);

			vDbDataReader.Close();
			vDbDataReader.Dispose();
			vDbCommand.Dispose();
			return lista;
		}

		private DbCommand CriarComando(DbConnection dbConnection, String comandoSQL)
		{
			var vDbCommand = _dbConnection.CreateCommand();
			vDbCommand.CommandText = "Select Id, Nome From Pessoa Where Nome LIKE @Nome;";
			vDbCommand.CommandType = CommandType.Text;
			return vDbCommand;
		}

		private DbParameter AdicionarParametro(DbCommand dbCommand, ParameterDirection direcao, DbType tipo, String nomeDoParametro, Object valor)
		{
			var vDbParameter = dbCommand.CreateParameter();
			vDbParameter.ParameterName = nomeDoParametro;
			vDbParameter.Direction = direcao;
			vDbParameter.DbType = tipo;
			vDbParameter.Value = valor;
			dbCommand.Parameters.Add(vDbParameter);
			return vDbParameter;
		}
	}
}