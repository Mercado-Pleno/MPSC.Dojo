namespace MP.LBJC.ServicoWindows.Util
{
	using System;
	using System.ComponentModel;
	using System.Reflection;
	using MP.LBJC.Utils;

	[RunInstaller(true)]
	public class Principal : ServiceInstallerUtil
	{
		public Principal()
			: base(Assembly.GetExecutingAssembly(), "MP.Teste.Name", "MP.Teste.Display", "MP.Teste.Description")
		{

		}
		public static int Main(String[] args)
		{
			var serviceInstallerUtil = new Principal();
			var serviceBase = new ServiceBaseUtil(serviceInstallerUtil, new NTPClient(), 40, true);
			return serviceBase.ProcessarParametro(args);
		}
	}
}