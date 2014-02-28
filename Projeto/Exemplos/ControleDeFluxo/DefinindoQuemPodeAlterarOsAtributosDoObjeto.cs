using System;
using System.Collections.Generic;
using System.Text;

namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	public class DefinindoQuemPodeAlterarOsAtributosDoObjeto : IExecutavel
	{
		public void Executar()
		{
			var contexto1 = new Owner();
			var contexto2 = new Owner();
			var contexto3 = new Owner();
			contexto1.outros.Add(contexto2);

			var employee = new Employee(
				contexto1,
				name: "John Doe",
				salary: 1500
			);

			try
			{
				employee.RaiseSalary(contexto1, 200);
				employee.RaiseSalary(contexto2, 200);
				employee.RaiseSalary(contexto3, 200);
			}
			catch (Exception)
			{
				// throws
			}

			Console.WriteLine("{0} - {1}", employee.Name, employee.Salary);
		}
	}

	public class Owner : IOwner
	{
		public IList<IOwner> outros = new List<IOwner>();
		public String GetUniqueId() { return GetHashCode().ToString(); }
		public IEnumerable<IOwner> Socios { get { return outros; } }
	}

	public interface IOwner
	{
		String GetUniqueId();
		IEnumerable<IOwner> Socios { get; }
	}

	public class Base
	{
		private readonly IOwner _owner;
		public Base(IOwner owner)
		{
			if (owner == null)
				throw new ArgumentNullException();
			_owner = owner;
		}

		public void VerifyPermissions(IOwner who)
		{
			if (who == null)
				throw new ArgumentNullException();

			Boolean vPermitido = _owner.GetUniqueId() == who.GetUniqueId();
			if (!vPermitido)
				foreach (var item in _owner.Socios)
					vPermitido = vPermitido || item.GetUniqueId() == who.GetUniqueId();

			if (!vPermitido)
			{
				Console.WriteLine("Invalid Context {0}. Permission Denied!", who.GetHashCode());
				throw new AccessViolationException();
			}
		}
	}

	public class Employee : Base
	{
		public string Name { get; private set; }
		public decimal Salary { get; private set; }

		public Employee(IOwner owner, string name, decimal salary)
			: base(owner)
		{
			Name = name;
			Salary = salary;
		}

		public void RaiseSalary(IOwner who, decimal amount)
		{
			VerifyPermissions(who);
			Salary += amount;
		}
	}
}
