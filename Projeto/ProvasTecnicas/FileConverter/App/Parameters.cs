using CommandLine;
using FileConverter.Transformers;
using System;
using System.IO;

namespace FileConverter.App
{
	public class Parameters
	{
		[Option('F', "FileName", Required = false, HelpText = "Full FileName Input")]
		public string InputFileName { get; set; }

		[Option('O', "OutFormat", Required = false, HelpText = "Format Of Output")]
		public string OutFormat { get; set; }

		public FileFormat Format => Enum.Parse<FileFormat>(OutFormat, true);

		private string Extension => Format.ToString().ToLower();

		public string OutputFileName => Path.ChangeExtension(InputFileName, Extension);
	}
}