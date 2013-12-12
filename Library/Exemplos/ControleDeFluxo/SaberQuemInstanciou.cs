namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	using System;

	public class SaberQuemInstanciou : IExecutavel
	{
		public void Executar()
		{
			A a = new A();
			B b = new B();

			C c1 = new C(null);
			C c2 = a.InstanciarERetornoarC();
			C c3 = b.InstanciarERetornoarC();
			C c4 = new C(new SaberQuemInstanciou());

			Console.WriteLine(c1.ToString());
			Console.WriteLine(c2.ToString());
			Console.WriteLine(c3.ToString());
			Console.WriteLine(c4.ToString());
		}
	}

	public class A
	{
		public A()
		{
			//...
		}
		public C InstanciarERetornoarC()
		{
			C c = new C(this);
			return c;
		}
	}

	public class B
	{
		public B()
		{
			//...
		}
		public C InstanciarERetornoarC()
		{
			C c = new C(this);
			return c;
		}
	}

	public class C
	{
		private Object owner;

		public C(Object pOwner)
		{
			this.owner = pOwner;
		}

		public override String ToString()
		{
			String vRetorno = String.Empty;

			if (owner == null)
				vRetorno = "Quem Instanciou C foi um Objeto que Passou um Parâmetro Nulo";
			else if (owner is A)
				vRetorno = "Quem Instanciou C foi um Objeto da Classe A";
			else if (owner is B)
				vRetorno = "Quem Instanciou C foi um Objeto da Classe B";
			else
				vRetorno = "Quem Instanciou C foi um Objeto da Classe " + owner.GetType().Name;

			return vRetorno;
		}
	}
}