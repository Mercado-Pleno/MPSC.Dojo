using System;
using System.CodeDom.Compiler;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.CSharp;

namespace LBJC.NavegadorDeDados
{
	public static class ClasseDinamica
	{
		public static Object CreateObjetoVirtual(Type tipo, IDataReader iDataReader)
		{
			Object obj = ((tipo == null) ? null : Activator.CreateInstance(tipo));
			for (Int32 i = 0; (iDataReader != null) && (!iDataReader.IsClosed) && (i < iDataReader.FieldCount); i++)
			{
				var property = tipo.GetProperty(NomeDoCampo(iDataReader.GetName(i)));
				if (property != null)
					property.SetValue(obj, iDataReader.IsDBNull(i) ? null : iDataReader.GetValue(i), null);
			}
			Application.DoEvents();
			return obj;
		}

		public static Type CriarTipoVirtual(IDataReader iDataReader)
		{
			var properties = String.Empty;
			for (Int32 i = 0; (iDataReader != null) && (!iDataReader.IsClosed) && (i < iDataReader.FieldCount); i++)
			{
				var propertyName = NomeDoCampo(iDataReader.GetName(i));
				if (!properties.Contains(" " + propertyName + " "))
					properties += String.Format("\t\tpublic {0}{1} {2} {{ get; set; }}\r\n", iDataReader.GetFieldType(i).Name, iDataReader.GetFieldType(i).IsValueType ? "?" : "", propertyName);
			}
			return CriarClasseVirtual("DadosDinamicos", properties);
		}

		private static String NomeDoCampo(String original)
		{
			return original.Replace(" ", "_").Replace(".", "_").Replace("\"", "");
		}

		private static Type CriarClasseVirtual(String nomeClasse, String codigo)
		{
			var classe = String.Format("using System;\nnamespace Virtual\n{{\n\tpublic class {0}\n\t{{\n{1}\t}}\n}}", nomeClasse, codigo);
			CodeDomProvider vCodeCompiler = new CSharpCodeProvider();
			CompilerResults vResults = vCodeCompiler.CompileAssemblyFromSource(CreateCompillerParameters(false, true), classe);
			return vResults.CompiledAssembly.GetType("Virtual." + nomeClasse, false, true);
		}

		private static CompilerParameters CreateCompillerParameters(Boolean generateExecutable, Boolean includeDebugInformation)
		{
			return new CompilerParameters(new String[] { Path.GetFileName(Assembly.GetExecutingAssembly().CodeBase) })
			{
				GenerateInMemory = !generateExecutable,
				GenerateExecutable = generateExecutable,
				IncludeDebugInformation = includeDebugInformation
			};
		}
	}
}