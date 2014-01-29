using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VendasModel;

namespace VendasView
{
    public partial class CadCliente : Form
    {
        public CadCliente()
            : this(null)
        {
        }

        public CadCliente(Cliente cliente)
        {
            InitializeComponent();
            ConfigurarCliente(cliente);
        }

        private void ConfigurarCliente(Cliente cliente)
        {
            if (cliente != null)
            {
                txtNome.Text = cliente.Nome;
                dtpNascimento.Value = cliente.Nascimento;
                ckbAtivo.Checked = cliente.Ativo;
                txtCodigo.Text = cliente.Codigo.ToString();
                txtIdade.Text = cliente.Idade.ToString();
            }
        }


        public Cliente ObterCliente()
        {
            Cliente cliente = new Cliente();
            cliente.Codigo = txtCodigo.Text == string.Empty ? 0 : Convert.ToInt32(txtCodigo.Text);
            cliente.Nome = txtNome.Text;
            cliente.Nascimento = dtpNascimento.Value;
            cliente.Ativo = ckbAtivo.Checked;

            return cliente;
        }
    }
}
