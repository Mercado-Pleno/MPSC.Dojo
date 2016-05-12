using System;
using System.Globalization;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class AutorDeObraBibliografica
	{

		public String[] DividirEmNomes(String nome)
		{
			return nome.Split(new[] { " ", "\t", "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
		}

		public String SobreNome(String nome)
		{
			var nomes = DividirEmNomes(nome);
			return nomes.Length > 0 ? SobreNomeComposto(nomes, nomes.Length - 1, nomes.Length > 2) : String.Empty;
		}

		public String PrimeirosNomes(String nome)
		{
			var nomes = DividirEmNomes(nome);
			var sobreNome = DividirEmNomes(SobreNome(nome));
			return String.Join(" ", nomes.Where(n => !In(n, sobreNome)));
		}

		public String Formatar(String nome)
		{
			var qtdNomes = DividirEmNomes(PrimeirosNomes(nome)).Length;
			return qtdNomes > 0 ? String.Format("{0}, {1}", SobreNome(nome).ToUpper(), Capitalizar(PrimeirosNomes(nome))) : SobreNome(nome).ToUpper();
		}

		private String Capitalizar(String nome)
		{
			var nomes = DividirEmNomes(nome.ToLower());
			for (int i = 0; i < nomes.Length; i++)
			{
				if (!In(nomes[i], "e", "da", "de", "do", "das", "dos"))
					nomes[i] = ToFirstUpper(nomes[i]);
			}

			return String.Join(" ", nomes);
		}

		private String ToFirstUpper(String nome)
		{
			return nome[0].ToString().ToUpper() + nome.Substring(1).ToLower();
		}


		private String SobreNomeComposto(String[] nomes, Int32 posicao, Boolean composto)
		{
			var sobreNome = nomes[posicao];
			if (composto && EhComposto(sobreNome))
				sobreNome = String.Format("{0} {1}", nomes[posicao - 1], nomes[posicao]);
			return sobreNome;
		}

		private Boolean EhComposto(String sobreNome)
		{
			return In(sobreNome.ToUpper(), "FILHO", "FILHA", "NETO", "NETA", "SOBRINHO", "SOBRINHA", "JUNIOR");
		}

		private Boolean In(String sobreNome, params String[] sobrenomes)
		{
			return sobrenomes.Contains(sobreNome);
		}

	}
}
