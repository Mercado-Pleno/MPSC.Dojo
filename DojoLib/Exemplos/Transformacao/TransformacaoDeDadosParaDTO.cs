namespace MPSC.Library.Exemplos.Transformacao
{
	using System;

	public interface IFormatoDTO
	{
		String ObterCampo(String pNomeAtributo, Object pObjeto);
	}

	public class XMLFormatoDTO : IFormatoDTO
	{
		public String ObterCampo(String pNomeAtributo, Object pObjeto)
		{
			return "\t<" + pNomeAtributo + ">" + pObjeto + "</" + pNomeAtributo + ">\r\n";
		}
	}

	public class JSONFormatoDTO : IFormatoDTO
	{
		//http://www.json.org/example.html
		public String ObterCampo(String pNomeAtributo, Object pObjeto)
		{
			return " \"" + pNomeAtributo + "\": \"" + pObjeto + "\"";
		}
	}

	public class CNPFormatoDTO : IFormatoDTO
	{
		public String ObterCampo(String pNomeAtributo, Object pObjeto)
		{
			return ", " + pNomeAtributo + " = " + pObjeto;
		}
	}
}