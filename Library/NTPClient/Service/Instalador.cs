namespace MP.LBJC.ServicoWindows.Util
{
	using System;
	using System.ComponentModel;
	using System.Reflection;
	using System.ServiceProcess;
	using MP.LBJC.Utils;

	[RunInstaller(true)]
	public class Instalador : InstaladorDeServico
	{
		private static Servico servico = new Servico(Assembly.GetExecutingAssembly(), new NTPClient())
			.ComServiceName("MP.Teste.Name").ComDisplayName("MP.Teste.Display").ComDescricao("MP.Teste.Description")
			.ComServiceStartMode(ServiceStartMode.Automatic).ComServiceAccount(ServiceAccount.LocalSystem).ComUsuarioESenha(null, null)
			.ComIntervaloEmSegundos(40).ComPodePausarContinuar(true).ComPodeFazerShutDown(true);

		public Instalador() : base(servico) { }

		public static int Main(String[] args)
		{
			var serviceControllerUtil = new ControladorDeServico(servico);
			var serviceBaseUtil = new BaseDeServico(servico);
			return serviceControllerUtil.ProcessarParametro(args, serviceBaseUtil);
		}
	}
}