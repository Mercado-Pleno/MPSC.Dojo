namespace MP.SVNControl
{
	using System;
	using MP.SVNControl;
	using System.Threading;
	using System.Diagnostics;
	using System.Runtime.InteropServices;

	public static class Program
	{
		public static int Main(String[] args)
		{
			SVNParam vSVNParam = new SVNParam(args);
			String vParametros = vSVNParam.ToString();

			var vErr = Console.OpenStandardError();
			foreach (var c in vParametros)
			{
				vErr.WriteByte((byte)c);
				vErr.Flush();
			}
			vErr.Close();
			vErr.Dispose();

			var vOut = Console.OpenStandardOutput();
			foreach (var c in vParametros)
			{
				vOut.WriteByte((byte)c);
				vOut.Flush();
			}
			vOut.Close();
			vOut.Dispose();


			return 1;
		}
	}
}