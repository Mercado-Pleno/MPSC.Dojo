namespace MP.LBJC.Util.Servico
{
	using System;
	using System.ServiceProcess;
	using MP.LBJC.Utils;

	public static class Principal
	{
		public static int Mains(String[] args)
		{
			var serviceInstallerUtil = new ServiceInstallerUtil("MP PlenoSMS", "MP PlenoSMS", "MP PlenoSMS", ServiceStartMode.Automatic, ServiceAccount.LocalSystem, null, null);
			var serviceBase = new ServiceBaseUtil(serviceInstallerUtil, new NTPClient(), 40, true, true);
			return serviceBase.ProcessarParametro(args);
		}
	}
}