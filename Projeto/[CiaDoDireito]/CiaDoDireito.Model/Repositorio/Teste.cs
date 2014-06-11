using System;
using System.Data;
using System.Data.SqlClient;

namespace DireitoECia
{
	public class Teste
	{
		public Teste()
		{
			// Pode estar no App ou Web .config
			var strConnection = "Data Source=Servidor;Initial Catalog=BancoDeDados;User ID=Usuario;Password=Senha;MultipleActiveResultSets=True;";
			var sqlConnection = new SqlConnection(strConnection);
			sqlConnection.Open();

			var sqlCommand = sqlConnection.CreateCommand();
			sqlCommand.CommandType = CommandType.Text;
			sqlCommand.CommandText = "Select * From Tabela Where (Campo = @variavel)";

			var sqlParam = sqlCommand.CreateParameter();
			sqlParam.ParameterName = "Variavel";
			sqlParam.Value = "Gil";
			sqlParam.DbType = DbType.String;
			sqlParam.Direction = ParameterDirection.Input;
			sqlCommand.Parameters.Add(sqlParam);

			var dbDataReader = sqlCommand.ExecuteReader();
			while (dbDataReader.Read())
			{
				int idade = Convert.ToInt32(dbDataReader["Idade"]);

				var indice = dbDataReader.GetOrdinal("Nascimento");
				if (!dbDataReader.IsDBNull(indice))
				{
					DateTime nascimento = Convert.ToDateTime(dbDataReader[indice]);
				}
			}
			dbDataReader.Close();
			dbDataReader.Dispose();
			sqlCommand.Dispose();
			sqlConnection.Close();
			sqlConnection.Dispose();
		}
	}
}