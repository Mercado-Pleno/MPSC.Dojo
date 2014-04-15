namespace MP.Library.Exemplos.Transformacao.Serializacao
{
	using System;
	using System.Collections.Generic;

	public class TransformManager
	{
		public ISerializador Serializador { get; set; }
		public IDiscretizador Discretizador { get; set; }

		public String Serializar(String nome, Object obj)
		{
			String vRetorno = String.Empty;
			IEnumerable<KeyValuePair<String, Object>> atributos = GetAtributos(obj);
			foreach (var item in atributos)
				vRetorno += Serializador.Serializar(item.Key, item.Value);
			return Serializador.Serializar(nome, vRetorno);
		}

		private IEnumerable<KeyValuePair<String, Object>> GetAtributos(object obj)
		{
			var properties = obj.GetType().GetProperties();
			foreach (var item in properties)
				yield return new KeyValuePair<String, Object>(item.Name, item.GetValue(obj, null));
		}
	}
}