namespace MP.Library.Exemplos.Transformacao.Serializacao
{
	using System;
	using System.Xml.Serialization;
	using System.Text;
	using System.IO;

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

	public static class XmlSerializeExtension
	{
		public static string XmlSerializeToString(this object objectInstance)
		{
			var serializer = new XmlSerializer(objectInstance.GetType());
			var sb = new StringBuilder();

			using (TextWriter writer = new StringWriter(sb))
			{
				serializer.Serialize(writer, objectInstance);
			}

			return sb.ToString();
		}

		public static T XmlDeserializeFromString<T>(this string objectData)
		{
			return (T)XmlDeserializeFromString(objectData, typeof(T));
		}

		public static object XmlDeserializeFromString(this string objectData, Type type)
		{
			var serializer = new XmlSerializer(type);
			object result;

			using (TextReader reader = new StringReader(objectData))
			{
				result = serializer.Deserialize(reader);
			}

			return result;
		}
	}
}