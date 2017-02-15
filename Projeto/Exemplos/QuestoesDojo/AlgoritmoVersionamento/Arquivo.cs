using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPSC.Library.Exemplos.QuestoesDojo.AlgoritmoVersionamento
{
	public class Versionador
	{
		private readonly Arquivo versaoInicial = new Arquivo();
		public readonly List<Modificacao> _modificacoes = new List<Modificacao>();
		public Int32 Versao { get { return _modificacoes.Count(); } }

		public void Versionar(Arquivo arquivo)
		{
			if (versaoInicial.EstaVazio)
				versaoInicial.Adicionar(arquivo.Conteudo);
			else
			{
				var arquivoAtual = ObterArquivo();
				var modificacoes = comparar(arquivoAtual, arquivo).OrderBy(m => m.Prioridade).ThenBy(m => m.Linha.Posicao).ToArray();
				_modificacoes.AddRange(modificacoes);
			}
		}

		public Arquivo ObterArquivo()
		{
			var arquivo = new Arquivo(versaoInicial.Linhas);
			_modificacoes.ForEach(m => m.Aplicar(arquivo));
			return arquivo;
		}

		private IEnumerable<Modificacao> comparar(Arquivo arquivoA, Arquivo arquivoB)
		{
			foreach (var linhaA in arquivoA.Linhas)
			{
				var linhaB = arquivoB.Linhas.FirstOrDefault(l => l.Conteudo == linhaA.Conteudo);
				if (linhaB == null)
					yield return new Exclusao(linhaA);
			}

			foreach (var linhaB in arquivoB.Linhas)
			{
				var linhaA = arquivoA.Linhas.FirstOrDefault(l => l.Conteudo == linhaB.Conteudo);
				if (linhaA == null)
					yield return new Insercao(linhaB);
				else if (linhaB.Posicao != linhaA.Posicao)
					yield return new Adicao(linhaB);
			}
		}

	}

	public static class Extension
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> acao)
		{
			foreach (var item in source)
				acao(item);
		}
	}

	public class Arquivo
	{
		private readonly List<Linha> _linhas = new List<Linha>();
		public Arquivo(IEnumerable<Linha> linhas) { Adicionar(linhas); }
		public Arquivo(String conteudo) { Adicionar(conteudo); }
		public Arquivo(params String[] linhas) { Adicionar(linhas); }

		public void Adicionar(String conteudo)
		{
			Adicionar(conteudo.Split(new[] { "\r\n" }, StringSplitOptions.None));
		}

		private void Adicionar(IEnumerable<String> linhas)
		{
			Adicionar(linhas.Select((l, i) => new Linha(l, i + 1)));
		}

		public void Adicionar(IEnumerable<Linha> linhas)
		{
			linhas.ForEach(Inserir);
		}

		public void Inserir(Linha linha)
		{
			var posicaoDaProximaLinha = PosicaoDaUltimaLinha + 1;
			if (linha.Posicao < posicaoDaProximaLinha)
				_linhas.Where(l => l.Posicao >= linha.Posicao).ForEach(l => l.Avancar());
			else if (linha.Posicao > posicaoDaProximaLinha)
			{
				var quantidade = linha.Posicao - posicaoDaProximaLinha;
				var linhasEmBranco = Enumerable.Range(posicaoDaProximaLinha, quantidade);
				_linhas.AddRange(linhasEmBranco.Select(p => new Linha(String.Empty, p)));
			}
			_linhas.Add(linha);
		}

		public void Atualizar(Linha linha)
		{
			Remover(linha);
			_linhas.Add(linha);
		}

		public void Remover(Linha linha)
		{
			var line = _linhas.FirstOrDefault(l => l.Posicao == linha.Posicao);
			if (line != null)
			{
				_linhas.Where(l => l.Posicao > line.Posicao).ForEach(l => l.Retroceder());
				_linhas.Remove(line);
			}
		}

		private Int32 PosicaoDaUltimaLinha { get { return _linhas.Any() ? _linhas.Max(l => l.Posicao) : 0; } }

		public String Conteudo { get { return String.Join("\r\n", Linhas.Select(l => l.Conteudo)); } }

		public Boolean EstaVazio { get { return !_linhas.Any(); } }

		public IEnumerable<Linha> Linhas { get { return _linhas.OrderBy(l => l.Posicao); } }

		public override String ToString()
		{
			return Conteudo;
		}
	}

	public class Linha
	{
		public readonly String Conteudo;
		private Int32 _posicao;

		public Int32 Posicao { get { return _posicao; } }

		public Linha(String conteudo, Int32 posicao)
		{
			Conteudo = conteudo;
			_posicao = posicao;
		}

		public void Avancar()
		{
			_posicao++;
		}
		public void Retroceder()
		{
			_posicao--;
		}
		public override String ToString()
		{
			return Conteudo;
		}
	}

	public abstract class Modificacao
	{
		public int Prioridade { get; private set; }
		public Linha Linha { get; private set; }

		public Modificacao(Linha linha, Int32 prioridade) { Linha = new Linha(linha.Conteudo, linha.Posicao); Prioridade = prioridade; }
		public abstract void Aplicar(Arquivo arquivo);

		public override String ToString()
		{
			return String.Format("{0} {1}", GetType().Name, Linha.ToString());
		}
	}
	public class Exclusao : Modificacao
	{
		public Exclusao(Linha linha) : base(linha, 1) { }
		public override void Aplicar(Arquivo arquivo)
		{
			arquivo.Remover(Linha);
		}
	}

	public class Adicao : Modificacao
	{
		public Adicao(Linha linha) : base(linha, 2) { }

		public override void Aplicar(Arquivo arquivo)
		{
			arquivo.Atualizar(Linha);
		}
	}


	public class Insercao : Modificacao
	{
		public Insercao(Linha linha) : base(linha, 3) { }
		public override void Aplicar(Arquivo arquivo)
		{
			arquivo.Inserir(Linha);
		}
	}
}