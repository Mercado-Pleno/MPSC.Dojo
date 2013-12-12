using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public static class Inicio
    {
        [STAThread]
        public static void Mains()
        {
            StreamReader lStreamReader = new StreamReader(@"C:\Users\brunonogueira\Desktop\lista.txt");
            String texto = lStreamReader.ReadToEnd();
            lStreamReader.Close();
            lStreamReader.Dispose();

            IList<String> listaEmails = texto.Replace("\r", "").Replace(" ", "").Split("\n".ToCharArray());
            IList<String> listaEmailsGrava = new List<String>();
            foreach (String eMail in listaEmails)
            {
                String mail = eMail;
                if (eMail.Contains("<") && eMail.Contains(">"))
                {
                    mail = eMail.Substring(eMail.IndexOf("<") + 1);
                    mail = mail.Substring(0, mail.IndexOf(">"));
                }
                else if (eMail.Contains("@"))
                    mail = eMail.Replace(",", "");

                listaEmailsGrava.Add(mail);
            }

            listaEmails = listaEmailsGrava.OrderBy(e => e).Distinct().ToList();

            StreamWriter lStreamWriter = new StreamWriter(@"C:\Users\brunonogueira\Desktop\lista2.txt", false);
            foreach (String eMail in listaEmails)
            {
                lStreamWriter.WriteLine(eMail + ",");
            }

            lStreamWriter.Flush();
            lStreamWriter.Close();
            lStreamWriter.Dispose();


            String chave = "ChaveUltraSecretaQueNãoPodeSerReveladaàNinguém";
            String original = "Abóbora, Melão e Melancia";
            String criptografado = Cripto(original, chave);
            String decriptografado = Cripto(criptografado, chave);
            MessageBox.Show(
                String.Format(
                    "Original: ({0}) {1}\n" +
                    "Criptografado: ({2}) {3}\n" +
                    "Original = Cripto(Criptografado): {4}",
                    original.Length, original,
                    criptografado.Length, criptografado,
                    original == decriptografado
                )
            );
        }

        public static String Cripto(String valor, String chave)
        {
            String retorno = String.Empty;
            chave += " "; //para ter pelo menos um caracter na chave e não gerar erro...
            int contador = 0;

            foreach (Char v in valor)
                retorno += XOR(v, chave[contador++ % chave.Length]);

            return retorno;
        }

        private static string XOR(char valor, char chave)
        {
            int criptografiaXOR = ((int)valor) ^ ((int)chave);
            return ((char)criptografiaXOR).ToString();
        }
    }

    public static class Program
    {
       public static void Executar()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1(new Relatorio()));
        }
        public static ListaDados PreencherDataSource()
        {
            ListaDados retorno = new ListaDados();
            for (int linha = 1; linha <= 10; linha++)
            {
                for (int coluna = 1; coluna <= 10; coluna++)
                {
                    retorno.Add(new Dados() { Data = DateTime.Now.AddDays(linha * coluna), Produto = "A" + (linha * coluna).ToString(), Valor = (linha + 2) * (coluna + linha) });
                }
            }
            return retorno;
        }
    }

    public class Relatorio
    {
        public IList<String> Produtos { get { return Program.PreencherDataSource().Select(dados => dados.Produto).Distinct().ToList(); } }
        public IList<String> Meses { get { return Program.PreencherDataSource().Select(dados => dados.Mes).Distinct().ToList(); } }
        public ListaDados DataSource { get { return Program.PreencherDataSource(); } }
    }

    public class ListaDados : List<Dados>, IList<Dados>, IEnumerable<Dados> { }

    public class Dados
    {
        public string Mes { get { return Data.ToString("MMMM", new CultureInfo("pt-BR")); } }
        public DateTime Data { get; set; }
        public String Produto { get; set; }
        public Double Valor { get; set; }
    }
}