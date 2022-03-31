using FileConverter.Domains;

namespace FileConverter.Transformers
{
	public interface ITransformer
	{
		string Execute(DataWrapper dataWrapper);
	}
}