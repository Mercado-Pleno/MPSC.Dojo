namespace MP.Library.Exemplos.Transformacao.Serializacao
{
	using System;
	using System.Collections.Generic;

	public interface IDiscretizador
	{
		Object Discretizar(String atributo, String serializacao);
	}

	public interface IDiscretizadorComplexo : IDiscretizador
	{
		KeyValuePair<String, Object> Discretizar(String serializacao);
	}
}