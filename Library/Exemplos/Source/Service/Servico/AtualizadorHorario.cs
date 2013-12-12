namespace MP.LBJC.Util
{
	using System;
	using System.ServiceProcess;
	using System.Timers;

	public partial class AtualizadorHorario : ServiceBase
	{
		private const int c1Segundo = 1000;
		private const int cIntervaloEmMiliSegundos = c1Segundo * 40;

		private Timer _timer;

		private Boolean _enabled = false;
		private Boolean Enabled
		{
			get { return _enabled; }
			set
			{
				_enabled = value;
				try { _timer.Enabled = value; }
				catch (Exception) { }
			}
		}

		public AtualizadorHorario()
		{
			InitializeComponent();
			ServiceInstallerUtil.Log(ServiceName, "I", "Serviço Instanciado");

			_timer = new Timer(cIntervaloEmMiliSegundos);
			_timer.Enabled = false;
			_timer.Elapsed += new ElapsedEventHandler(TenteProcessar);
		}

		~AtualizadorHorario()
		{
			ServiceInstallerUtil.Log(ServiceName, "I", "Serviço Destruido");
			_timer.Stop();
			_timer.Close();
			_timer.Dispose();
		}

		protected override void OnStart(string[] args)
		{
			ServiceInstallerUtil.Log(ServiceName, "A", "Serviço Iniciado");
			Enabled = true;
			base.OnStart(args);
		}

		protected override void OnStop()
		{
			ServiceInstallerUtil.Log(ServiceName, "A", "Serviço Parado");
			Enabled = false;
			base.OnStop();
		}

		protected override void OnPause()
		{
			ServiceInstallerUtil.Log(ServiceName, "A", "Serviço Pausado");
			Enabled = false;
			base.OnPause();
		}

		protected override void OnContinue()
		{
			ServiceInstallerUtil.Log(ServiceName, "A", "Serviço Continuado");
			Enabled = true;
			base.OnContinue();
			NTPClient.SetSystemTime();
		}

		protected override void OnShutdown()
		{
			ServiceInstallerUtil.Log(ServiceName, "A", "Serviço Quebrado");
			Enabled = false;
			base.OnShutdown();
		}

		private void TenteProcessar(Object sender, ElapsedEventArgs e)
		{
			Enabled = false;
			VerifiqueSeAtendeOsRequisitosParaProcessar();
			Enabled = true;
		}

		private void VerifiqueSeAtendeOsRequisitosParaProcessar()
		{
			try
			{
				if (Datas.HoraDoBrasil().Minute == 50)
					Processar();
			}
			catch (Exception vException)
			{
				ServiceInstallerUtil.Log(ServiceName, "\r\nE", "Erro: " + vException.Message);
			}
			finally
			{
			}
		}

		private void Processar()
		{
			ServiceInstallerUtil.Log(ServiceName, "\r\nI", "Ini: Processamento");
			NTPClient.SetSystemTime();
			ServiceInstallerUtil.Log(ServiceName, "I", "Fim: Processamento");
		}
	}

	public class Datas
	{
		public static DateTime HoraDoBrasil()
		{
			return DateTime.Now;
		}
	}
}