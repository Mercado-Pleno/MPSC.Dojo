using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Aula.Curso.DojoOnLine;

namespace MPSC.Library.TestesUnitarios.SolutionTest.DojoOnLine
{
	[TestClass]
	public class EspiralTest
	{
		#region Matrizes de 1 Linha
		private const int[,] matriz_1x1 = 
		{
			{1}
		};
		private static int[,] matriz_1x2 = 
		{
			{1, 2}
		};
		private static int[,] matriz_1x3 = 
		{
			{1, 2, 3}
		};
		private static int[,] matriz_1x4 = 
		{
			{1, 2, 3, 4}
		};
		private static int[,] matriz_1x5 = 
		{
			{1, 2, 3, 4, 5}
		};
		private static int[,] matriz_1x6 = 
		{
			{1, 2, 3, 4, 5, 6}
		};
		#endregion

		#region Matrizes de 2 Linhas
		private static int[,] matriz_2x1 = 
		{
			{1},
			{2}
		};
		private static int[,] matriz_2x2 = 
		{
			{1, 2},
			{4, 3}
		};
		private static int[,] matriz_2x3 = 
		{
			{1, 2, 3},
			{6, 5, 4}
		};
		private static int[,] matriz_2x4 = 
		{
			{1, 2, 3, 4},
			{8, 7, 6, 5}
		};
		private static int[,] matriz_2x5 = 
		{
			{01, 2, 3, 4, 5},
			{10, 9, 8, 7, 6}
		};
		private static int[,] matriz_2x6 = 
		{
			{01, 02, 03, 4, 5, 6},
			{12, 11, 10, 9, 8, 7}
		};
		#endregion

		#region Matrizes de 3 Linhas
		private static int[,] matriz_3x1 = 
		{
			{1},
			{2},
            {3},
		};
		private static int[,] matriz_3x2 = 
		{
			{1, 2},
			{6, 3},
			{5, 4},
		};
		private static int[,] matriz_3x3 = 
		{
			{1, 2, 3},
			{8, 9, 4},
			{7, 6, 5},
		};
		private static int[,] matriz_3x4 = 
		{
			{01, 02, 03, 04},
			{10, 11, 12, 05},
			{09, 08, 07, 06},
		};
		private static int[,] matriz_3x5 = 
		{
			{01, 02, 03, 04, 05},
			{12, 13, 14, 15, 06},
			{11, 10, 09, 08, 07},
		};
		private static int[,] matriz_3x6 = 
		{
			{01, 02, 03, 04, 05, 06},
			{14, 15, 16, 17, 18, 07},
			{13, 12, 11, 10, 09, 08},
		};
		#endregion

		#region Matrizes de 4 Linhas
		private static int[,] matriz_4x1 = 
		{
			{1},
			{2},
            {3},
            {4},
		};
		private static int[,] matriz_4x2 = 
		{
			{1, 2},
			{8, 3},
			{7, 4},
			{6, 5},
		};
		private static int[,] matriz_4x3 = 
		{
			{01, 02, 03},
			{10, 11, 04},
			{09, 12, 05},
			{08, 07, 06},
		};
		private static int[,] matriz_4x4 = 
		{
			{01, 02, 03, 04},
			{12, 13, 14, 05},
			{11, 16, 15, 06},
			{10, 09, 08, 07},
		};
		private static int[,] matriz_4x5 = 
		{
			{01, 02, 03, 04, 05},
			{14, 15, 16, 17, 06},
			{13, 20, 19, 18, 07},
			{12, 11, 10, 09, 08},
		};
		private static int[,] matriz_4x6 = 
		{
			{01, 02, 03, 04, 05, 06},
			{16, 17, 18, 19, 20, 07},
			{15, 24, 23, 22, 21, 08},
			{14, 13, 12, 11, 10, 09},
		};

		#endregion

		#region Matrizes de 5 Linhas
		private static int[,] matriz_5x1 = 
		{
			{1},
			{2},
            {3},
            {4},
            {5},
		};

		private static int[,] matriz_5x2 = 
		{
			{01, 02},
			{10, 03},
			{09, 04},
			{08, 05},
			{07, 06},
		};

