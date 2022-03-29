namespace MPSTI.Core.Abstracts
{
	public interface IDateTimeProvider
	{
		Task<DateTime> GetUtcNow();
	}
}