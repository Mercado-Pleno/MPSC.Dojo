namespace MPSC.Library.Exemplos.Transformacao
{
	using System;
	using MP.Library.Exemplos.Transformacao.Serializacao;

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

			TransformManager vTransformManager = new TransformManager();
			vTransformManager.Discretizador = new TransformJSON();
			vTransformManager.Serializador = new TransformJSON();
			Console.WriteLine(vTransformManager.Serializar("Cliente", new Cliente()));
		}
	}

	public class Cliente
	{
		public String Nome { get; set; }
		public String Idade { get; set; }
		public String Sexo { get; set; }
		public String Documento { get; set; }
		public String NomeMae { get; set; }
		public String NomePai { get; set; }
	}
}