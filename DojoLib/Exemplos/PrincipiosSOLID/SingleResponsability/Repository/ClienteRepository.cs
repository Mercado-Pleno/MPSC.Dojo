using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Repository
{
	public class ClienteRepository: Repositorio<Cliente>
	{
		public ClienteRepository()
			: base(new SqlConnection())
		{
		}

		public override void Incluir(Cliente cliente)
		{
			ProcessarSQL(cliente, "Insert Into Cliente (Id, Nome) Values (@Id, @Nome);");
		}

		public override void Alterar(Cliente cliente)
		{
			ProcessarSQL(cliente, "Update Cliente Set Nome = @Nome Where Id = @Id;");
		}

		private void ProcessarSQL(Cliente cliente, String comandoSQL)
		{
			DbCommand comando = CriarComando(comandoSQL);
			AdicionarParametro(comando, "Id", DbType.Int32, ParameterDirection.Input, cliente.Id);
			AdicionarParametro(comando, "Nome", DbType.String, ParameterDirection.Input, cliente.Nome);
			comando.ExecuteNonQuery();
		}

		private DbCommand CriarComando(String comandoSQL)
		{
			DbCommand comando = conexao.CreateCommand();
			comando.CommandText = comandoSQL;
			comando.CommandType = CommandType.Text;
			return comando;
		}

		private void AdicionarParametro(DbCommand comando, String nomeDoParametro, DbType tipoDoParametro, ParameterDirection direcaoDoParametro, Object valorDoParametro)
		{
			var parametro = comando.CreateParameter();
			parametro.ParameterName = nomeDoParametro;
			parametro.Value = valorDoParametro;
			parametro.DbType = tipoDoParametro;
			parametro.Direction = direcaoDoParametro;
			comando.Parameters.Add(parametro);
		}


		public void emitirRelatorio_Click(Object sender, EventArgs e)
		{
			EmitirRelatorio();
		}

		public void gravar_Click(Object sender, EventArgs e)
		{
			ProcessarGravacao();
		}

		private void EmitirRelatorio()
		{
			ProcessarGravacao();
			/* 
			 * MOstra mensagem de sucesso ou erro
				Emite Relatorio 
			 */
		}		
		private void ProcessarGravacao()
		{
			/*
			 * if (Valida os camposa na tela)
			 * if (Valida os camposa na tela)
			 * if (Valida os camposa na tela)
			 * preenche a entidade
			 * Chama o metodo grava
			 */

		}

	}
}