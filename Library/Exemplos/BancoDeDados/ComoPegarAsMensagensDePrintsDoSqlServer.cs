namespace MPSC.Library.Exemplos.BancoDados
{
	using System;
	using System.Data;
	using System.Data.SqlClient;

	public enum Tipo
	{
		Scalar,
		NonQuery,
		Reader,
		DataSet
	}

	public class ComoPegarAsMensagensDePrintsDoSqlServer : IExecutavel
	{
		private static String _stringConexao = @"Persist Security Info=True;Data Source=127.0.0.1;Initial Catalog=Master;User ID=sa;Password=SenhaDeAcessoSuperSecreta;MultipleActiveResultSets=True;";
		private static String _comandoSQL = @"
Set NoCount On;
Print 'Bom dia pessoal!';
Select GetDate() as Data;
Print 'Valeu pessoal.';
";
		public ItemMenu Executar()
		{
			Testar(_stringConexao, _comandoSQL, Tipo.NonQuery);
			return null;
		}

		public static void Testar(String stringConexao, String comandoSQL, Tipo tipo)
		{
			using (SqlConnection vSqlConnection = new SqlConnection(stringConexao))
			{
				vSqlConnection.Open();
				vSqlConnection.InfoMessage += MostrarPrints;

				var vSqlCommand = vSqlConnection.CreateCommand();
				vSqlCommand.CommandText = comandoSQL;

				switch (tipo)
				{
					case Tipo.Scalar: // Só mostra a primeira Mensagem
						MostrarDados(vSqlCommand.ExecuteScalar());
						break;

					case Tipo.NonQuery: // Mostra as duas mensagens, mas não é possível saber a data e hora
						MostrarDados(vSqlCommand.ExecuteNonQuery());
						break;

					case Tipo.Reader: // Assim, mostra tudo, na ordem de execução dos comandos
						var vSqlDataReader = vSqlCommand.ExecuteReader();
						do
						{
							while (vSqlDataReader.Read())
								MostrarDados(vSqlDataReader.GetDateTime(0));
						} while (vSqlDataReader.NextResult()); // <- Se não tiver esta linha, só mostra a mensagem do primeiro Print.

						vSqlDataReader.Close();
						vSqlDataReader.Dispose();
						break;

					case Tipo.DataSet: // Assim, mostra todas as mensagens, e por fim, a Data
						var vDataSet = new DataSet();
						var vSqlDataAdapter = new SqlDataAdapter(vSqlCommand);
						vSqlDataAdapter.Fill(vDataSet, "DataEHoraDoServidor");
						var vDataTable = vDataSet.Tables["DataEHoraDoServidor"];
						var vDataRow = vDataTable.Rows[0];
						MostrarDados(vDataRow["Data"]);

						vDataTable.Clear();
						vDataTable.Dispose();

						vDataSet.Clear();
						vDataSet.Dispose();

						vSqlDataAdapter.Dispose();
						break;
					default:
						break;
				}

				vSqlCommand.Dispose();
				vSqlConnection.Close();
			}
		}

		private static void MostrarPrints(Object sender, SqlInfoMessageEventArgs e)
		{
			Console.WriteLine(e.Message);
		}

		private static void MostrarDados(Object dados)
		{
			if (dados is DateTime)
				MostrarDados(Convert.ToDateTime(dados));
			else
				Console.WriteLine(dados);
		}

		private static void MostrarDados(DateTime dataEHora)
		{
			Console.WriteLine(dataEHora.ToString("dd/MM/yyyy HH:mm:ss:fff"));
		}
	}
}