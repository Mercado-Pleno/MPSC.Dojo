namespace MP.LBJC.ServicoWindows.Util
{
	using System;
	using MP.LBJC.Utils;
	using System.Reflection;

	public static class Principal
	{
		public static int Main(String[] args)
		{
			var serviceInstallerUtil = new ServiceInstallerUtil(Assembly.GetExecutingAssembly(),"MP.Teste.Name", "MP.Teste.Display", "MP.Teste.Description");
			var serviceBase = new ServiceBaseUtil(serviceInstallerUtil, new NTPClient(), 40, true);
			return serviceBase.ProcessarParametro(args);
		}
	}
}