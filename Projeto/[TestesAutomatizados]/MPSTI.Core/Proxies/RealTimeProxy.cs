using MPSTI.Core.Abstracts;

namespace MPSTI.Core.Proxies
{
	public class RealTimeProxy : IDateTimeProvider
	{
		public async Task<DateTime> GetUtcNow() => await Task.FromResult(DateTime.UtcNow);
	}
}