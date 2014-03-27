
using System.Diagnostics;
namespace ProjetoTest
{
    public enum Direcao
    {
        Direita = 1,
        ABaixo = 2,
        Esquerda = 3,
        Acima = 4,
    }

    public class Espiral
    {
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
