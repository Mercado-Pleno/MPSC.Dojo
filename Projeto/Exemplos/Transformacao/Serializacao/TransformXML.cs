namespace MP.Library.Exemplos.Transformacao.Serializacao
{
	using System;

	public class TransformXML : TranformObject
	{
		public TransformXML() : base("\t", "\r\n") { }

		public override String Serializar(String atributo, Object valor)
		{
			return marca1 + "<" + atributo + ">" + valor + "</" + atributo + ">" + marca2;
		}

		public override Object Discretizar(String atributo, String serializacao)
		{
			return ExtrairXML(serializacao, atributo);
		}

		private String ExtrairXML(String xml, String atributo)
		{
			return Extrair(xml, "<" + atributo + ">", "</" + atributo + ">");
		}
	}
}