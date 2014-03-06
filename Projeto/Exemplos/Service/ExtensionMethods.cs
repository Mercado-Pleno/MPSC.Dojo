using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MPSC.Library.Exemplos.Service
{
    public class ExtensionMethods : IExecutavel
    {



        public void Executar()
        {
            String xml = "<xml><Pessoa><Nome>Glautter</Nome></Pessoa><Pessoa><Nome>Fernandes</Nome></Pessoa></xml>";

            String valor = "50";

            int numero1 = valor.Valor();
            int numero2 = "50".Valor();

            var igual1 = "50".Equals(valor);
            var igual2 = valor.Equals("50");

            var num = 50.5f.ToString();


            Console.WriteLine(num);
            // Sem extension Methods
            Console.WriteLine(StringExt.ExtrairXML(xml, "xml" ));
            Console.WriteLine(StringExt.ExtrairXML(xml, "Pessoa"));
            Console.WriteLine(StringExt.ExtrairXML(xml, "Nome"));

            // Com extension Methods
            Console.WriteLine(xml.ExtrairXML("xml"));
            Console.WriteLine(xml.ExtrairXML("Pessoa"));
            Console.WriteLine(xml.ExtrairXML("Nome"));
        }
    }

    public static class StringExt
    {

        public static int Valor(this String str)
        {
            return Convert.ToInt32(str);
        }

        public static String ExtrairXML(this String source, String tag)
        {
            return source.Extrair("<" + tag + ">", "</" + tag + ">");
        }

        public static String Extrair(this String source, String tagInicial, String tagFinal)
        {
            var posicao = source.IndexOf(tagInicial);
            if (posicao >= 0)
            {
                source = source.Substring(posicao + tagInicial.Length);

                posicao = source.IndexOf(tagFinal);
                if (posicao >= 0)
                {
                    source = source.Substring(0, posicao);
                }
                else
                    source = String.Empty;
            }
            else
                source = String.Empty;
            return source;
        }
    }
}
