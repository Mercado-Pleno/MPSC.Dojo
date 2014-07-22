﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.CSharp;

namespace LBJC.NavegadorDeDados
{
	public class ClasseDinamica : IDisposable
	{
		private Type _tipo = null;
		private Conexao _conexao = null;
		public Conexao Conexao { get { return (_conexao ?? (_conexao = new Conexao())); } }

		public Object Executar(String query)
		{
			var dataReader = Conexao.Executar(query);
			
			if (dataReader != null)
			{
				var properties = String.Empty;
				var colunas = dataReader.FieldCount;
				for (Int32 i = 0; i < colunas; i++)
				{
					var propertyName = dataReader.GetName(i);
					if (!properties.Contains(" " + propertyName + " "))
						properties += String.Format("\t\tpublic {0}{1} {2} {{ get; set; }}\r\n", dataReader.GetFieldType(i).Name, dataReader.GetFieldType(i).IsValueType ? "?" : "", propertyName);
				}
				_tipo = CriarClasseVirtual("DadosDinamicos", properties);
			}
			return null;
		}

		public IEnumerable<Object> Transformar()
		{
			var page = 0;
			while ((_conexao != null) && (_conexao.iDataReader != null) && !_conexao.iDataReader.IsClosed && _conexao.iDataReader.Read() && page++ < 100)
				yield return CreateAnonymousObject(_conexao.iDataReader);
		}

		private Object CreateAnonymousObject(IDataReader dataReader)
		{
			var obj = Activator.CreateInstance(_tipo);
			var colunas = dataReader.FieldCount;
			for (Int32 i = 0; i < colunas; i++)
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
			CodeDomProvider vCodeCompiler = new CSharpCodeProvider();
			CompilerParameters vCompilerParameters = new CompilerParameters();
			vCompilerParameters.GenerateInMemory = true;
			vCompilerParameters.GenerateExecutable = false;
			vCompilerParameters.IncludeDebugInformation = true;
			var classe = String.Format("using System;\nnamespace LBJC.Virtual\n{{\n\tpublic class {0}\n\t{{\n{1}\t}}\n}}", nomeClasse, codigo);
			try
			{
				CompilerResults vResults = vCodeCompiler.CompileAssemblyFromSource(vCompilerParameters, classe);
				vType = vResults.CompiledAssembly.GetType("LBJC.Virtual." + nomeClasse, true);
			}
			catch (Exception) { }

#pragma warning restore 0618
			return vType;
		}

		public void Dispose()
		{
			if (_tipo != null)
				_tipo = null;

			if (_conexao != null)
			{
				_conexao.Dispose();
				_conexao = null;
			}
		}
	}
}