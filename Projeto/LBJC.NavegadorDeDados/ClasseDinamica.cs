using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.CSharp;

namespace LBJC.NavegadorDeDados
{
	public class ClasseDinamica
	{
		private Type _tipo = null;
		private IDataReader _dataReader = null;


		public ClasseDinamica(IDataReader dataReader) { Reset(dataReader); }

		public void Reset(IDataReader dataReader)
		{
			if (dataReader != null)
			{
				_dataReader = dataReader;
				var props = String.Empty;
				var colunas = dataReader.FieldCount;
				for (int i = 0; i < colunas; i++)
				{
					var prop = String.Format("public {0} {1} {{ get; set; }}\r\n", dataReader.GetFieldType(i).Name, dataReader.GetName(i));
					props += prop;
				}
				_tipo = CriarClasseVirtual("DadosDinamicos", props);
			}
		}

		public IEnumerable<Object> Transformar()
		{
			var page = 0;
			while (_dataReader.Read() && page++ < 100)
				yield return CreateAnonymousObject(_dataReader);
		}

		private Object CreateAnonymousObject(IDataReader dataReader)
		{
			var obj = Activator.CreateInstance(_tipo);
			var colunas = dataReader.FieldCount;
			for (int i = 0; i < colunas; i++)
			{
				var prop = _tipo.GetProperty(dataReader.GetName(i));
				prop.SetValue(obj, dataReader.IsDBNull(i) ? null : dataReader.GetValue(i), null);
				Application.DoEvents();
			}
			Application.DoEvents();
			return obj;
		}


		private Type CriarClasseVirtual(String nomeClasse, String codigo)
		{
#pragma warning disable 0618
			Type vType = null;
			//ICodeCompiler vCodeCompiler = (new CSharpCodeProvider() as CodeDomProvider).CreateCompiler();
			CodeDomProvider vCodeCompiler = new CSharpCodeProvider();
			CompilerParameters vCompilerParameters = new CompilerParameters();
			vCompilerParameters.GenerateInMemory = true;
			vCompilerParameters.GenerateExecutable = false;
			vCompilerParameters.IncludeDebugInformation = true;
			var classe = @"using System; namespace LBJC.Virtual {{ public class {0} {{ {1} }} }}";
			try
			{
				CompilerResults vResults = vCodeCompiler.CompileAssemblyFromSource(vCompilerParameters, String.Format(classe, nomeClasse, codigo));
				vType = vResults.CompiledAssembly.GetType("LBJC.Virtual." + nomeClasse, true);
			}
			catch (Exception) { }

#pragma warning restore 0618
			return vType;
		}
	}
}