namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability
{
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Controller;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain;

	public class SingleResponsabilityCRUD : IExecutavel
	{
		public void Executar()
		{
			Cliente cliente = new Cliente() { Id = 1, Nome = "Walmir" };

			var clienteController = new ClienteController();
			clienteController.Incluir(cliente);
			cliente.Nome = "Fernandes";
			clienteController.Alterar(cliente);
		}
	}
}