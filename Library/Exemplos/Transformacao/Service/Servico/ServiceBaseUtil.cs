namespace MP.LBJC.Util.Servico
{
	using System;
	using System.ServiceProcess;
	using System.Timers;

	public partial class ServiceBaseUtil : ServiceBase, IProcessoService
	{
		private ServiceInstallerUtil _serviceInstallerUtil;
		private IProcessoService _processoService;
		private Timer _timer;
		private Boolean _iniciaProcessando;
		private Boolean Enabled
		{
			get { return _timer.Enabled; }
			set { try { _timer.Enabled = value; } catch (Exception) { } }
		}

		public ServiceBaseUtil(ServiceInstallerUtil serviceInstallerUtil, IProcessoService processoService, Decimal intervaloEmSegundos, Boolean iniciaProcessando, Boolean podePausarContinuar, Boolean podeSerDerrubado)
		{
			InitializeComponent(serviceInstallerUtil, processoService, Convert.ToInt32(intervaloEmSegundos * 1000), iniciaProcessando, podePausarContinuar, podeSerDerrubado);
			serviceInstallerUtil.Log(LogEnum.Informativo, "Serviço Instanciado");
		}

		private void InitializeComponent(ServiceInstallerUtil serviceInstallerUtil, IProcessoService processoService, Int32 intervaloEmMiliSegundos, Boolean iniciaProcessando, Boolean podePausarContinuar, Boolean podeSerDerrubado)
		{
			ServiceName = serviceInstallerUtil.ServiceName;
			CanShutdown = podeSerDerrubado;
			CanPauseAndContinue = podePausarContinuar;

			_iniciaProcessando = iniciaProcessando;
			_processoService = processoService;
			_serviceInstallerUtil = serviceInstallerUtil;
			_timer = new Timer(intervaloEmMiliSegundos);
			_timer.Enabled = false;
			_timer.Elapsed += new ElapsedEventHandler(TenteProcessar);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			GC.Collect();
		}


		~ServiceBaseUtil()
		{
			_serviceInstallerUtil.Log(LogEnum.Informativo, "Serviço Destruido");
			_timer.Stop();
			_timer.Close();
			_timer.Dispose();
		}

		protected override void OnStart(string[] args)
		{
			_serviceInstallerUtil.Log(LogEnum.Administrativo, "Serviço Iniciado");
			Enabled = true;
			base.OnStart(args);
			if (_iniciaProcessando) Processar();
		}

		protected override void OnStop()
		{
			_serviceInstallerUtil.Log(LogEnum.Administrativo, "Serviço Parado");
			Enabled = false;
			base.OnStop();
		}

		protected override void OnPause()
		{
			_serviceInstallerUtil.Log(LogEnum.Administrativo, "Serviço Pausado");
			Enabled = false;
			base.OnPause();
		}

		protected override void OnContinue()
		{
			_serviceInstallerUtil.Log(LogEnum.Administrativo, "Serviço Continuado");
			Enabled = true;
			base.OnContinue();
			Processar();
		}

		protected override void OnShutdown()
		{
			_serviceInstallerUtil.Log(LogEnum.Administrativo, "Serviço Quebrado");
			Enabled = false;
			base.OnShutdown();
		}

		private void TenteProcessar(Object sender, ElapsedEventArgs e)
		{
			Enabled = false;
			VerifiqueSeAtendeOsRequisitosParaProcessar();
			Enabled = true;
		}

		protected void VerifiqueSeAtendeOsRequisitosParaProcessar()
		{
			try
			{
				Processar();
			}
			catch (Exception vException)
			{
				_serviceInstallerUtil.Log(LogEnum.Exception, "Erro: " + vException.Message + "\r\n\r\n");
			}
			finally
			{
			}
		}

		public Boolean Processar()
		{
			_serviceInstallerUtil.Log(LogEnum.Informativo, "Ini: Processamento");
			var vRetorno = _processoService.Processar();
			_serviceInstallerUtil.Log(LogEnum.Informativo, "Fim: Processamento" + "\r\n");
			return vRetorno;
		}

		public int ProcessarParametro(String[] parametros)
		{
			return _serviceInstallerUtil.ProcessarParametro(parametros, this);
		}
	}
}