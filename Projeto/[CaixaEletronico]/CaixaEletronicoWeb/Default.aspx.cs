using MP.Library.CaixaEletronico;
using MP.Library.CaixaEletronico.Notas;
using System;

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

			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);
			try
			{
				Int32 vValorSaque = valorDesejado();

				var notas = saque.Sacar(vValorSaque);
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