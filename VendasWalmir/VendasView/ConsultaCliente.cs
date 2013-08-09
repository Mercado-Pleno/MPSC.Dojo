using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VendasController;
using VendasModel;

namespace VendasView
{

    public partial class ConsultaCliente : Form
    {
        private enum Operacao
        {
            Incluir,
            Alterar,
            Excluir
        }

        private ClienteController controller;
        public ConsultaCliente()
        {
            InitializeComponent();
            controller = new ClienteController();
        }

        private void RefreshGrid()
        {
            dgvCliente.DataSource = null;
            dgvCliente.DataSource = controller.ObterTodosClientes();
        }

        private void btListar_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btIncluir_Click(object sender, EventArgs e)
        {
            AbrirCadastrodeClientes(null, Operacao.Incluir);
        }


        private void btAlterar_Click(object sender, EventArgs e)
        {
            Cliente cliente = dgvCliente.CurrentRow.DataBoundItem as Cliente;
            AbrirCadastrodeClientes(cliente, Operacao.Alterar);
        }

        private void btRemover_Click(object sender, EventArgs e)
        {
            Cliente cliente = dgvCliente.CurrentRow.DataBoundItem as Cliente;
            AbrirCadastrodeClientes(cliente, Operacao.Excluir);

        }

        private void AbrirCadastrodeClientes(Cliente cliente, Operacao operacao)
        {

            CadCliente cadCliente = new CadCliente(cliente);
            if (cadCliente.ShowDialog() == DialogResult.OK)
            {
                switch (operacao)
                {
                    case Operacao.Incluir:
                        controller.IncluirCliente(cadCliente.ObterCliente());
                        break;
                    case Operacao.Alterar:
                        controller.AlterarCliente(cadCliente.ObterCliente());
                        break;
                    case Operacao.Excluir:
                        controller.RemoverCliente(cliente.Codigo);
                        break;
                    default:
                        break;
                }
                RefreshGrid();
            }
            cadCliente.Close();
        }



        private void btPesquisar_Click(object sender, EventArgs e)
        {
            dgvCliente.DataSource = null;
            dgvCliente.DataSource = controller.ObterClientePeloNome(txtPesquisa.Text);

        }
    }
}
