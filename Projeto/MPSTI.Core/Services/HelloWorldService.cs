using MPSTI.Core.Abstracts;

namespace MPSTI.Core.Services
{
	public class HelloWorldService : BaseClass
	{
		private readonly IDateTimeProvider _dateTimeProvider;

		public HelloWorldService(IServiceProvider serviceProvider) : base(serviceProvider) 
		{
			_dateTimeProvider = GetService<IDateTimeProvider>();
		}

		public async Task<string> Hello(string planet)
		{
			var greet = $"Hello {planet}! ";
			var dateTime = await _dateTimeProvider.GetUtcNow();

			if (dateTime.Hour < 6)
				greet += "It's still too Early!";

			else if (dateTime.Hour < 12)
				greet += "Good Morning!";
			else if (dateTime.Hour < 18)
				greet += "Good Afternoon!";
			else
				greet += "Good Night!";

			return greet;
		}
	}
}