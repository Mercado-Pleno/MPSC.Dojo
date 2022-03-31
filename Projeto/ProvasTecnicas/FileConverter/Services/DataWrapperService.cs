using FileConverter.Domains;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter.Services
{
	public class DataWrapperService
	{
		public DataWrapper GetDataWrapperFromCsv(IEnumerable<string> allLines)
		{
			var lines = allLines.Where(l => !string.IsNullOrWhiteSpace(l));
			var dataWrapper = new DataWrapper(lines.FirstOrDefault());

			foreach (var line in lines.Skip(1))
				dataWrapper.AddRow(line);

			return dataWrapper;
		}
	}
}