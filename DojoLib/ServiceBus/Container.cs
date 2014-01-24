namespace ServiceBus
{
	using System;

	public class Container : ContainerIoC
	{
		private static readonly Object _lock = new Object();
		private static IContainerIoC instancia;
		public static IContainerIoC IoC
		{
			get { return (instancia ?? (IoC = new Container())); }
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