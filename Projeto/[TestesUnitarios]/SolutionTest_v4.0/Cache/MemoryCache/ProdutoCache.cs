using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.TestesUnitarios.SolutionTest_v4.Cache.MemoryCache
{
	public class Produto
	{
		public int Id { get; set; }
	}

	public class ResultadoDaOperacao
	{
		public List<Produto> Valor { get; set; }

		public List<String> Mensagens { get; set; }
	}

	public class ProdutoServiceClient : IDisposable
	{
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		internal ResultadoDaOperacao ObterProdutosDoVG()
		{
			throw new NotImplementedException();
		}

		internal void Close()
		{
			throw new NotImplementedException();
		}
	}

	internal class ProdutoCache : CacheContainer<Int32, Produto, ProdutoCache>
	{
		protected override Produto ObterDadosExternos(Int32 key) { return null; }

		protected override IEnumerable<KeyValuePair<Int32, Produto>> ObterDadosExternos()
		{
			using (var client = new ProdutoServiceClient())
			{
				try
				{
					var result = client.ObterProdutosDoVG();
					if (result == null)
						throw new Exception("Houve um problema interno ao executar esta operação", null);
					else if (result.Valor == null)
						throw new Exception("Houve um problema interno ao executar esta operação", new ApplicationException(String.Join("\r\n", result.Mensagens.ToArray())));

					return result.Valor.Select(p => New(p.Id, p));
				}
				//catch (FaultException exception)
				//{
				//	throw new Exception(String.Format("Não foi possível pesquisar os Produtos, pois o Barramento de Serviços Corporativos estava fora de operação\r\n{0}", exception.Action), exception);
				//}
				catch (Exception exception)
				{
					throw new Exception("Não foi possível pesquisar os Produtos, pois o Barramento de Serviços Corporativos estava fora de operação", exception);
				}
				finally
				{
					client.Close();
				}
			}
		}
	}
}