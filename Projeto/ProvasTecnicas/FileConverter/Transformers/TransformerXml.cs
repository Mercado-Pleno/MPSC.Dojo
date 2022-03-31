using FileConverter.Domains;
using System.Collections.Generic;

namespace FileConverter.Transformers
{
	public class TransformerXml : ITransformer
	{
		public string Execute(DataWrapper dataWrapper)
		{
			var jsonResult = SerializeLines(dataWrapper);
			return $"<Root>{string.Join("", jsonResult)}</Root>";
		}

		private IEnumerable<string> SerializeLines(DataWrapper dataWrapper)
		{
			foreach (var row in dataWrapper.Rows)
			{
				var json = SerializeLine(dataWrapper, row);
				yield return json;
			}
		}

		private string SerializeLine(DataWrapper dataWrapper, object[] row)
		{
			var jsonResult = SerializeColumns(dataWrapper, row);
			return $"<Entity>{string.Join("", jsonResult)}</Entity>";
		}

		private IEnumerable<string> SerializeColumns(DataWrapper dataWrapper, object[] row)
		{
			var i = 0;
			while (i < dataWrapper.Header.Count)
			{
				var column = dataWrapper.Header[i];
				var grouping = dataWrapper.GetGrouping(column);

				var values = string.Join("", SerializeColumn(row, i, grouping.Sufix));
				i += grouping.Sufix.Length;

				if (grouping.Sufix.Length > 1)
					yield return $@"<{grouping.Prefix}>{values}</{grouping.Prefix}>";
				else
					yield return values;
			}
		}

		private static IEnumerable<string> SerializeColumn(object[] row, int index, IEnumerable<string> sufixes)
		{
			foreach (var sufix in sufixes)
			{
				var value = row[index];
				yield return $@"<{sufix}>{value}</{sufix}>";
				index++;
			}
		}
	}
}