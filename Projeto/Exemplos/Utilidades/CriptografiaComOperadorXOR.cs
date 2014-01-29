namespace MPSC.Library.Exemplos.Utilidades
{
	using System;
	using System.Windows.Forms;

	public class CriptografiaComOperadorXOR : IExecutavel
	{
		public void Executar()
		{
			String chave = "ChaveUltraSecretaQueNãoPodeSerReveladaàNinguém";
			String original = "Abóbora, Melão e Melancia";
			String criptografado = Cripto(original, chave);
			String decriptografado = Cripto(criptografado, chave);
			MessageBox.Show(
				String.Format(
					"Original: ({0}) {1}\n" +
					"Criptografado: ({2}) {3}\n" +
					"Original = Cripto(Criptografado): {4}",
					original.Length, original,
					criptografado.Length, criptografado,
					original == decriptografado
				)
			);
		}

		public static String Cripto(String valor, String chave)
		{
			String retorno = String.Empty;
			chave += " "; //para ter pelo menos um caracter na chave e não gerar erro...
			int contador = 0;

			foreach (Char v in valor)
				retorno += XOR(v, chave[contador++ % chave.Length]);

			return retorno;
		}

		private static string XOR(char valor, char chave)
		{
			int criptografiaXOR = ((int)valor) ^ ((int)chave);
			return ((char)criptografiaXOR).ToString();
		}
	}
}