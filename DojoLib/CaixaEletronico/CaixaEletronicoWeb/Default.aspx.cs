using System;
using System.Collections.Generic;
using CaixaEletronico;

namespace CaixaEletronicoWeb
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		private int valorDesejado()
		{
			Int32 vValorSaque = 0;
			if (!Int32.TryParse(textValorSaque.Text, out vValorSaque))
				throw new Exception("Deve ser digitado um valor numérico inteiro!!");
			return vValorSaque;
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			/* 
			 * Verificar se o valor digitado só tem numeros...
			 * Converter o valor digitado em int
			 * Usar try catch pra capturar as mensagens de erro e validaçoes
			 * preencher a lista com as notas retornadas.
			 */
			lblInformativo.Text = "";
			lbListadeNotas.Items.Clear();

			Saque saque = new Saque();
			try
			{
				Int32 vValorSaque = valorDesejado();

				List<Nota> notas = saque.Sacar(vValorSaque);

				foreach (Nota nota in notas)
					lbListadeNotas.Items.Add(nota.ToString());

			}
			catch (Exception exception)
			{
				lblInformativo.Text = exception.Message;
			}
		}
	}
}
