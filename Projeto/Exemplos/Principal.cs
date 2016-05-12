namespace MPSC.Library.Exemplos
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using MPSC.Library.Exemplos.BancoDeDados;
	using MPSC.Library.Exemplos.ControleDeFluxo;
	using MPSC.Library.Exemplos.ControleDeFluxo.Reflection;
	using MPSC.Library.Exemplos.DesignPattern.Strategy.Classes;
	using MPSC.Library.Exemplos.Medidas;
	using MPSC.Library.Exemplos.QuestoesDojo;
	using MPSC.Library.Exemplos.Service;
	using MPSC.Library.Exemplos.Transformacao;
	using MPSC.Library.Exemplos.Utilidades;

	public interface IExecutavel
	{
		void Executar();
	}

	public static class Principal
	{
		public static void Main(String[] args)
		{
			ListaMenu mainMenu = MenuRepository();
			ItemMenu itemMenu;
			while (((itemMenu = mainMenu.Mostrar()) != null) && (itemMenu.Comando != null))
			{
				itemMenu.Comando.Executar();
				var key = Console.ReadKey();
				while ((key.Key != ConsoleKey.Escape) && (key.Key != ConsoleKey.Enter))
					key = Console.ReadKey();
			}
		}


		private static ListaMenu MenuRepository()
		{
			const char esc = (char)27;
			return new ListaMenu(
				new ItemMenu('1', "Banco de Dados",
					new ItemMenu('1', "Sql Server",
						new ItemMenu('1', "Como Pegar As Mensagens De Prints Do Sql Server", new ComoPegarAsMensagensDePrintsDoSqlServer())
					),
					new ItemMenu('2', "Varias Coisas Com Objetos De Banco De Dados", new VariasCoisasComObjetosDeBancoDeDados())
				),

				new ItemMenu('2', "Controle de Fluxo",
					new ItemMenu('1', "Maquina de Estado", new ValidarTransicoesDeEstado()),
					new ItemMenu('2', "Saber Quem Instanciou", new SaberQuemInstanciou()),
					new ItemMenu('3', "Teste De Construtores Estaticos", new TesteDeConstrutoresEstaticos()),
					new ItemMenu('4', "Diretiva De Pré-Processamento Via Reflection", new DiretivaDePreProcessamentoViaReflection()),
					new ItemMenu('5', "Definindo Quem Pode Alterar Os Atributos Do Objeto", new DefinindoQuemPodeAlterarOsAtributosDoObjeto()),
					new ItemMenu('6', "Definindo Quem Pode Alterar Os Atributos Do Objeto Em Tempo Design 1", new DefinindoQuemPodeAlterarOsAtributosDoObjetoEmTempoDesign1()),
					new ItemMenu('7', "Definindo Quem Pode Alterar Os Atributos Do Objeto Em Tempo Design 2", new DefinindoQuemPodeAlterarOsAtributosDoObjetoEmTempoDesign2()),
					new ItemMenu('8', "Controlando Se O Programa Esta Sendo Executado", new ControlandoSeOProgramaEstaSendoExecutado()),
					new ItemMenu('9', "Controlando Recursos Compartilhados", new ControlandoRecursosCompartilhados())
				),

				new ItemMenu('3', "Reflection",
					new ItemMenu('1', "Atributos Descritivos Dos Enumerados", new AtributosDescritivosDosEnumerados())
				),

				new ItemMenu('4', "Medidas",
					new ItemMenu('1', "Duração de um Intervalo de Tempo de Alta Precisão", new DuracaoIntervaloTempoAltaPrecisao()),
					new ItemMenu('2', "Performance de Geração de Ids via Sequence vs Tabela", new TestandoPerformanceDeGeracaoDeIdsViaSequenceVsTabela())
				),

				new ItemMenu('5', "Utilidades",
					new ItemMenu('1', "Separar Lista EMails E Remover Duplicados", new SeparaListaEMailsERemoveDuplicados()),
					new ItemMenu('2', "Criptografia Com Operador XOR", new CriptografiaComOperadorXOR()),
					new ItemMenu('3', "Alguma Coisa Com Relatorios usando Linq", new AlgumaCoisaComRelatoriosUsandoLinq()),
					new ItemMenu('4', "Transformacao De Dados Para DTO", new TransformacaoDeDadosParaDTO()),
					new ItemMenu('5', "Wake Up On LAN", new ProgramWakeOnLan())
				),

				new ItemMenu('6', "Design Pattern",
					new ItemMenu('1', "Strategy - Mini Simulador De Patos", new MiniSimuladorDePatos()),
					new ItemMenu('2', "Observer - Estação de Monitoramento")
				),

				new ItemMenu('7', "Métodos de extensão",
					new ItemMenu('1', "Extrair XML", new ExtensionMethods())
				),

				new ItemMenu('8', "Questões e Treinamentos de DOJO (On-Line)",
					new ItemMenu('1', "Gerador de Matriz Espiral", new MatrizEspiralRunner()),
					new ItemMenu('2', "Numero Romano", new NumeroRomanoRunner()),
					new ItemMenu('3', "Autor De Obra Bibliográfica", new AutorDeObraBibliograficaRunner()),
					new ItemMenu('4', "Livraria Do Harry Potter", new LivrariaHarryPotterRunner()),
					new ItemMenu('5', "Palavras Primas", new PalavrasPrimasRunner()),
					new ItemMenu('6', "TestaPivot", new TestaPivot())
				),

				new ItemMenu(esc, "Sair")
			);
		}
	}

	public class ItemMenu
	{
		public Char Codigo { get; set; }
		public String Descricao { get; set; }
		public ListaMenu Menus = new ListaMenu();
		public IExecutavel Comando { get; set; }

		#region //Contrutores
		public ItemMenu(Char codigo, String descricao)
		{
			Codigo = codigo;
			Descricao = descricao;
		}

		public ItemMenu(Char codigo, String descricao, IExecutavel comando)
			: this(codigo, descricao)
		{
			Comando = comando;
		}

		public ItemMenu(Char codigo, String descricao, params ItemMenu[] menus)
			: this(codigo, descricao)
		{
			foreach (var item in menus)
				Menus.Add(item);
		}
		#endregion //Contrutores

		public void Mostrar()
		{
			Console.WriteLine(Codigo + " - " + Descricao);
		}
	}

	public class ListaMenu : List<ItemMenu>
	{
		public ListaMenu(params ItemMenu[] menus)
		{
			foreach (var item in menus)
				this.Add(item);
		}

		public ItemMenu Mostrar()
		{
			Console.Clear();
			foreach (ItemMenu item in this)
				item.Mostrar();

			ItemMenu itemMenu = EsperarUsuario();

			Console.Clear();
			return itemMenu;
		}

		private ItemMenu EsperarUsuario()
		{
			ItemMenu itemMenu = Avaliar();
			while (itemMenu == null)
			{
				Console.Clear();
				Console.WriteLine("Opção inválida");
				foreach (ItemMenu item in this)
					item.Mostrar();
				itemMenu = Avaliar();
			}

			return itemMenu;
		}

		private ItemMenu Avaliar()
		{
			var codigo = Console.ReadKey().KeyChar;

			ItemMenu itemMenu = this.FirstOrDefault(m => m.Codigo == codigo);

			while ((itemMenu != null) && (itemMenu.Menus != null) && (itemMenu.Menus.Count > 0))
				itemMenu = itemMenu.Menus.Mostrar();

			return itemMenu;
		}
	}
}