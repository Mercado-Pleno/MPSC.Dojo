using System;

namespace MPSC.Library.Aula.Curso.DojoOnLine
{

    public class Principal
    {
        static void Main(string[] args)
        {
            Espiral espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(5, 6);
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
        private Direcao direcao = Direcao.Direita;
        int linha = 0;
        int coluna = 0;
        int voltaLinha = 0;
        int voltaColuna = 0;


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

        class Parametro
        {
            private int direcao = 1;
            public int inicio = 0;
            public int posicao = 0;
            public int termino = 0;

            public void Inicializar()
            {
                posicao = direcao == 1 ? inicio : termino - 1;
            }

            public void Proximo()
            {
                posicao += direcao;
                if ((posicao == inicio) || (posicao == termino))
                    direcao = -direcao;
            }

            public Boolean OK()
            {
                Boolean retorno = direcao > 0 ? (posicao >= inicio) && (posicao < termino) : (posicao > inicio) && (posicao <= termino);
                if (posicao == termino)
                    termino--;
                if (posicao == inicio)
                    inicio++;
                if (!retorno) posicao+= direcao;
                return retorno;
            }
        }

        public int[,] GerarMatrizEspiralV2(int quantidadeDeLinhas, int quantidadeDeColunas)
        {
            var matriz = new int[quantidadeDeLinhas, quantidadeDeColunas];
            var linha = new Parametro() { inicio = 1, termino = quantidadeDeLinhas };
            var coluna = new Parametro() { termino = quantidadeDeColunas };
            var contador = 0;

            while (contador < quantidadeDeLinhas * quantidadeDeColunas)
            {
                for (coluna.Inicializar(); coluna.OK(); coluna.Proximo())
                    matriz[linha.posicao, coluna.posicao] = ++contador;

                for (linha.Inicializar(); linha.OK(); linha.Proximo())
                    matriz[linha.posicao, coluna.posicao] = ++contador;
            }

            return matriz;
        }

        public void Print(int[,] matriz)
        {
            var linhas = matriz.GetUpperBound(0);
            var colunas = matriz.GetUpperBound(1);

            for (int linha = 0; linha <= linhas; linha++)
            {
                for (int coluna = 0; coluna <= colunas; coluna++)
                {
                    Console.Write(matriz[linha, coluna].ToString().PadLeft(3));
                }
                Console.WriteLine();
            }
        }
    }
}
