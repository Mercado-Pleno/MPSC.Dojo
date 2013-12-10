using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLTest
{
	public class Principal
	{
		public static void Main(String[] args)
		{
			ComoPegarAsMensagensDePrintsDoSqlServer.Testar(Tipo.Reader);
			Console.ReadKey();
		}
	}
}