namespace ServiceBus
{
	using System;

	public interface IContainerIoC
	{
		ContainerIoC Map<Interface, Classe>(params Object[] parametros)
			where Interface : class
			where Classe : class;
		Type Get<Interface>() where Interface : class;
		Interface New<Interface>(params Object[] parametros) where Interface : class;
	}
}