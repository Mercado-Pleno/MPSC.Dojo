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
			var menuRepository = new MenuRepository();
			ItemMenu itemMenu;
			while (((itemMenu = menuRepository.Mostrar()) != null) && (itemMenu.Comando != null))
			{
				itemMenu.Comando.Executar();
				Console.ReadLine();
			}
		}
	}

	public class MenuRepository
	{
		private ListaMenu mainMenu = new ListaMenu();

		public MenuRepository()
		{
			var imBanco = mainMenu.Adicionar('1', "Banco de Dados", null);
			var imSql = imBanco.Adicionar('1', "Sql Server", null);
			var itemMenu = imSql.Adicionar('1', "Como Pegar As Mensagens De Prints Do Sql Server", new ComoPegarAsMensagensDePrintsDoSqlServer());

			var imSair = mainMenu.Adicionar('2', "Sair", null);
		}

		public ItemMenu Mostrar()
		{
			return mainMenu.Mostrar();
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

		public void Mostrar()
		{
			Console.WriteLine(Codigo + " - " + Descricao);
		}

		public ItemMenu Executar()
		{
			return Menus.Mostrar();
		}

		public ItemMenu Adicionar(Char codigo, String descricao, IExecutavel comando)
		{
			this.Comando = this;
			return Menus.Adicionar(codigo, descricao, comando);
		}

	}

	public class ListaMenu : List<ItemMenu>
	{
		public ItemMenu Adicionar(Char codigo, String descricao, IExecutavel comando)
		{
			var menu = new ItemMenu() { Codigo = codigo, Descricao = descricao, Comando = comando };
			this.Add(menu);
			return menu;
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