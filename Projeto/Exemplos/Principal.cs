using MPSC.Library.Exemplos.BancoDeDados;
using MPSC.Library.Exemplos.ControleDeFluxo;
using MPSC.Library.Exemplos.ControleDeFluxo.Reflection;
using MPSC.Library.Exemplos.Delegates;
using MPSC.Library.Exemplos.DesignPattern.Strategy.Classes;
using MPSC.Library.Exemplos.Medidas;
using MPSC.Library.Exemplos.QuestoesDojo;
using MPSC.Library.Exemplos.Service;
using MPSC.Library.Exemplos.Transformacao;
using MPSC.Library.Exemplos.Utilidades;
using MPSC.Library.Exemplos.Utilidades.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

/*
1) Site tipo OLX
2) "Cozinhe com o que tem"
3) Checkup de Autos
4) "PetBook" (facebook de animais)
5) Pesquisa de satisfação
6) Implementação de um Jogo (dos pontos)
*/

namespace MPSC.Library.Exemplos
{
	public static class Principal
	{
		public const Char ESC = (char)27;
		public static void Main(String[] args)
		{
			var mainMenu = ObterMenuPrincipal();
			var itemMenu = mainMenu.Exibir();
			while ((itemMenu != null) && itemMenu.PossuiComando)
			{
				itemMenu.Executar();
				itemMenu = mainMenu.Exibir();
			}
		}

		private static Menu ObterMenuPrincipal()
		{
			return new Menu('0', "Principal",
				new Menu('1', "Banco de Dados",
					new Menu('1', "Sql Server",
						new Menu('1', "Como Pegar As Mensagens De Prints Do Sql Server", new ComoPegarAsMensagensDePrintsDoSqlServer())
					),
					new Menu('2', "Varias Coisas Com Objetos De Banco De Dados", new VariasCoisasComObjetosDeBancoDeDados())
				),

				new Menu('2', "Controle de Fluxo",
					new Menu('0', "Processando Ordenação Com Delegate", new ProcessandoOrdenacaoComDelegate()),
					new Menu('1', "Maquina de Estado", new ValidarTransicoesDeEstado()),
					new Menu('2', "Saber Quem Instanciou", new SaberQuemInstanciou()),
					new Menu('3', "Teste De Construtores Estaticos", new TesteDeConstrutoresEstaticos()),
					new Menu('4', "Diretiva De Pré-Processamento Via Reflection", new DiretivaDePreProcessamentoViaReflection()),
					new Menu('5', "Definindo Quem Pode Alterar Os Atributos Do Objeto", new DefinindoQuemPodeAlterarOsAtributosDoObjeto()),
					new Menu('6', "Definindo Quem Pode Alterar Os Atributos Do Objeto Em Tempo Design 1", new DefinindoQuemPodeAlterarOsAtributosDoObjetoEmTempoDesign1()),
					new Menu('7', "Definindo Quem Pode Alterar Os Atributos Do Objeto Em Tempo Design 2", new DefinindoQuemPodeAlterarOsAtributosDoObjetoEmTempoDesign2()),
					new Menu('8', "Controlando Se O Programa Esta Sendo Executado", new ControlandoSeOProgramaEstaSendoExecutado()),
					new Menu('9', "Controlando Recursos Compartilhados", new ControlandoRecursosCompartilhados())
				),

				new Menu('3', "Reflection",
					new Menu('1', "Atributos Descritivos Dos Enumerados", new AtributosDescritivosDosEnumerados())
				),

				new Menu('4', "Medidas",
					new Menu('1', "Duração de um Intervalo de Tempo de Alta Precisão", new DuracaoIntervaloTempoAltaPrecisao()),
					new Menu('2', "Performance de Geração de Ids via Sequence vs Tabela", new TestandoPerformanceDeGeracaoDeIdsViaSequenceVsTabela())
				),

				new Menu('5', "Utilidades",
					new Menu('1', "Separar Lista EMails E Remover Duplicados", new SeparaListaEMailsERemoveDuplicados()),
					new Menu('2', "Criptografia Com Operador XOR", new CriptografiaComOperadorXOR()),
					new Menu('3', "Alguma Coisa Com Relatorios usando Linq", new AlgumaCoisaComRelatoriosUsandoLinq()),
					new Menu('4', "Transformacao De Dados Para DTO", new TransformacaoDeDadosParaDTO()),
					new Menu('5', "Wake Up On LAN", new ProgramWakeOnLan()),
					new Menu('6', "Árvore Utópica", new ArvoreUtopica())
				),

				new Menu('6', "Design Pattern",
					new Menu('1', "Strategy - Mini Simulador De Patos", new MiniSimuladorDePatos()),
					new Menu('2', "Observer - Estação de Monitoramento")
				),

				new Menu('7', "Métodos de extensão",
					new Menu('1', "Extrair XML", new ExtensionMethods())
				),

				new Menu('8', "Questões e Treinamentos de DOJO (On-Line)",
					new Menu('1', "Gerador de Matriz Espiral", new MatrizEspiralRunner()),
					new Menu('2', "Numero Romano", new NumeroRomanoRunner()),
					new Menu('3', "Autor De Obra Bibliográfica", new AutorDeObraBibliograficaRunner()),
					new Menu('4', "Livraria Do Harry Potter", new LivrariaHarryPotterRunner()),
					new Menu('5', "Palavras Primas", new PalavrasPrimasRunner()),
					new Menu('6', "TestaPivot", new TestaPivot())
				),

				new Menu(ESC, "Sair")
			);
		}
	}
	