		private static int[,] matriz_5x3 = 
		{
			{01, 02, 03},
			{12, 13, 04},
			{11, 14, 05},
			{10, 15, 06},
            {09, 08, 07},
		};

		private static int[,] matriz_5x4 = 
		{
			{01, 02, 03, 04},
			{14, 15, 16, 05},
			{13, 20, 17, 06},
			{12, 19, 18, 07},
			{11, 10, 09, 08},
		};

		private static int[,] matriz_5x5 = 
		{
			{01, 02, 03, 04, 05},
			{16, 17, 18, 19, 06},
			{15, 24, 25, 20, 07},
			{14, 23, 22, 21, 08},
			{13, 12, 11, 10, 09},
		};

		private static int[,] matriz_5x6 = 
		{
			{01, 02, 03, 04, 05, 06},
			{18, 19, 20, 21, 22 ,07},
			{17, 28, 29, 30, 23 ,08},
			{16, 27, 26 ,25 ,24, 09},
			{15, 14 ,13 ,12 ,11 ,10},
		};
		#endregion

		#region Matrizes de 6 Linhas
		private static int[,] matriz_6x1 = 
		{
			{1},
			{2},
            {3},
            {4},
            {5},
            {6},
		};

		private static int[,] matriz_6x2 = 
		{
			{01, 02},
			{12, 03},
			{11, 04},
			{10, 05},
			{09, 06},
			{08, 07},
		};

		private static int[,] matriz_6x3 = 
		{
			{01, 02, 03},
			{14, 15, 04},
			{13, 16, 05},
			{12, 17, 06},
            {11, 18, 07},
            {10, 09, 08},
		};

		private static int[,] matriz_6x4 = 
		{
			{01, 02, 03, 04},
			{16, 17, 18, 05},
			{15, 24, 19, 06},
			{14, 23, 20, 07},
			{13, 22, 21, 08},
    		{12, 11, 10, 09},
		};

		private static int[,] matriz_6x5 = 
		{
			{01, 02, 03, 04, 05},
			{18, 19, 20, 21, 06},
			{17, 28, 29, 22, 07},
			{16, 27, 30, 23, 08},
			{15, 26, 25, 24, 09},
			{14, 13, 12, 11, 10},
		};

		private static int[,] matriz_6x6 = 
		{
			{01, 02, 03, 04, 05, 06},
			{20, 21, 22, 23, 24 ,07},
			{19, 32, 33, 34, 25 ,08},
			{18, 31, 36 ,35 ,26, 09},
			{17, 30 ,29 ,28 ,27 ,10},
    		{16, 15 ,14 ,13 ,12 ,11},
		};
		#endregion

