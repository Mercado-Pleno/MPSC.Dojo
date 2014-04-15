namespace MP.Library.Exemplos.Transformacao.Serializacao
{
	using System;
	using System.Diagnostics;

	public abstract class TranformObject : ISerializador, IDiscretizador
	{
		protected String marca1 { get; private set; }
		protected String marca2 { get; private set; }

		public TranformObject(String start, String end) { IfDebugMode(start, end); }

		[Conditional("DEBUG")]
		private void IfDebugMode(String start, String end)
		{
			marca1 = start;
			marca2 = end;
		}

		public abstract String Serializar(String atributo, Object valor);
		public abstract Object Discretizar(String atributo, String serializacao);

		protected String Extrair(String serializacao, String strI, String strF)
		{
			String retorno = String.Empty;
			int posicao = serializacao.IndexOf(strI);
			if (posicao >= 0)
			{
				serializacao = serializacao.Substring(posicao + strI.Length);
				posicao = serializacao.IndexOf(strF);
				if (posicao >= 0)
					retorno = serializacao.Substring(0, posicao);
			}
			return retorno;
		}
	}
}