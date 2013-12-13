namespace MP.LBJC.Util.ServicoWindows
{
	using System;
	using System.ServiceProcess;
	using System.Timers;

	public partial class ServiceBaseUtil : ServiceBase, IProcessoService
	{
		protected IServiceInstallerUtil serviceInstallerUtil;
		protected IProcessoService processoService;
		protected Timer timer;
		protected Boolean iniciaProcessando;
		protected virtual Boolean PodeSerProcessado { get { return true; } }
		protected virtual Boolean Enabled
		{
			get { return timer.Enabled; }
			set { try { timer.Enabled = value; } catch (Exception) { } }
		}

		public ServiceBaseUtil(IServiceInstallerUtil serviceInstallerUtil, IProcessoService processoService, Decimal intervaloEmSegundos)
		{
			InitializeComponent(serviceInstallerUtil, processoService, Convert.ToInt32(intervaloEmSegundos * 1000), false, true, true);
		}

		public ServiceBaseUtil(IServiceInstallerUtil serviceInstallerUtil, IProcessoService processoService, Decimal intervaloEmSegundos, Boolean iniciaProcessando)
		{
			InitializeComponent(serviceInstallerUtil, processoService, Convert.ToInt32(intervaloEmSegundos * 1000), iniciaProcessando, true, true);
		}

		public ServiceBaseUtil(IServiceInstallerUtil serviceInstallerUtil, IProcessoService processoService, Decimal intervaloEmSegundos, Boolean iniciaProcessando, Boolean podePausarContinuar, Boolean podeFazerShutDown)
		{
			InitializeComponent(serviceInstallerUtil, processoService, Convert.ToInt32(intervaloEmSegundos * 1000), iniciaProcessando, podePausarContinuar, podeFazerShutDown);
		}

		private void InitializeComponent(IServiceInstallerUtil serviceInstallerUtil, IProcessoService processoService, Int32 intervaloEmMiliSegundos, Boolean iniciaProcessando, Boolean podePausarContinuar, Boolean podeFazerShutDown)
		{
			base.ServiceName = serviceInstallerUtil.ServiceName;
			base.CanShutdown = podeFazerShutDown;
			base.CanPauseAndContinue = podePausarContinuar;

			this.iniciaProcessando = iniciaProcessando;
			this.processoService = processoService;
			this.serviceInstallerUtil = serviceInstallerUtil;
			this.timer = new Timer(intervaloEmMiliSegundos);
			this.timer.Enabled = false;
			this.timer.Elapsed += new ElapsedEventHandler(TenteProcessar);

			Log(LogEnum.Informativo, String.Format(@"Serviço Instanciado. Parametros: serviceInstallerUtil={0}; processoService={1}; intervaloEmMiliSegundos={2}; iniciaProcessando={3}; podePausarContinuar={4}; podeFazerShutDown={5};", serviceInstallerUtil.GetType().Name, processoService.GetType().Name, intervaloEmMiliSegundos, iniciaProcessando, podePausarContinuar, podeFazerShutDown));
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			GC.Collect();
		}

		~ServiceBaseUtil()
		{
			Log(LogEnum.Informativo, "Serviço Destruido");
			internalDispose();
		}

		private void internalDispose()
		{
			if (timer != null)
			{
				try
				{
					timer.Stop();
					timer.Close();
					timer.Dispose();
				}
				catch (Exception)
				{ }
				finally
				{
					timer = null;
				}
			}

			if (processoService != null)
			{
				try
				{
					processoService.Dispose();
				}
				catch (Exception)
				{ }
				finally
				{
					processoService = null;
				}
			}

			if (serviceInstallerUtil != null)
			{
				try
				{
					serviceInstallerUtil.Dispose();
				}
				catch (Exception)
				{ }
				finally
				{
					serviceInstallerUtil = null;
				}
			}
		}

		protected override void OnStart(string[] args)
		{
			Log(LogEnum.Administrativo, "Serviço Iniciado");
			Enabled = true;
			base.OnStart(args);
			if (iniciaProcessando) Processar();
		}

		protected override void OnStop()
		{
			Log(LogEnum.Administrativo, "Serviço Parado");
			Enabled = false;
			base.OnStop();
		}

		protected override void OnPause()
		{
			Log(LogEnum.Administrativo, "Serviço Pausado");
			Enabled = false;
			base.OnPause();
		}

		protected override void OnContinue()
		{
			Log(LogEnum.Administrativo, "Serviço Continuado");
			Enabled = true;
			base.OnContinue();
			if (iniciaProcessando) Processar();
		}

		protected override void OnShutdown()
		{
			Log(LogEnum.Administrativo, "Serviço Quebrado");
			Enabled = false;
			base.OnShutdown();
		}

		protected virtual void TenteProcessar(Object sender, ElapsedEventArgs e)
		{
			Enabled = false;
			try
			{
				Processar();
			}
			catch (Exception vException)
			{
				Log(LogEnum.Exception, "Erro: " + vException.Message + "\r\n\r\n");
			}
			Enabled = true;
		}

		public virtual Boolean Processar()
		{
			Log(LogEnum.Informativo, "Ini: Processamento");
			var vRetorno = PodeSerProcessado && processoService.Processar();
			Log(LogEnum.Informativo, "Fim: Processamento" + "\r\n");
			return vRetorno;
		}

		public virtual int ProcessarParametro(String[] parametros)
		{
			return (serviceInstallerUtil != null) ? serviceInstallerUtil.ProcessarParametro(parametros, this) : -99;
		}

		private void Log(LogEnum logEnum, string mensagem)
		{
			if (serviceInstallerUtil != null) serviceInstallerUtil.Log(logEnum, mensagem);
		}
	}
}