		private Boolean CompararMatrizes(int[,] matrizCorreta, int[,] matrizGerada)
		{
			var linhas = matrizCorreta.GetUpperBound(0);
			var colunas = matrizCorreta.GetUpperBound(1);
			var retorno = ((matrizGerada.GetUpperBound(0) == linhas) && (matrizGerada.GetUpperBound(1) == colunas));

			for (int linha = 0; (linha <= linhas) && retorno; linha++)
			{
				for (int coluna = 0; (coluna <= colunas) && retorno; coluna++)
				{
					retorno = retorno && (matrizCorreta[linha, coluna] == matrizGerada[linha, coluna]);
					Assert.AreEqual(matrizCorreta[linha, coluna], matrizGerada[linha, coluna], String.Format("Diferente na posicao L={0} C={1} (X,Y = {0},{1})", linha + 1, coluna + 1, linha, coluna));
				}
			}

			return retorno;
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_1Por1()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(1, 1);
			Assert.IsTrue(CompararMatrizes(matriz_1x1, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_1Por2()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(1, 2);
			Assert.IsTrue(CompararMatrizes(matriz_1x2, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_1Por3()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(1, 3);
			Assert.IsTrue(CompararMatrizes(matriz_1x3, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_1Por4()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(1, 4);
			Assert.IsTrue(CompararMatrizes(matriz_1x4, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_1Por5()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(1, 5);
			Assert.IsTrue(CompararMatrizes(matriz_1x5, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_1Por6()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(1, 6);
			Assert.IsTrue(CompararMatrizes(matriz_1x6, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_2Por1()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(2, 1);
			Assert.IsTrue(CompararMatrizes(matriz_2x1, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_2Por2()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(2, 2);
			Assert.IsTrue(CompararMatrizes(matriz_2x2, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_2Por3()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(2, 3);
			Assert.IsTrue(CompararMatrizes(matriz_2x3, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_2Por4()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(2, 4);
			Assert.IsTrue(CompararMatrizes(matriz_2x4, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_2Por5()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(2, 5);
			Assert.IsTrue(CompararMatrizes(matriz_2x5, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_2Por6()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(2, 6);
			Assert.IsTrue(CompararMatrizes(matriz_2x6, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_3Por1()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(3, 1);
			Assert.IsTrue(CompararMatrizes(matriz_3x1, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_3Por2()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(3, 2);
			Assert.IsTrue(CompararMatrizes(matriz_3x2, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_3Por3()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(3, 3);
			Assert.IsTrue(CompararMatrizes(matriz_3x3, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_3Por4()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(3, 4);
			Assert.IsTrue(CompararMatrizes(matriz_3x4, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_3Por5()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(3, 5);
			Assert.IsTrue(CompararMatrizes(matriz_3x5, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_3Por6()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(3, 6);
			Assert.IsTrue(CompararMatrizes(matriz_3x6, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_4Por1()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(4, 1);
			Assert.IsTrue(CompararMatrizes(matriz_4x1, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_4Por2()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(4, 2);
			Assert.IsTrue(CompararMatrizes(matriz_4x2, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_4Por3()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(4, 3);
			Assert.IsTrue(CompararMatrizes(matriz_4x3, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_4Por4()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(4, 4);
			Assert.IsTrue(CompararMatrizes(matriz_4x4, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_4Por5()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(4, 5);
			Assert.IsTrue(CompararMatrizes(matriz_4x5, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_4Por6()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(4, 6);
			Assert.IsTrue(CompararMatrizes(matriz_4x6, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_5Por1()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(5, 1);
			Assert.IsTrue(CompararMatrizes(matriz_5x1, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_5Por2()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(5, 2);
			Assert.IsTrue(CompararMatrizes(matriz_5x2, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_5Por3()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(5, 3);
			Assert.IsTrue(CompararMatrizes(matriz_5x3, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_5Por4()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(5, 4);
			Assert.IsTrue(CompararMatrizes(matriz_5x4, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_5Por5()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(5, 5);
			Assert.IsTrue(CompararMatrizes(matriz_5x5, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_5Por6()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(5, 6);
			Assert.IsTrue(CompararMatrizes(matriz_5x6, matriz), "Matrizes com tamanhos incompatíveis.");

		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_6Por1()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(6, 1);
			Assert.IsTrue(CompararMatrizes(matriz_6x1, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_6Por2()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(6, 2);
			Assert.IsTrue(CompararMatrizes(matriz_6x2, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_6Por3()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(6, 3);
			Assert.IsTrue(CompararMatrizes(matriz_6x3, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_6Por4()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(6, 4);
			Assert.IsTrue(CompararMatrizes(matriz_6x4, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_6Por5()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(6, 5);
			Assert.IsTrue(CompararMatrizes(matriz_6x5, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_6Por6()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(6, 6);
			Assert.IsTrue(CompararMatrizes(matriz_6x6, matriz), "Matrizes com tamanhos incompatíveis.");
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_50Por5()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(50, 5);
			Assert.AreEqual(250, matriz.Cast<int>().Count(i => i > 0));
			Assert.AreEqual(250, matriz.Cast<int>().Max(i => i));
			Assert.AreEqual(250, matriz.Cast<int>().Distinct().Count());
		}

		[TestMethod]
		public void DeveMontarMatrizEspiral_2Por99()
		{
			var espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(2, 99);
			Assert.AreEqual(198, matriz.Cast<int>().Count(i => i > 0));
			Assert.AreEqual(198, matriz.Cast<int>().Max(i => i));
			Assert.AreEqual(198, matriz.Cast<int>().Distinct().Count());
		}
	}
}