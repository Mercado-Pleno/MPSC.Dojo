namespace MP.LBJC.ServicoWindows.Util
{
	using System;
	using System.ComponentModel;
	using System.Reflection;
	using System.ServiceProcess;
    using MPSC.Library.Exemplos.Service;

	[RunInstaller(true)]
	public class Instalador : InstaladorDeServico
	{
		private static DadosDoServico servico = ConfigurarServico();

		public Instalador() : base(servico) { }

		public static int Main(String[] args)
		{
			var servicoDoWindows = new ServicoDoWindows(servico, new NTPClient());
			var controladorDeServico = new ControladorDeServico(servicoDoWindows);
			return controladorDeServico.ProcessarParametro(args);
		}

		private static DadosDoServico ConfigurarServico()
		{
			return new DadosDoServico(Assembly.GetExecutingAssembly())
			.ComServiceName("MP.Teste.Name")
			.ComDisplayName("MP.Teste.Display")
			.ComDescricao("MP.Teste.Description")

			.ComServiceStartMode(ServiceStartMode.Automatic)
			.ComServiceAccount(ServiceAccount.LocalSystem)
			.ComUsuarioESenha(null, null)

			.ComIntervaloEmSegundos(40)
			.ComPodePausarContinuar(true)
			.ComPodeFazerShutDown(true);
		}
	}
}