using System.Diagnostics;
using System;

namespace MPSC.Library.Aula.Curso.DojoOnLine
{

	public class Principal
	{
		static void Main(string[] args)
		{
			Espiral espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(5, 6);
			var igual = espiral.comparar(espiral.matriz_5x6, matriz);
			Console.WriteLine(igual);
		}
	}
	public enum Direcao
	{
		Direita = 1,
		ABaixo = 2,
		Esquerda = 3,
		Acima = 4,
	}

	public class Espiral
	{

		public int[,] matriz_1x1 = 
		{
			{1}
		};

		public int[,] matriz_1x2 = 
		{
			{1, 2}
		};

		public int[,] matriz_2x1 = 
		{
			{1},
			{2}
		};

		public int[,] matriz_2x2 = 
		{
			{1, 2},
			{4, 3}
		};

		public int[,] matriz_5x6 = 
		{
			{01, 02, 03, 04, 05, 06},
			{18, 19, 20, 21, 22 ,07},
			{17, 28, 29, 30, 23 ,08},
			{16, 27, 26 ,25 ,24, 09},
			{15, 14 ,13 ,12 ,11 ,10},
		};

		public Boolean comparar(int[,] matrizCorreta, int[,] matrizGerada)
		{
			var linhas = matrizCorreta.GetUpperBound(0);
			var colunas = matrizCorreta.GetUpperBound(1);
			var retorno = matrizGerada.GetUpperBound(0) == linhas && matrizGerada.GetUpperBound(1) == colunas;
			for (int i = 0; i <= linhas && retorno; i++)
			{
				for (int j = 0; j <= colunas && retorno; j++)
				{
					retorno = retorno && (matrizGerada[i, j] == matrizCorreta[i, j]);
				}
			}
			return retorno;
		}

		private Direcao direcao = Direcao.Direita;

		private void MudarDirecao()
		{
			if (direcao == Direcao.Direita)
				coluna--;
			else if (direcao == Direcao.ABaixo)
				linha--;
			else if (direcao == Direcao.Esquerda)
				coluna++;
			else if (direcao == Direcao.Acima)
				linha++;

			var direcaoAtual = (int)direcao;
			var novaDirecao = direcaoAtual % 4 + 1;
			direcao = (Direcao)novaDirecao;

			if (direcao == Direcao.Direita)
				coluna++;
			else if (direcao == Direcao.ABaixo)
				linha++;
			else if (direcao == Direcao.Esquerda)
				coluna--;
			else if (direcao == Direcao.Acima)
				linha--;
		}

		int linha = 0;
		int coluna = 0;
		int voltaLinha = 0;
		int voltaColuna = 0;

		public int[,] GerarMatrizEspiral(int quantidadeDeLinhas, int quantidadeDeColunas)
		{
			var matriz = new int[quantidadeDeLinhas, quantidadeDeColunas];
			var contador = 0;

			while (contador < quantidadeDeLinhas * quantidadeDeColunas)
			{
				if ((coluna >= 0) && (linha >= 0) && (coluna < quantidadeDeColunas) && (linha < quantidadeDeLinhas))
					matriz[linha, coluna] = ++contador;

				if (direcao == Direcao.Direita)
					coluna++;
				else if (direcao == Direcao.ABaixo)
					linha++;
				else if (direcao == Direcao.Esquerda)
					coluna--;
				else if (direcao == Direcao.Acima)
					linha--;

				if ((coluna < voltaColuna) || (linha < voltaLinha) || (coluna == quantidadeDeColunas - voltaColuna) || (linha == quantidadeDeLinhas - voltaLinha))
				{
					MudarDirecao();
					if (direcao == Direcao.Acima)
						voltaLinha++;
					else if (direcao == Direcao.Direita)
						voltaColuna++;
				}
			}


			return matriz;
		}

		public void Print(int[,] matriz)
		{
			foreach (var item in matriz)
			{
				Debug.WriteLine(item);
			}
		}
	}
}
