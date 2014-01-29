using System;
using System.Collections.Generic;
using VendasModel;
using System.Linq;

namespace VendasController
{
    public class ClienteController
    {
        public IList<Cliente> ObterTodosClientes()
        {
            return ClienteRepository.Instancia.ListaCliente.Where(cliente => cliente.Ativo).OrderBy(cliente => cliente.Nome).ToList() ;
        }

        public IList<Cliente> ObterClientePeloNome(String nomeCliente)
        {
            return ClienteRepository.Instancia.ListaCliente
                .Where(cliente => cliente.Nome.Contains(nomeCliente))
                .OrderBy(cliente => cliente.Nome).ToList();
        }

        public Cliente ObterClientePeloCodigo(int codigoCliente)
        {
            return ClienteRepository.Instancia.ListaCliente.FirstOrDefault(cliente => cliente.Codigo == codigoCliente);
        }

        private Boolean RemoverCliente(Cliente cliente)
        {
            Boolean existe = ClienteRepository.Instancia.ListaCliente.Contains(cliente);
            if (existe)
                ClienteRepository.Instancia.ListaCliente.Remove(cliente);
            return existe;
        }

        public Boolean RemoverCliente(int codigoCliente)
        {
            Cliente cliente = ObterClientePeloCodigo(codigoCliente);
            return RemoverCliente(cliente);
        }


        public void IncluirCliente(Cliente cliente)
        {
            cliente.Codigo = ClienteRepository.Instancia.ObterNovoCodigo();
            cliente.Ativo = true;
            ClienteRepository.Instancia.ListaCliente.Add(cliente);
            
        }

        public void AlterarCliente(Cliente cliente)
        {
            RemoverCliente(cliente.Codigo);
            ClienteRepository.Instancia.ListaCliente.Add(cliente);
        }
    }
}
