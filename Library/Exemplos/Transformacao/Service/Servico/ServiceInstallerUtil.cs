namespace MP.LBJC.Util.Servico
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Configuration.Install;
	using System.IO;
	using System.Reflection;
	using System.ServiceProcess;
	using System.Collections.Generic;

	[RunInstaller(true)]
	public class ServiceInstallerUtil : Installer
	{
		private ServiceInstaller _serviceInstaller;
		private ServiceProcessInstaller _serviceProcessInstaller;
		public string ServiceName { get; private set; }

		public ServiceInstallerUtil(String serviceName, String displayName, String description, ServiceStartMode serviceStartMode, ServiceAccount serviceAccount, String username, String password)
		{
			InitializeComponent(serviceName, displayName, description, serviceStartMode, serviceAccount, username, password);
		}

		private void InitializeComponent(String serviceName, String displayName, String description, ServiceStartMode serviceStartMode, ServiceAccount serviceAccount, String username, String password)
		{
			ServiceName = serviceName;
			_serviceProcessInstaller = new ServiceProcessInstaller();
			_serviceInstaller = new ServiceInstaller();

			_serviceProcessInstaller.Account = serviceAccount;
			_serviceProcessInstaller.Username = username;
			_serviceProcessInstaller.Password = password;

			_serviceInstaller.ServiceName = serviceName;
			_serviceInstaller.DisplayName = displayName;
			_serviceInstaller.Description = description;
			_serviceInstaller.StartType = serviceStartMode;

			Installers.AddRange(new Installer[] { this._serviceProcessInstaller, this._serviceInstaller });
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			while (Installers.Count > 0)
				Installers.RemoveAt(0);
		}

		public Boolean Instalar()
		{
			return InstalarServico(ServiceName);
		}

		public Boolean Desinstalar()
		{
			return DesinstalarServico(ServiceName);
		}

		public Boolean EstaInstalado { get { return ServicoEstaInstalado(ServiceName); } }

		public Boolean Iniciar()
		{
			return IniciarServico(ServiceName);
		}

		public Boolean Parar()
		{
			return PararServico(ServiceName);
		}

		public Boolean EstaRodando { get { return ServicoEstaRodando(ServiceName); } }

		private Boolean InstalarServico(String nomeDoServico)
		{
			Log(LogEnum.Administrativo, "Instalando o Serviço");
			Boolean vRetorno = ServicoEstaInstalado(nomeDoServico);
			if (!vRetorno)
			{
				try
				{
					using (AssemblyInstaller installer = GetInstaller())
					{
						IDictionary state = new Hashtable();
						try
						{
							installer.Install(state);
							installer.Commit(state);
						}
						catch (Exception exception)
						{
							vRetorno = false;
							Log(LogEnum.FalhaInterna, exception.Message);
							installer.Rollback(state);
						}
					}
					vRetorno = ServicoEstaInstalado(nomeDoServico);
				}
				catch (Exception exception)
				{
					vRetorno = false;
					Log(LogEnum.FalhaInterna, exception.Message);
				}
			}
			return vRetorno;
		}

		private Boolean DesinstalarServico(String nomeDoServico)
		{
			Log(LogEnum.Administrativo, "Desinstalando o Serviço");
			Boolean vRetorno = !ServicoEstaInstalado(nomeDoServico);
			if (!vRetorno)
			{
				try
				{
					using (AssemblyInstaller installer = GetInstaller())
					{
						IDictionary state = new Hashtable();
						try
						{
							installer.Uninstall(state);
							installer.Commit(state);
						}
						catch (Exception exception)
						{
							vRetorno = false;
							Log(LogEnum.FalhaInterna, exception.Message);
							installer.Rollback(state);
						}
					}
				}
				catch (Exception exception)
				{
					vRetorno = false;
					Log(LogEnum.FalhaInterna, exception.Message);
				}
			}
			return vRetorno;
		}

		private Boolean ServicoEstaInstalado(String nomeDoServico)
		{
			Boolean vRetorno = false;
			try
			{
				using (ServiceController controller = new ServiceController(nomeDoServico))
				{
					ServiceControllerStatus status = controller.Status;
					controller.Close();
					vRetorno = true;
				}
			}
			catch (Exception exception)
			{
				vRetorno = false;
				Log(LogEnum.FalhaInterna, exception.Message);
			}
			return vRetorno;
		}

		private Boolean IniciarServico(String nomeDoServico)
		{
			Boolean vRetorno = ServicoEstaInstalado(nomeDoServico);
			if (vRetorno)
			{
				try
				{
					using (ServiceController controller = new ServiceController(nomeDoServico))
					{
						if (controller.Status != ServiceControllerStatus.Running)
						{
							controller.Start();
							controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
						}
						vRetorno = (controller.Status == ServiceControllerStatus.Running);
						controller.Close();
					}
				}
				catch (Exception exception)
				{
					vRetorno = false;
					Log(LogEnum.FalhaInterna, exception.Message);
				}
			}
			return vRetorno;
		}

		private Boolean PararServico(String nomeDoServico)
		{
			Boolean vRetorno = ServicoEstaInstalado(nomeDoServico);
			if (vRetorno)
			{
				try
				{
					using (ServiceController controller = new ServiceController(nomeDoServico))
					{
						if (controller.Status != ServiceControllerStatus.Stopped)
						{
							controller.Stop();
							controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
						}
						vRetorno = (controller.Status == ServiceControllerStatus.Stopped);
						controller.Close();
					}
				}
				catch (Exception exception)
				{
					vRetorno = false;
					Log(LogEnum.FalhaInterna, exception.Message);
				}
			}
			return vRetorno;
		}

		private Boolean ServicoEstaRodando(String nomeDoServico)
		{
			Boolean vRetorno = ServicoEstaInstalado(nomeDoServico);
			if (vRetorno)
			{
				try
				{
					using (ServiceController controller = new ServiceController(nomeDoServico))
					{
						vRetorno = (controller.Status == ServiceControllerStatus.Running);
						controller.Close();
					}
				}
				catch (Exception exception)
				{
					vRetorno = false;
					Log(LogEnum.FalhaInterna, exception.Message);
				}
			}
			return vRetorno;
		}

		private AssemblyInstaller GetInstaller()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			return new AssemblyInstaller(assembly, null) { UseNewContext = true };
		}

		public Boolean Log(LogEnum tipo, String mensagem)
		{
			Boolean vRetorno = false;
			String vArquivo = @"C:\MPSC\" + ServiceName.Replace(" ", "_").Replace("/", ".") + ".log";
			String vMensagem = String.Format("{0} {1} -> {2}", tipo.ToString().Substring(0, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"), mensagem);
			try
			{
				StreamWriter vStreamWriter = new StreamWriter(vArquivo, true);
				vStreamWriter.WriteLine(vMensagem);
				vStreamWriter.Flush();
				vStreamWriter.Close();
				vStreamWriter.Dispose();
				vRetorno = true;
			}
			catch (Exception exception)
			{
				vRetorno = false;
				throw new Exception(mensagem, exception);
			}

			return vRetorno;
		}

		public int ProcessarParametro(String[] parametros, IProcessoService processoService)
		{
			var vRetorno = 0;
			try
			{
				var vParam = new List<String>((parametros == null) ? new string[] { "{Null}" } : parametros);

				if ((processoService is ServiceBase) && !Environment.UserInteractive)
					ServiceBase.Run(processoService as ServiceBase);
				else
				{
					if (vParam.Contains("/R") || vParam.Contains("/E"))
						processoService.Processar();
					
					if (vParam.Contains("/I"))
						Instalar();

					if (vParam.Contains("/C"))
						Iniciar();

					if (vParam.Contains("/P"))
						Parar();

					if (vParam.Contains("/U") || vParam.Contains("/D"))
						Desinstalar();

					else
					{
						Console.WriteLine("Parâmetro inválido. Use:");

						Log(LogEnum.Administrativo, "Parâmetro '" + parametros + "' inválido");
					}
				}
			}
			catch (Exception)
			{
				Log(LogEnum.Exception, "Parâmetro '" + parametros + "' inválido");
				vRetorno = -99;
			}

			return vRetorno;
		}

	}

	public interface IProcessoService
	{
		Boolean Processar();
	}

	public enum LogEnum
	{
		Administrativo,
		FalhaInterna,
		Informativo,
		Exception
	}
}