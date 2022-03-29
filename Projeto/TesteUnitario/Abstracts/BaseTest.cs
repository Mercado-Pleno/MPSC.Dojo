using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TesteUnitario.Abstracts
{
	public abstract class BaseTest
	{
		protected static readonly IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();

		private IServiceProvider _serviceProvider;
		private IServiceProvider ServiceProvider => _serviceProvider ??= ServiceCollection.BuildServiceProvider();

		private IServiceCollection _serviceCollection;
		private IServiceCollection ServiceCollection => _serviceCollection ??= GetServiceCollection();

		private IServiceCollection GetServiceCollection()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddSingleton(sp => Configuration);
			ConfigureServices(serviceCollection);
			return serviceCollection;
		}
		protected abstract void ConfigureServices(IServiceCollection services);

		protected void ConfigureServices(Action<IServiceCollection> services) => services.Invoke(ServiceCollection);

		protected TService GetService<TService>() => ServiceProvider.GetRequiredService<TService>();
	}
}