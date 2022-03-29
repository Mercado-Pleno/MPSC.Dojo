namespace MPSC.Library.Exemplos.BancoDeDados
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
    using System.Data.SqlClient;
    using System.Globalization;
	

	public class VariasCoisasComObjetosDeBancoDeDados : IExecutavel
	{
		public void Executar()
		{
			var vDbConnection = new SqlConnection("Password=SeiNao;Persist Security Info=True;User ID=User;Data Source=192.168.0.11;Initial Catalog=Addsrva1;DefaultCollection=eSimHmo;");
			Acesso vAcesso = new Acesso(vDbConnection);
			vAcesso.ExecutarSQL("Select * From Pessoa P Inner Join Cosseguradora C ON C.PessoaId = P.PessoaId Where C.PessoaId = (Select Max(PessoaId) From ESIMHMO.Cosseguradora)");
		}
	}

	public class Acesso
	{
		DbConnection _DbConnection;
		public Acesso(DbConnection pDbConnection)
		{
			_DbConnection = pDbConnection;
			_DbConnection.Open();
		}

		public IList<AtributoValorado> ExecutarSQL(String pComandoSQL, DbConnection pDbConnection)
		{
			IList<AtributoValorado> vListaAtributoValorTipado = new List<AtributoValorado>();

			DbCommand vDbCommand = pDbConnection.CreateCommand();
			vDbCommand.CommandText = pComandoSQL;

			DbDataReader vDbDataReader = vDbCommand.ExecuteReader();
			IList<Coluna> listaAtributo = vDbDataReader.GetFieldInfo();

			while (vDbDataReader.Read())
			{
				AtributoValorado vAtributoValorado = new AtributoValorado();
				foreach (Coluna vAtributo in listaAtributo)
					vAtributoValorado[vAtributo.Nome] = vDbDataReader[vAtributo.Posicao];

				vListaAtributoValorTipado.Add(vAtributoValorado);
			}

			return vListaAtributoValorTipado;
		}

		public IList<AtributoValorado> ExecutarSQL(String pComandoSQL)
		{
			return ExecutarSQL(pComandoSQL, _DbConnection);
		}
	}

	public class Objeto
	{

		private Object _valor;
		public Type Type { get; set; }

		public Objeto(Object pObjValor)
		{
			Valor = pObjValor;
		}

		public Object Valor
		{
			get
			{
				return _valor;
			}
			set
			{
				Type = ((value == null) ? typeof(Object) : value.GetType());
				_valor = value;
			}
		}

		public Byte Byte
		{
			get
			{
				return (Byte)Valor;
			}
			set
			{
				Type = typeof(Byte);
				Valor = value;
			}
		}

		public Int16 InteiroCurto
		{
			get
			{
				return (Int16)Valor;
			}
			set
			{
				Type = typeof(Int16);
				Valor = value;
			}
		}

		public Int32 Inteiro
		{
			get
			{
				return (Int32)Valor;
			}
			set
			{
				Type = typeof(Int32);
				Valor = value;
			}
		}

		public Int64 InteiroLongo
		{
			get
			{
				return (Int64)Valor;
			}
			set
			{
				Type = typeof(Int64);
				Valor = value;
			}
		}

		public Decimal Decimal
		{
			get
			{
				return (Decimal)Valor;
			}
			set
			{
				Type = typeof(Decimal);
				Valor = value;
			}
		}

		public Double Real
		{
			get
			{
				return (Double)Valor;
			}
			set
			{
				Type = typeof(Double);
				Valor = value;
			}
		}

		public float Flutuante
		{
			get
			{
				return (float)Valor;
			}
			set
			{
				Type = typeof(float);
				Valor = value;
			}
		}

		public DateTime DataHora
		{
			get
			{
				return (DateTime)Valor;
			}
			set
			{
				Type = typeof(DateTime);
				Valor = value;
			}
		}

		public DateTime Data
		{
			get
			{
				return ((DateTime)Valor).Date;
			}
			set
			{
				Type = typeof(DateTime);
				Valor = value.Date;
			}
		}

		public Char Caracter
		{
			get
			{
				return (Char)Valor;
			}
			set
			{
				Type = typeof(Char);
				Valor = value;
			}
		}

		public String Texto
		{
			get
			{
				return (String)Valor;
			}
			set
			{
				Type = typeof(String);
				Valor = value;
			}
		}

		public Boolean Logico
		{
			get
			{
				return (Boolean)Valor;
			}
			set
			{
				Type = typeof(Boolean);
				Valor = value;
			}
		}




	}

	public class AtributoValorado : Dictionary<String, Object>
	{

	}

	public class Coluna
	{
		private string tipo;
		public int Posicao { get; set; }
		public String Nome { get; set; }
		public String Tipo
		{
			set
			{ tipo = value; }
		}
		public Type Type
		{
			get { return typeof(int); }
		}
	}
	public static class Extensao
	{
		public static Objeto Objeto(this Object pObjValor)
		{
			return new Objeto(pObjValor);
		}

		public static IList<Coluna> GetFieldInfo(this DbDataReader pDbDataReader)
		{
			IList<Coluna> lista = new List<Coluna>();
			int vFieldCount = pDbDataReader.FieldCount;
			for (int posicao = 0; posicao < vFieldCount; posicao++)
			{
				lista.Add(new Coluna() { Posicao = posicao, Nome = pDbDataReader.GetName(posicao), Tipo = pDbDataReader.GetDataTypeName(posicao) });

			}

			return lista;
		}

		public static void AddRange<T>(this IList<T> pIListDestino, IEnumerable<T> pIEnumerableOrigem)
		{
			foreach (T vItemLista in pIEnumerableOrigem)
				pIListDestino.Add(vItemLista);
		}

		public static String ToCapitalizeCase(this String pTexto)
		{
			CultureInfo vCultureInfo = new CultureInfo("pt-BR");
			return vCultureInfo.TextInfo.ToTitleCase(pTexto);
		}

		public static String ToString(this DateTime pData)
		{
			return pData.ToString("MMMM").ToCapitalizeCase();

		}
	}
}