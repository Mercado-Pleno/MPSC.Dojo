using System;

namespace FileConverter.Transformers
{
	public class TransformerFactory
	{
		public ITransformer Get(FileFormat format)
		{
			return format switch
			{
				FileFormat.Json => new TransformerJson(),
				FileFormat.Xml => new TransformerXml(),
				FileFormat.Tab => new TransformerTxt("\t"),
				FileFormat.Csv => new TransformerTxt(","),
				FileFormat.Ssv => new TransformerTxt(";"),
				_ => throw new NotImplementedException("Formato desconhecido"),
			};
		}
	}
}