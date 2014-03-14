using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MPSC.Library.Exemplos.Service
{
    [Table]
    public class Cliente
    {
        [Property]
        public String Nome { get; set; }

        [Property(DataType = "DateTime")]
        public DateTime Nascimento { get; set; }
    }


    class CleinteDTO
    {
        public String Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public Decimal ValorDeComprasAVencer { get; set; }
    }






    class EmpresaDTO
    {
        public EnderecoVO Endereco { get; set; }
        public String RazaoSocial { get; set; }

        public void AlterarLogradouro(string logradouro)
        {
            Endereco = Endereco.AlterarLogradouro(logradouro);
        }

        public void AlterarBairro(string bairro)
        {
            Endereco = Endereco.AlterarBairro(bairro);
        }
    }

    class EnderecoVO
    {
        private String cidade;

        public String Logradouro { get; set; }
        public String Bairro { get; set; }

        public EnderecoVO(String rua, String bairro)
        {
            Logradouro = rua;
            Bairro = bairro;
        }

        public String Cidade
        {
            get { return cidade; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    cidade = value;
                else
                    throw new ArgumentException("A cidade não pode ser nula");
            }
        }

        public int UltimoMes { get { return 12; } }

        public EnderecoVO AlterarLogradouro(String rua)
        {
            return new EnderecoVO(rua, Bairro);
        }

        public EnderecoVO AlterarBairro(String bairro)
        {
            return new EnderecoVO(Logradouro, bairro);
        }
    }

    class DateTimeEstudo
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Day { get; set; }

        public DateTimeEstudo(int ano, int mes, int dia)
        {
            Ano = ano;
            Mes = mes;
            Day = dia;
        }

        public void AddDays(int diasParaAdicionar)
        {
            Day = Day + diasParaAdicionar;
        }
    }

    public class ExtensionMethods : IExecutavel
    {
        public void Executar()
        {

            var endereco = new EnderecoVO("Estrada 1", "Ilha do Governador");
            endereco.Cidade = "Rio de Janeiro";

            int ultimoMes = endereco.UltimoMes;

            
            var empresa1 = new EmpresaDTO()
            {
                RazaoSocial = "Mercado Pleno",
                Endereco = endereco
            };

            var empresa2 = new EmpresaDTO()
            {
                RazaoSocial = "Microsoft",
                Endereco = endereco

            };

            Console.WriteLine("Antes");
            Console.WriteLine("{0} -> {1}, {2}", empresa1.RazaoSocial, empresa1.Endereco.Logradouro, empresa1.Endereco.Bairro);
            Console.WriteLine("{0} -> {1}, {2}", empresa2.RazaoSocial, empresa2.Endereco.Logradouro, empresa2.Endereco.Bairro);

            empresa1.AlterarLogradouro("Avenida Principal");
            empresa1.AlterarBairro("Avenida Principal");

            //.Endereco.AlterarRua("Avenida Principal");

            Console.WriteLine("Depois");
            Console.WriteLine("{0} -> {1}, {2}", empresa1.RazaoSocial, empresa1.Endereco.Logradouro, empresa1.Endereco.Bairro);
            Console.WriteLine("{0} -> {1}, {2}", empresa2.RazaoSocial, empresa2.Endereco.Logradouro, empresa2.Endereco.Bairro);


            String xml = "<xml><Pessoa><Nome>Glautter</Nome></Pessoa><Pessoa><Nome>Fernandes</Nome></Pessoa></xml>";

            String valor = "50";

            int numero1 = valor.Valor();
            int numero2 = "50".Valor();

            var igual1 = "50".Equals(valor);
            var igual2 = valor.Equals("50");

            var num = 50.5f.ToString();


            Console.WriteLine(num);
            // Sem extension Methods
            Console.WriteLine(StringExt.ExtrairXML(xml, "xml"));
            Console.WriteLine(StringExt.ExtrairXML(xml, "Pessoa"));
            Console.WriteLine(StringExt.ExtrairXML(xml, "Nome"));

            // Com extension Methods
            Console.WriteLine(xml.ExtrairXML("xml"));
            Console.WriteLine(xml.ExtrairXML("Pessoa"));
            Console.WriteLine(xml.ExtrairXML("Nome"));

            String xml2 = null;
            xml2.ExtrairXML(".");
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
            if (source != null)
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
            }
            return source;
        }
    }
}
/*
             int a = 5;
            int b = 6;
            a = b + 1;
            
            DateTime hoje = new DateTime(2014, 03, 13);
            hoje.AddDays(1);
            Console.WriteLine(hoje.Day);

            Console.WriteLine();

 */