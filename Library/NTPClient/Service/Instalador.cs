namespace MP.LBJC.ServicoWindows.Util
{
	using System;
	using System.ComponentModel;
	using System.Reflection;
	using MP.LBJC.Utils;

	[RunInstaller(true)]
	public class Instalador : ServiceInstallerUtil
	{
		public Instalador()
			: base(Assembly.GetExecutingAssembly(), "MP.Teste.Name", "MP.Teste.Display", "MP.Teste.Description")
		{

		}
		public static int Main(String[] args)
		{
			var serviceBase = new ServiceBaseUtil(new Instalador(), new NTPClient(), 40, true);
			return serviceBase.ProcessarParametro(args);
		}
	}
}