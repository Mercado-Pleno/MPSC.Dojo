using FileConverter.Domains;
using System.Collections.Generic;

namespace FileConverter.Transformers
{
	public class TransformerTxt : ITransformer
	{
		private readonly string _separator;
		public TransformerTxt(string separator) => _separator = separator;

		public string Execute(DataWrapper dataWrapper)
		{
			var jsonResult = SerializeLines(dataWrapper);
			return string.Join("\r\n", jsonResult);
		}

		private IEnumerable<string> SerializeLines(DataWrapper dataWrapper)
		{
			yield return string.Join(_separator, dataWrapper.Header);
			foreach (var row in dataWrapper.Rows)
				yield return string.Join(_separator, row);
		}
	}
}