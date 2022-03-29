using Microsoft.Extensions.DependencyInjection;

namespace MPSTI.Core.Abstracts
{
	public class BaseClass
	{
		protected readonly IServiceProvider _serviceProvider;
		protected TService GetService<TService>() => _serviceProvider.GetRequiredService<TService>();

		public BaseClass(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
	}
}