using CommandLine;
using FileConverter.Services;
using FileConverter.Transformers;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileConverter.App
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var result = Parser.Default.ParseArguments<Parameters>(args)
				.WithParsed(parameters => RunOptionsAndReturnExitCode(parameters))
				.WithNotParsed(errors => HandleParseError(errors));
		}

		private static void RunOptionsAndReturnExitCode(Parameters parameters)
		{
			var transformerFactory = new TransformerFactory();
			var dataWrapperService = new DataWrapperService();

			var lines = File.ReadAllLines(parameters.InputFileName);

			var dataWrapper = dataWrapperService.GetDataWrapperFromCsv(lines);
			var transformer = transformerFactory.Get(parameters.Format);
			var formatedFile = transformer.Execute(dataWrapper);

			File.WriteAllText(parameters.OutputFileName, formatedFile);
		}

		public static void HandleParseError(IEnumerable<Error> errors)
		{
			foreach (var error in errors)
				Console.WriteLine(error);
		}
	}
}