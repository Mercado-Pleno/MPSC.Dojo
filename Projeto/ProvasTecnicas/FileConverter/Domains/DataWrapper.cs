using FileConverter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter.Domains
{
	public class DataWrapper
	{
		private const string ColumnGrouping = "_";
		private static readonly char[] ColumnSeparator = new char[] { ';', ',', '\t' };

		public Grouping[] HeaderGroups { get; }
		public List<string> Header { get; } = new List<string>();
		public List<object[]> Rows { get; } = new List<object[]>();

		public DataWrapper(string header)
		{
			var headers = header.Split(ColumnSeparator, StringSplitOptions.RemoveEmptyEntries);
			Header.AddRange(headers);
			HeaderGroups = headers.GroupBy(h => h.Prefix(ColumnGrouping))
				.Select(g => new Grouping(g.Key, g.Select(i => i.Sufix(ColumnGrouping))))
				.ToArray();
		}

		public void AddRow(string values) => Rows.Add(values.Split(ColumnSeparator, StringSplitOptions.RemoveEmptyEntries));

		public Grouping GetGrouping(string columnName)
		{
			var prefix = columnName.Prefix(ColumnGrouping);
			return HeaderGroups.FirstOrDefault(a => a.Prefix == prefix);
		}

		public class Grouping
		{
			public string Prefix { get; }
			public string[] Sufix { get; }

			public Grouping(string prefix, IEnumerable<string> sufix)
			{
				Prefix = prefix;
				Sufix = sufix.ToArray();
			}
		}
	}
}