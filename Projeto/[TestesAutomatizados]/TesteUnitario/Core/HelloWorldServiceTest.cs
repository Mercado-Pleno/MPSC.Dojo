using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using MPSTI.Core.Abstracts;
using MPSTI.Core.Proxies;
using MPSTI.Core.Services;
using RestSharp;
using TesteUnitario.Abstracts;
using Xunit;
using System;
using System.Threading.Tasks;

namespace TesteUnitario.Core
{
	public class HelloWorldServiceTest : BaseTest
	{
		protected override void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<HelloWorldService, HelloWorldService>();
		}

		[Fact]
		public async void DeveCumprimentarCorretamenteConformeOHorarioExternalApi()
		{
			ConfigureServices(services => services.AddSingleton(sp => new RestClient("https://worldtimeapi.org/api/")));
			ConfigureServices(services => services.AddSingleton<IDateTimeProvider, WorldTimeProxy>());

			var helloWorldService = GetService<HelloWorldService>();
			var hello = await helloWorldService.Hello("World");

			hello.Should().NotBeNull();
			hello.Should().Be("Hello World! Good Night!");
		}


		[Fact]
		public async void DeveCumprimentarCorretamenteConformeOHorarioLocalTime()
		{
			ConfigureServices(services => services.AddSingleton<IDateTimeProvider, RealTimeProxy>());

			var helloWorldService = GetService<HelloWorldService>();
			var hello = await helloWorldService.Hello("World");

			hello.Should().NotBeNull();
			hello.Should().Be("Hello World! It's still too Early!");
		}


		[Fact]
		public async void DeveCumprimentarCorretamenteConformeOHorarioFakeTime01h()
		{
			var dateTime = DateTime.Today.AddHours(1);
			ConfigureServices(services => services.AddSingleton<IDateTimeProvider>(sp => new FakeTimeProxy(dateTime)));

			var helloWorldService = GetService<HelloWorldService>();
			var hello = await helloWorldService.Hello("World");

			hello.Should().NotBeNull();
			hello.Should().Be("Hello World! It's still too Early!");
		}

		[Fact]
		public async void DeveCumprimentarCorretamenteConformeOHorarioFakeTime09h()
		{
			var dateTime = DateTime.Today.AddHours(9);
			ConfigureServices(services => services.AddSingleton<IDateTimeProvider>(sp => new FakeTimeProxy(dateTime)));

			var helloWorldService = GetService<HelloWorldService>();
			var hello = await helloWorldService.Hello("World");

			hello.Should().NotBeNull();
			hello.Should().Be("Hello World! Good Morning!");
		}

		[Fact]
		public async void DeveCumprimentarCorretamenteConformeOHorarioFakeTime15h()
		{
			var dateTime = DateTime.Today.AddHours(15);
			ConfigureServices(services => services.AddSingleton<IDateTimeProvider>(sp => new FakeTimeProxy(dateTime)));

			var helloWorldService = GetService<HelloWorldService>();
			var hello = await helloWorldService.Hello("World");

			hello.Should().NotBeNull();
			hello.Should().Be("Hello World! Good Afternoon!");
		}

		[Fact]
		public async void DeveCumprimentarCorretamenteConformeOHorarioFakeTime23h()
		{
			var dateTime = new DateTime(2022, 03, 28, 23, 00, 00);
			ConfigureServices(services => services.AddSingleton<IDateTimeProvider>(sp => new FakeTimeProxy(dateTime)));

			var helloWorldService = GetService<HelloWorldService>();
			var hello = await helloWorldService.Hello("World");

			hello.Should().NotBeNull();
			hello.Should().Be("Hello World! Good Night!");
		}
	}

	public class FakeTimeProxy : IDateTimeProvider
	{
		private readonly DateTime _dateTime;
		public FakeTimeProxy(DateTime dateTime) => _dateTime = dateTime;

		public async Task<DateTime> GetUtcNow() => await Task.FromResult(_dateTime);
	}
}