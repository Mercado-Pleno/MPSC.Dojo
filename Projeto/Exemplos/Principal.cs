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
	public interface IExecutavel
	{
		void Executar();
	}

	public static class Principal
	{
		public const Char ESC = (char)27;
		public static void Main(String[] args)
		{
			var mainMenu = ObterMenuPrincipal();
			var itemMenu = mainMenu.MostrarSub();
			while ((itemMenu != null) && itemMenu.PossuiComando)
			{
				itemMenu.Executar();
				itemMenu = mainMenu.MostrarSub();
			}
		}


		private static ItemMenu ObterMenuPrincipal()
		{
			return new ItemMenu('0', "",
				new ItemMenu('1', "Banco de Dados",
					new ItemMenu('1', "Sql Server",
						new ItemMenu('1', "Como Pegar As Mensagens De Prints Do Sql Server", new ComoPegarAsMensagensDePrintsDoSqlServer())
					),
					new ItemMenu('2', "Varias Coisas Com Objetos De Banco De Dados", new VariasCoisasComObjetosDeBancoDeDados())
				),

				new ItemMenu('2', "Controle de Fluxo",
					new ItemMenu('0', "Processando Ordenação Com Delegate", new ProcessandoOrdenacaoComDelegate()),
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
					new ItemMenu('5', "Wake Up On LAN", new ProgramWakeOnLan()),
					new ItemMenu('6', "Árvore Utópica", new ArvoreUtopica())
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

				new ItemMenu(ESC, "Sair")
			);
		}
	}

	public class ItemMenu
	{
		private readonly Char Codigo;
		private readonly String Descricao;
		private readonly ListaMenu SubMenus;
		private readonly IExecutavel Comando;
		public ItemMenu Parent { get; internal set; }
		public Boolean PossuiComando { get { return Comando != null; } }
		public Boolean PossuiSubMenus { get { return SubMenus.Count > 0; } }

		#region //Contrutores
		public ItemMenu(Char codigo, String descricao) { Codigo = codigo; Descricao = descricao; SubMenus = new ListaMenu(this); }

		public ItemMenu(Char codigo, String descricao, IExecutavel comando) : this(codigo, descricao) { Comando = comando; }

		public ItemMenu(Char codigo, String descricao, params ItemMenu[] menus) : this(codigo, descricao) { SubMenus.AddRange(menus); SubMenus.ForEach(m => m.Parent = this); }

		#endregion //Contrutores

		internal Boolean CodigoEhIgual(Char codigo)
		{
			return Codigo == codigo;
		}

		public void Mostrar()
		{
			Console.WriteLine(Codigo + " - " + Descricao);
		}

		internal ItemMenu MostrarSub()
		{
			return SubMenus.Mostrar();
		}

		public void Executar()
		{
			Comando.Executar();
			var key = Console.ReadKey();
			while ((key.Key != ConsoleKey.Escape) && (key.Key != ConsoleKey.Enter))
				key = Console.ReadKey();
		}
	}

	public class ListaMenu : List<ItemMenu>
	{
		public readonly ItemMenu Parent;
		public ListaMenu(ItemMenu parent, params ItemMenu[] menus) : base(menus) { Parent = parent; ForEach(m => m.Parent = parent); }

		private void InternalShow(String primeiraOpcao)
		{
			Console.Clear();
			Console.WriteLine(primeiraOpcao);
			foreach (var item in this)
				item.Mostrar();
		}

		public ItemMenu Mostrar()
		{
			InternalShow("Selecione\r\n");

			return EsperarUsuario();
		}

		private ItemMenu EsperarUsuario()
		{
			var itemMenu = Avaliar();
			while (itemMenu == null)
			{
				InternalShow("Opção inválida\r\nSelecione\r\n");
				itemMenu = Avaliar();
			}

			return itemMenu;
		}

		private ItemMenu Avaliar()
		{
			var codigo = Console.ReadKey().KeyChar;

			var itemMenu = this.FirstOrDefault(m => m.CodigoEhIgual(codigo));

			while ((itemMenu != null) && itemMenu.PossuiSubMenus)
				itemMenu = itemMenu.MostrarSub();

			return (itemMenu == null) && (codigo == Principal.ESC) ? Parent.Parent : itemMenu;
		}
	}
}