	public interface IExecutavel
	{
		void Executar();
	}

	public class Menu
	{
		private readonly Char Codigo;
		private readonly String Descricao;
		private readonly Menus SubMenus;
		private readonly IExecutavel Comando;
		public Menu Parent { get; internal set; }
		public Boolean PossuiComando { get { return Comando != null; } }
		public Boolean PossuiSubMenus { get { return SubMenus.Count > 0; } }

		#region //Contrutores
		public Menu(Char codigo, String descricao) { Codigo = codigo; Descricao = descricao; SubMenus = new Menus(this); }

		public Menu(Char codigo, String descricao, IExecutavel comando) : this(codigo, descricao) { Comando = comando; }

		public Menu(Char codigo, String descricao, params Menu[] menus) : this(codigo, descricao) { SubMenus.Add(menus); }

		#endregion //Contrutores

		internal Boolean CodigoEhIgual(Char codigo)
		{
			return Codigo == codigo;
		}

		internal void ExibirItem()
		{
			Console.WriteLine(Codigo + " - " + Descricao);
		}

		internal Menu Exibir()
		{
			return SubMenus.Exibir();
		}

		public void Executar()
		{
			Comando.Executar();
			AguardarEnterOuEsc("\r\nPressione {Enter} Ou {Esc} para voltar ao menu");
		}

		private void AguardarEnterOuEsc(String mensagem)
		{
			Console.WriteLine(mensagem);
			var key = Console.ReadKey();
			while ((key.Key != ConsoleKey.Escape) && (key.Key != ConsoleKey.Enter))
			{
				LimparUltimoCaracter();
				key = Console.ReadKey();
			}
		}

		private void LimparUltimoCaracter()
		{
			Console.CursorLeft--;
			Console.Write(" ");
			Console.CursorLeft--;
		}
	}

	public class Menus : List<Menu>
	{
		private readonly Menu Parent;

		public Menus(Menu parent) { Parent = parent; }

		public void Add(IEnumerable<Menu> menus)
		{
			AddRange(menus);
			ForEach(m => m.Parent = Parent);
		}

		private void Exibir(String primeiraOpcao)
		{
			Console.Clear();
			Console.WriteLine(primeiraOpcao);
			foreach (var item in this)
				item.ExibirItem();
		}

		public Menu Exibir()
		{
			Exibir("Selecione\r\n");
			var itemMenu = EsperarUsuarioEscolherOpcaoDoMenu();
			while (itemMenu == null)
			{
				Exibir("Opção inválida\r\nSelecione\r\n");
				itemMenu = EsperarUsuarioEscolherOpcaoDoMenu();
			}

			return itemMenu;
		}

		private Menu EsperarUsuarioEscolherOpcaoDoMenu()
		{
			var codigo = Console.ReadKey().KeyChar;

			var itemMenu = this.FirstOrDefault(m => m.CodigoEhIgual(codigo));

			while ((itemMenu != null) && itemMenu.PossuiSubMenus)
				itemMenu = itemMenu.Exibir();

			return (itemMenu == null) && (codigo == Principal.ESC) ? Parent.Parent : itemMenu;
		}
	}
}