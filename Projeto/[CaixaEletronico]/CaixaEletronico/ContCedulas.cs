using System;
using System.Collections.Generic;

namespace PosSetup.bat
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string [] prParam)
        {
            Teste();
        }

        public static void Teste()
        {
            List<double> lListaCedulas = new List<double>();
            lListaCedulas.Add(20);
            lListaCedulas.Add(50);

            Dictionary<double, int> lRetorno = MSV_Calcular(220, lListaCedulas);
            string lstRetorno = "";
            foreach (KeyValuePair<double, int> item in lRetorno)
                lstRetorno += item.Value.ToString() + " Notas de " + item.Key.ToString() + "!\n";

            Console.WriteLine(lstRetorno);
            Console.ReadLine();
        }

        public static Dictionary<double, int> MSV_Calcular(double prreValorTotal, List<double> prListaCedulas)
        {
            Dictionary<double, int> ldcRetorno = new Dictionary<double, int>();
            prListaCedulas.Sort();
            foreach (double lreCedula in prListaCedulas)
                ldcRetorno.Add(lreCedula, 0);

            double lreSoma = 0;
            int linQtdCedulas = prListaCedulas.Count;
            double lreValorCedula = 0;
            while ((lreSoma < prreValorTotal) && (lreSoma>=0))
            {
                if (linQtdCedulas > 0)
                    lreValorCedula = prListaCedulas[linQtdCedulas-1];

                if (lreValorCedula <= (prreValorTotal - lreSoma))
                {
                    int linQtd = Convert.ToInt32(Convert.ToInt32(prreValorTotal - lreSoma) / Convert.ToInt32(lreValorCedula));
                    ldcRetorno[lreValorCedula] = linQtd;
                    lreSoma += lreValorCedula * linQtd;
                    linQtdCedulas--;
                }
                if (linQtdCedulas > 0)
                    lreValorCedula = prListaCedulas[linQtdCedulas - 1];
                
                if (((prreValorTotal - lreSoma)>0) && (lreValorCedula > (prreValorTotal - lreSoma)))
                {
                    linQtdCedulas++;
                    lreValorCedula = prListaCedulas[linQtdCedulas];
                    ldcRetorno[lreValorCedula] = ldcRetorno[lreValorCedula] - 1;
                    lreSoma -= lreValorCedula;
                    linQtdCedulas--;
                }
            }
            return ldcRetorno;
        }
    }
}