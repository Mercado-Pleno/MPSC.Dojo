using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo.AlgoritmoVersionamento;
using System;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo.AlgoritmoVersionamento
{
	[TestClass]
	public class TestandoVersionador
	{
		private Versionador versionador = new Versionador();

		[TestInitialize]
		public void InicializarTeste()
		{
			var arquivo = new Arquivo("Bruno");
			versionador.Versionar(arquivo);
			Assert.AreEqual(0, versionador.Versao);
		}

		[TestMethod]
		public void Quando_Versiona_O_Mesmo_Arquivo_Nao_Gera_Alteracoes()
		{
			var arquivo = new Arquivo("Bruno");
			versionador.Versionar(arquivo);
			Assert.AreEqual(0, versionador.Versao);
		}

		[TestMethod]
		public void Quando_Versiona_Arquivo_Com1_Alteracao_OVersionador_DeveSer_Versao1()
		{
			var arquivo = new Arquivo("Bruno", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(1, versionador.Versao);

			var arquivoVersionado = versionador.ObterArquivo();
			Assert.AreEqual(arquivo.Conteudo, arquivoVersionado.Conteudo);
		}

		[TestMethod]
		public void Quando_Versiona_Arquivo_Com2_Alteracao_OVersionador_DeveSer_Versao2()
		{
			var arquivo = new Arquivo("Bruno", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(1, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Nogueira", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(3, versionador.Versao);

			var arquivoVersionado = versionador.ObterArquivo();
			Assert.AreEqual(arquivo.Conteudo, arquivoVersionado.Conteudo);
		}

		[TestMethod]
		public void Quando_Versiona_Arquivo_Com5_Alteracao_OVersionador_DeveSer_Versao5()
		{
			var arquivo = new Arquivo("Bruno", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(1, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Nogueira", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(3, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Nogueira", "Fernandes", "Luani", "Santos");
			versionador.Versionar(arquivo);
			Assert.AreEqual(5, versionador.Versao);

			var arquivoVersionado = versionador.ObterArquivo();
			Assert.AreEqual(arquivo.Conteudo, arquivoVersionado.Conteudo);
		}

		[TestMethod]
		public void Quando_Versiona_Arquivo_Com6_Alteracao_OVersionador_DeveSer_Versao6()
		{
			var arquivo = new Arquivo("Bruno", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(1, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Nogueira", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(3, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Nogueira", "Fernandes", "Luani", "Santos");
			versionador.Versionar(arquivo);
			Assert.AreEqual(5, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Fernandes", "Luani", "Santos");
			versionador.Versionar(arquivo);
			Assert.AreEqual(6, versionador.Versao);

			var arquivoVersionado = versionador.ObterArquivo();
			Assert.AreEqual(arquivo.Conteudo, arquivoVersionado.Conteudo);
		}

		[TestMethod]
		public void Quando_Versiona_Arquivo_Com7_Alteracao_OVersionador_DeveSer_Versao7()
		{
			var arquivo = new Arquivo("Bruno", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(1, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Nogueira", "Fernandes");
			versionador.Versionar(arquivo);
			Assert.AreEqual(3, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Nogueira", "Fernandes", "Luani", "Santos");
			versionador.Versionar(arquivo);
			Assert.AreEqual(5, versionador.Versao);

			arquivo = new Arquivo("Bruno", "Fernandes", "Luani", "Santos", "Papae");
			versionador.Versionar(arquivo);
			Assert.AreEqual(7, versionador.Versao);

			var arquivoVersionado = versionador.ObterArquivo();
			Assert.AreEqual(arquivo.Conteudo, arquivoVersionado.Conteudo);
		}
	}


	[TestClass]
	public class TestandoArquivo
	{
		[TestMethod]
		public void Quando_Cria_Um_Arquivo_Seu_Conteudo_Deve_Ser_Vazio()
		{
			var arquivo = new Arquivo();

			Assert.AreEqual(String.Empty, arquivo.Conteudo);
		}

		[TestMethod]
		public void Quando_Cria_Um_Arquivo_E_Adiciona_UmaLinha_Seu_Conteudo_Deve_Ser_O_Conteudo_DaLinha()
		{
			var arquivo = new Arquivo();
			var linha = new Linha("Bruno", 1);
			arquivo.Atualizar(linha);

			Assert.AreEqual(linha.Conteudo, arquivo.Conteudo);
		}

		[TestMethod]
		public void Quando_Cria_Um_Arquivo_E_Adiciona_Uma_Linha3_Seu_Conteudo_Deve_Ser_2Linhas_Em_Branco_e_Conteudo_DaLinha()
		{
			var arquivo = new Arquivo();
			var linha = new Linha("Bruno", 3);
			arquivo.Inserir(linha);

			Assert.AreEqual("\r\n\r\n" + linha.Conteudo, arquivo.Conteudo);
		}

		[TestMethod]
		public void Quando_Cria_Um_Arquivo_E_Adiciona_Linha3_Linha1_Seu_Conteudo_Deve_Ser_de4Linhas()
		{
			var arquivo = new Arquivo();
			arquivo.Inserir(new Linha("Fernandes", 3));
			arquivo.Inserir(new Linha("Bruno", 1));

			Assert.AreEqual("Bruno\r\n\r\n\r\nFernandes", arquivo.Conteudo);
		}

		[TestMethod]
		public void Quando_Cria_Um_Arquivo_E_Adiciona_Linha3_Linha1_ERemoveLinha2_Seu_Conteudo_Deve_Ser_de3Linhas()
		{
			var arquivo = new Arquivo();
			arquivo.Atualizar(new Linha("Fernandes", 3));
			arquivo.Atualizar(new Linha("Bruno", 1));
			arquivo.Remover(new Linha(String.Empty, 2));

			Assert.AreEqual("Bruno\r\nFernandes", arquivo.Conteudo);
		}

		[TestMethod]
		public void Quando_Cria_Um_Arquivo_Com_ConteudoNo_Construtor_O_Conteudo_Deve_Ser_deIgual_Ao_Informado()
		{
			var conteudoOriginal = "Bruno\r\n\r\n\r\nFernandes";
			var arquivo = new Arquivo(conteudoOriginal);

			Assert.AreEqual(conteudoOriginal, arquivo.Conteudo);
		}

	}
}