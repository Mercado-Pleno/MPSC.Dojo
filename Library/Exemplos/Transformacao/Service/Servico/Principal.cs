﻿namespace MP.LBJC.Util.ServicoWindows
{
	using System;
	using MP.LBJC.Utils;

	public static class Principal
	{
		public static int Mains(String[] args)
		{
			var serviceInstallerUtil = new ServiceInstallerUtil("MP.Teste.Name", "MP.Teste.Display", "MP.Teste.Description");
			var serviceBase = new ServiceBaseUtil(serviceInstallerUtil, new NTPClient(), 40, true);
			return serviceBase.ProcessarParametro(args);
		}
	}
}