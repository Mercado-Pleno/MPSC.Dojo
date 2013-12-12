namespace MPSC.Library.Exemplos
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using MPSC.Library.Exemplos.BancoDeDados;
	using MPSC.Library.Exemplos.ControleDeFluxo;
	using MPSC.Library.Exemplos.ControleDeFluxo.Reflection;
	using MPSC.Library.Exemplos.Medidas;
	using MPSC.Library.Exemplos.Utilidades;

	public static class Principal
	{
		public static void Main(String[] args)
		{
			ListaMenu mainMenu = MenuRepository();
			ItemMenu itemMenu;
			while (((itemMenu = mainMenu.Mostrar()) != null) && (itemMenu.Comando != null))
			{
				itemMenu.Comando.Executar();
				Console.ReadLine();
			}
		}


		private static ListaMenu MenuRepository()
		{
			return new ListaMenu(
				new ItemMenu('1', "Banco de Dados",
					new ItemMenu('1', "Sql Server",
						new ItemMenu('1', "Como Pegar As Mensagens De Prints Do Sql Server", new ComoPegarAsMensagensDePrintsDoSqlServer())
					)
				),

				new ItemMenu('2', "Controle de Fluxo",
					new ItemMenu('1', "Maquina de Estado", new ValidarTransicoesDeEstado()),
					new ItemMenu('2', "Saber Quem Instanciou", new SaberQuemInstanciou())
				),

				new ItemMenu('3', "Reflection",
					new ItemMenu('1', "Atributos Descritivos Dos Enumerados", new AtributosDescritivosDosEnumerados())
				),

				new ItemMenu('4', "Medidas",
					new ItemMenu('1', "Duracção de um Intervalo de Tempo de Alta Precisão", new DuracaoIntervaloTempoAltaPrecisao())
				),

				new ItemMenu('5', "Utilidades",
					new ItemMenu('1', "Separar Lista EMails E Remover Duplicados", new SeparaListaEMailsERemoveDuplicados()),
					new ItemMenu('2', "Criptografia Com Operador XOR", new CriptografiaComOperadorXOR()),
					new ItemMenu('3', "Alguma Coisa Com Relatorios usando Linq", new AlgumaCoisaComRelatoriosUsandoLinq())
				),

				new ItemMenu('←', "Sair")
			);
		}
	}


	public interface IExecutavel
	{
		void Executar();
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