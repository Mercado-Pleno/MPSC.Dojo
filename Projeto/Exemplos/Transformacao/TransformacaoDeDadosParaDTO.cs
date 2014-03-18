namespace MPSC.Library.Exemplos.Transformacao
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	public class TransformacaoDeDadosParaDTO : IExecutavel
	{
		public void Executar()
		{
			TranformObject vTranformObject = new TransformXML();
			var str = vTranformObject.Serializar("Nome", "Bruno");
			Console.WriteLine(str);
			Console.WriteLine(vTranformObject.Discretizar("Nome", str));

			vTranformObject = new TransformCleanJSON();
			str = vTranformObject.Serializar("Nome", "Bruno");
			Console.WriteLine(str);
			Console.WriteLine(vTranformObject.Discretizar("Nome", str));

			vTranformObject = new TransformJSON();
			str = vTranformObject.Serializar("Nome", "Bruno");
			Console.WriteLine(str);
			Console.WriteLine(vTranformObject.Discretizar("Nome", str));
		}
	}

	public interface ISerializador
	{
		String Serializar(String atributo, Object valor);
	}

	public interface IDiscretizador
	{
		Object Discretizar(String atributo, String serializacao);
	}

	public interface IDiscretizadorComplexo : IDiscretizador
	{
		KeyValuePair<String, Object> Discretizar(String serializacao);
	}

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