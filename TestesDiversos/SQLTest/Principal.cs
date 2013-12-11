namespace MPSC.Lib
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using MPSC.Lib.BancoDados;

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
				new ItemMenu('1', "Banco de Dados"
					, new ItemMenu('1', "Sql Server"
						, new ItemMenu('1', "Como Pegar As Mensagens De Prints Do Sql Server", new ComoPegarAsMensagensDePrintsDoSqlServer())
					)
				)
				, new ItemMenu('2', "Sair")
			);
		}
	}


	public interface IExecutavel
	{
		ItemMenu Executar();
	}

	public class ItemMenu : IExecutavel
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
			Comando = this;

			foreach (var item in menus)
				Menus.Add(item);
		}
		#endregion //Contrutores

		public void Mostrar()
		{
			Console.WriteLine(Codigo + " - " + Descricao);
		}

		public ItemMenu Executar()
		{
			return Menus.Mostrar();
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

			while ((itemMenu != null) && (itemMenu is ItemMenu) && (itemMenu.Comando != null) && (itemMenu.Comando is ItemMenu))
				itemMenu = itemMenu.Comando.Executar();

			return itemMenu;
		}
	}
}