namespace MPSC.Library.Exemplos.Transformacao.Serializacao
{
	using System;

	public class TransformCleanJSON : TranformObject
	{
		public TransformCleanJSON() : base("\"", " ") { }

		//http://www.json.org/example.html
		public override String Serializar(String atributo, Object valor)
		{
			return marca1 + atributo + marca1 + marca2 + ":" + marca2 + "\"" + valor + "\"" + " ";
		}

		public override Object Discretizar(String atributo, String serializacao)
		{
			return Extrair(serializacao, marca1 + atributo + marca1 + marca2 + ":" + marca2 + "\"", "\" ");
		}
	}
}