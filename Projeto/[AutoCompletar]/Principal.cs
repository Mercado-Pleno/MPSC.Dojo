namespace AutoCompletar
{
    using System;
    using System.Windows.Forms;

    public static class Principal
    {
        [STAThread]
        public static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Exemplo());
        }
    }
}