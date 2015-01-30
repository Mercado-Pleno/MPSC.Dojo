namespace MPSC.Library.Exemplos.Transformacao.Serializacao
{
	using System;

	public class TransformCNP : TranformObject
	{
		public TransformCNP() : base("", "\r\n") { }

		public override String Serializar(String atributo, Object valor)
		{
			return ", " + atributo + " = " + valor;
		}

		public override Object Discretizar(String atributo, String serializacao)
		{
			return Extrair(serializacao, ", " + atributo + " = ", " ");
		}
	}
}