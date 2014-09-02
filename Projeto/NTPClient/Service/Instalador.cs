namespace MP.LBJC.ServicoWindows.Util
{
	using System;
	using System.ComponentModel;
	using System.ServiceProcess;
	using MPSC.Library.Exemplos.Service;

	[RunInstaller(true)]
	public class Instalador : InstaladorDeServico
	{
		private static DadosDoServico servico = ConfigurarServico();

		public Instalador() : base(servico) { }

		public static int Main(String[] args)
		{
			return servico.IniciarExecucao(new NTPClient(), args);
		}

		private static DadosDoServico ConfigurarServico()
		{
			return new DadosDoServico()
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