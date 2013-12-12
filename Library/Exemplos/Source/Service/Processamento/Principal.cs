namespace MP.LBJC.Util
{
	using System;
	using System.Reflection;
	using System.ServiceProcess;

	public static class Principal
	{
		public static int Main(String[] args)
		{
			if (Environment.UserInteractive)
			{
				String vParam = ((args != null) && (args.Length > 0)) ? args[0].ToUpper() : "{Null}";

				if (vParam.Equals("/R"))
					NTPClient.SetSystemTime();
				else
					ServiceInstallerUtil.ProcessarParametro("MP PlenoSMS", vParam);
			}
			else
				ServiceBase.Run(new AtualizadorHorario());

			return 0;
		}
	}
}