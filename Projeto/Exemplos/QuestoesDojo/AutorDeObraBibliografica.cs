using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class AutorDeObraBibliograficaRunner : IExecutavel
	{
		public void Executar()
		{
			Console.Write("Informe um nome de autor: ");
			var nome = Console.ReadLine();
			var autor = new AutorDeObraBibliografica(nome);
			Console.WriteLine("Esse autor deve ser referenciado assim: {0}", autor.Referencia);
		}
		
		public String[] DividirEmNomes(String nome)
		{
			return new AutorDeObraBibliografica(nome).Nomes;
		}

		public String SobreNome(String nome)
		{
			return new AutorDeObraBibliografica(nome).SobreNome;
		}

		public String PrimeirosNomes(String nome)
		{
			return new AutorDeObraBibliografica(nome).Nome;
		}

		public String Formatar(String nome)
		{
			return new AutorDeObraBibliografica(nome).Referencia;
		}

	}

	public class AutorDeObraBibliografica
	{
		public readonly String[] Nomes;
		public readonly String[] SobreNomes;
		public readonly String Nome;
		public readonly String SobreNome;
		public readonly String Referencia;

		public AutorDeObraBibliografica(String nome)
		{
			Nomes = DividirEmNomes(nome);
			SobreNomes = (Nomes.Length > 0) ? SobreNomeComposto(Nomes, Nomes.Length - 1, Nomes.Length > 2).ToArray() : new String[0];

			SobreNome = String.Join(" ", SobreNomes);
			Nome = String.Join(" ", Nomes.Where(n => !In(n, SobreNomes)));

			Referencia = (String.IsNullOrWhiteSpace(Nome)) ? SobreNome.ToUpper() : String.Format("{0}, {1}", SobreNome.ToUpper(), Capitalizar(Nome.ToLower()));
		}

		private String Capitalizar(String nome)
		{
			var nomes = DividirEmNomes(nome);
			for (var i = 0; i < nomes.Length; i++)
			{
				if (!EhComplemento(nomes[i]))
					nomes[i] = ToFirstUpper(nomes[i]);
			}
			return String.Join(" ", nomes);
		}

		private IEnumerable<String> SobreNomeComposto(String[] nomes, Int32 posicao, Boolean composto)
		{
			var sobreNome = nomes[posicao];
			if (composto && EhComposto(sobreNome))
				yield return nomes[posicao - 1];

			yield return sobreNome;
		}

		private String ToFirstUpper(String nome)
		{
			return nome.Substring(0, 1).ToUpper() + nome.Substring(1).ToLower();
		}

		private Boolean EhComplemento(String nome)
		{
			return In(nome.ToLower(), "e", "da", "de", "do", "das", "dos");
		}

		private Boolean EhComposto(String sobreNome)
		{
			return In(sobreNome.ToUpper(), "FILHO", "FILHA", "NETO", "NETA", "SOBRINHO", "SOBRINHA", "JUNIOR");
		}

		private Boolean In(String item, params String[] lista)
		{
			return lista.Contains(item);
		}

		public String[] DividirEmNomes(String nome)
		{
			return nome.Split(new[] { " ", "\t", "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}