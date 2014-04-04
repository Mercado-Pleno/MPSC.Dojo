namespace LBJC
{
	using System;

	public interface IIoC
	{
		IIoC Map<Interface, Classe>(params Object[] parametros);
		Type Get<Interface>();
		Interface New<Interface>(params Object[] parametros);
		Interface Singleton<Interface>(String sessionKey, params Object[] parametros);
	}
}