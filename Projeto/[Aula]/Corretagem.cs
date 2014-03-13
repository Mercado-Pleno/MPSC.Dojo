using System;
using System.Collections.Generic;
using System.Text;

namespace MPSC.Library.Aula.Curso
{
    public class Corretagem
    {
    }

    public class Operacao
    {
        public Acao Acao { get; set; }
    }

    public class OperacaoCompra : Operacao
    {
    }

    public class OperacaoVenda : Operacao
    {
    }

    public class Carteira
    {
        private Acionista acionista { get; set; }
        private IList<Operacao> operacoes { get; set; }

        public void AdicionarOperacao (Operacao operacao)
        {
            // operacao.Acao.Cotacao.valor;
            operacoes.Add(operacao);
        }

    }

    public class Ordem
    {
        public Acionista Acionista { get; set; }
        public List<Acao> ListaAcao { get; set; }
        public void Efetivar() { }
        public void Abortar() { }
    }

    public class OrdemCompra : Ordem
    {
    }

    public class OrdemVenda : Ordem
    {
    }

    public class Cotacao
    {
        public Decimal Valor;
    }

    public class Pessoa
    {
        public List<Acao> ListaAcao { get; set; }

        public void VenderAcao(Acao titulo)
        {
            if (titulo.Pessoa == this)
                ListaAcao.Remove(titulo);
        }

        public void ComprarAcao(Acao titulo)
        {
            if (titulo.Pessoa != this)
                ListaAcao.Add(titulo);
        }

        public bool Vendeu(Acao titulo)
        {
            return !ListaAcao.Contains(titulo);
        }

        public bool Comprou(Acao titulo)
        {
            return ListaAcao.Contains(titulo);
        }
    }

    public class Acionista : Pessoa
    {
    }

    public class Corretora
    {
        public List<Empresa> ListaEmpresa { get; set; }
        public List<Acionista> ListaAcionista { get; set; }
        public void ProcessarOrdemCompra(OrdemCompra ordemCompra)
        {
        }

        public void ProcessarOrdemVenda()
        {
        }

        public void CalcularCorretagem()
        {

        }
        public void CalcularEmolumentos()
        {

        }
    }

    public class BolsaValor
    {
        public void RegistrarCompraVenda(Pessoa vendedor, Pessoa comprador, List<Acao> acoes)
        {
            foreach (Acao acao in acoes)
            {
                vendedor.VenderAcao(acao);
                comprador.ComprarAcao(acao);
                acao.Transferir(vendedor, comprador);
            }
        }
    }

    public class Empresa : Pessoa
    {
        public List<Acao> ListaTitulo { get; set; }
    }

    public class Acao
    {
        public Cotacao Cotacao { get; set; }

        public Pessoa Pessoa { get; set; }

        public void Transferir(Pessoa vendedor, Pessoa comprador)
        {
            if ((Pessoa == vendedor) && vendedor.Vendeu(this) && comprador.Comprou(this))
            {
                Pessoa = comprador;
            }
        }
    }
}