namespace MP.LBJC.Util
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Configuration.Install;
	using System.IO;
	using System.Reflection;
	using System.ServiceProcess;

	[RunInstaller(true)]
	public class ServiceInstallerUtil : Installer
	{
		private IContainer components = null;
		private ServiceInstaller serviceInstaller;
		private ServiceProcessInstaller serviceProcessInstaller;

		public ServiceInstallerUtil(String serviceName, String displayName, String description)
		{
			InitializeComponent(serviceName, displayName, description);
		}

		private void InitializeComponent(String serviceName, String displayName, String description)
		{
			this.serviceProcessInstaller = new ServiceProcessInstaller();
			this.serviceInstaller = new ServiceInstaller();

			this.serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			this.serviceProcessInstaller.Password = null;
			this.serviceProcessInstaller.Username = null;

			this.serviceInstaller.ServiceName = serviceName;
			this.serviceInstaller.DisplayName = displayName;
			this.serviceInstaller.Description = description;
			this.serviceInstaller.StartType = ServiceStartMode.Automatic;

			this.Installers.AddRange(new Installer[] { this.serviceProcessInstaller, this.serviceInstaller });
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		public static void Instalar<T>(String nomeDoServico)
		{
			Instalar(nomeDoServico, typeof(T).Assembly);
		}

		public static void Instalar(String nomeDoServico, Assembly assembly)
		{
			InstalarServico(nomeDoServico, assembly);
			Iniciar(nomeDoServico);
		}

		public static void Desinstalar<T>(String nomeDoServico)
		{
			Desinstalar(nomeDoServico, typeof(T).Assembly);
		}

		public static void Desinstalar(String nomeDoServico, Assembly assembly)
		{
			Parar(nomeDoServico);
			DesinstalarServico(nomeDoServico, assembly);
		}

		public static Boolean Log(String nomeDoServico, String tipo, String mensagem)
		{
			return Log(@"C:\MPSC\", nomeDoServico, tipo, mensagem);
		}

		public static Boolean Log(String path, String nomeDoServico, String tipo, String mensagem)
		{
			Boolean vRetorno = false;
			mensagem = String.Format("{0} {1} -> {2}", tipo, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"), mensagem);
			try
			{
				StreamWriter vStreamWriter = new StreamWriter(path + nomeDoServico.Replace(" ", "_").Replace("/", ".") + ".log", true);
				vStreamWriter.WriteLine(mensagem);
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

		public static Boolean Iniciar(String nomeDoServico)
		{
			Boolean vRetorno = EstaInstalado(nomeDoServico);
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
					Log(nomeDoServico, "F", exception.Message);
				}
			}
			return vRetorno;
		}

		public static Boolean Parar(String nomeDoServico)
		{
			Boolean vRetorno = EstaInstalado(nomeDoServico);
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
					Log(nomeDoServico, "F", exception.Message);
				}
			}
			return vRetorno;
		}

		public static Boolean EstaInstalado(String nomeDoServico)
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
				Log(nomeDoServico, "F", exception.Message);
			}
			return vRetorno;
		}

		public static Boolean EstaRodando(String nomeDoServico)
		{
			Boolean vRetorno = EstaInstalado(nomeDoServico);
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
					Log(nomeDoServico, "F", exception.Message);
				}
			}
			return vRetorno;
		}

		private static Boolean InstalarServico(String nomeDoServico, Assembly assembly)
		{
			Log(nomeDoServico, "A", "Instalando o Serviço");
			Boolean vRetorno = EstaInstalado(nomeDoServico);
			if (!vRetorno)
			{
				try
				{
					using (AssemblyInstaller installer = GetInstaller(assembly))
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
							Log(nomeDoServico, "F", exception.Message);
							installer.Rollback(state);
						}
					}
					vRetorno = EstaInstalado(nomeDoServico);
				}
				catch (Exception exception)
				{
					vRetorno = false;
					Log(nomeDoServico, "F", exception.Message);
				}
			}
			return vRetorno;
		}

		private static Boolean DesinstalarServico(String nomeDoServico, Assembly assembly)
		{
			Log(nomeDoServico, "A", "Desinstalando o Serviço");
			Boolean vRetorno = !EstaInstalado(nomeDoServico);
			if (!vRetorno)
			{
				try
				{
					using (AssemblyInstaller installer = GetInstaller(assembly))
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
							Log(nomeDoServico, "F", exception.Message);
							installer.Rollback(state);
						}
					}
				}
				catch (Exception exception)
				{
					vRetorno = false;
					Log(nomeDoServico, "F", exception.Message);
				}
			}
			return vRetorno;
		}

		private static AssemblyInstaller GetInstaller(Assembly assembly)
		{
			return new AssemblyInstaller(assembly, null) { UseNewContext = true };
		}

		public static void ProcessarParametro(String nomeDoServico, String param)
		{
			if (param.Equals("/I"))
				ServiceInstallerUtil.Instalar(nomeDoServico, Assembly.GetExecutingAssembly());
			else if (param.Equals("/U"))
				ServiceInstallerUtil.Desinstalar(nomeDoServico, Assembly.GetExecutingAssembly());
			else
				ServiceInstallerUtil.Log(nomeDoServico, "F", "Parâmetro '" + param + "' inválido");
		}
	}
}