namespace MP.Library.Exemplos.Transformacao.Serializacao
{
	using System;

	public interface ISerializador
	{
		String Serializar(String atributo, Object valor);
	}
}