using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace TesteUnitario.Abstracts
{
	public static class ServiceCollectionMockExtension
	{
		public static TService CreateMock<TService>(this IServiceProvider serviceProvider, Action<Mock<TService>> mockSetup = null) where TService : class
		{
			var mockService = new Mock<TService>(serviceProvider);
			mockSetup?.Invoke(mockService);
			return mockService.Object;
		}

		public static IServiceCollection AddSingletonMock<TService>(this IServiceCollection serviceCollection, Action<Mock<TService>> mockSetup = null) where TService : class
		{
			return serviceCollection.AddSingleton<TService>(serviceProvider => CreateMock<TService>(serviceProvider, mockSetup));
		}

		public static IServiceCollection AddScopedMock<TService>(this IServiceCollection serviceCollection, Action<Mock<TService>> mockSetup = null) where TService : class
		{
			return serviceCollection.AddScoped<TService>(serviceProvider => CreateMock<TService>(serviceProvider, mockSetup));
		}

		public static IServiceCollection AddTransientMock<TService>(this IServiceCollection serviceCollection, Action<Mock<TService>> mockSetup = null) where TService : class
		{
			return serviceCollection.AddTransient<TService>(serviceProvider => CreateMock<TService>(serviceProvider, mockSetup));
		}
	}
}