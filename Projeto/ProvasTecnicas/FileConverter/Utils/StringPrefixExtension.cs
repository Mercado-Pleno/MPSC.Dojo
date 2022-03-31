namespace FileConverter.Utils
{
	public static class StringPrefixExtension
	{
		public static string Prefix(this string self, string separator)
		{
			var index = self.IndexOf(separator);
			return index > 0 ? self.Substring(0, index) : self;
		}

		public static string Sufix(this string self, string separator)
		{
			var index = self.IndexOf(separator);
			return index > 0 ? self.Substring(index + separator.Length) : self;
		}
	}
}