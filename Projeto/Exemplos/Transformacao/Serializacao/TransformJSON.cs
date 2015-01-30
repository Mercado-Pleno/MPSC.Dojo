namespace MPSC.Library.Exemplos.Transformacao.Serializacao
{
	using System;

	public class TransformJSON : TranformObject
	{
		public TransformJSON() : base(" ", "\r\n") { }

		//http://www.json.org/example.html
		public override String Serializar(String atributo, Object valor)
		{
			return "\"" + atributo + "\"" + marca1 + ":" + marca1 + "\"" + valor + "\" " + marca2;
		}

		public override Object Discretizar(String atributo, String serializacao)
		{
			return Extrair(serializacao, "\"" + atributo + "\"" + marca1 + ":" + marca1 + "\"", "\" ");
		}
	}
}