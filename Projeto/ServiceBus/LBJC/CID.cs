namespace LBJC
{
	using System;

	/// <summary>
	/// Container de Inversão de Dependência
	/// </summary>
	public class CID : IoC
	{
		private static readonly Object _lock = new Object();
		private static IIoC instancia;
		public static IIoC IoC
		{
			get { return (instancia ?? (IoC = new CID())); }
			set
			{
				if (PodeAlterar(instancia, value))
				{
					lock (_lock)
					{
						if (PodeAlterar(instancia, value))
							instancia = value;
					}
				}
			}
		}

		private static Boolean PodeAlterar(Object obj1, Object obj2)
		{
			return (obj1 == null) || ((obj2 != null) && (obj1 != obj2));
		}
	}
}