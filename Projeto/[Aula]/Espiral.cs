using System;

namespace MPSC.Library.Aula.Curso.DojoOnLine
{
	public class Espiral
	{
		public int[,] GerarMatrizEspiral(int quantidadeDeLinhas, int quantidadeDeColunas)
		{
			var matriz = new int[quantidadeDeLinhas, quantidadeDeColunas];
			var linha = new Parametro() { inicio = 1, termino = quantidadeDeLinhas };
			var coluna = new Parametro() { posicao = -1, termino = quantidadeDeColunas };
			var contador = 0;

			while (contador < quantidadeDeLinhas * quantidadeDeColunas)
			{
				for (coluna.Inicializar(); coluna.OK(); coluna.Proximo())
					matriz[linha.posicao, coluna.posicao] = ++contador;
				coluna.MudarDirecao();

				for (linha.Inicializar(); linha.OK(); linha.Proximo())
					matriz[linha.posicao, coluna.posicao] = ++contador;
				linha.MudarDirecao();
			}

			return matriz;
		}

		public void Print(int[,] matriz)
		{
			var linhas = matriz.GetUpperBound(0);
			var colunas = matriz.GetUpperBound(1);

			Console.Clear();
			for (int linha = 0; linha <= linhas; linha++)
			{
				for (int coluna = 0; coluna <= colunas; coluna++)
					Console.Write(matriz[linha, coluna].ToString().PadLeft(4));
				Console.WriteLine();
			}
		}

		private class Parametro
		{
			private int direcao = 1;
			public int inicio = 0;
			public int posicao = 0;
			public int termino = 0;

			public void Inicializar()
			{
				Proximo();
			}

			public void Proximo()
			{
				posicao += direcao;
			}

			public void MudarDirecao()
			{
				if (direcao > 0)
					termino--;
				else
					inicio++;
				direcao = -direcao;
				Proximo();
			}

			public Boolean OK()
			{
				return (posicao < termino) && ((direcao > 0) || (posicao >= inicio));
			}
		}
	}
}