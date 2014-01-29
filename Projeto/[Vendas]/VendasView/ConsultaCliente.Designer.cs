namespace VendasView
{
    partial class ConsultaCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvCliente = new System.Windows.Forms.DataGridView();
            this.btListar = new System.Windows.Forms.Button();
            this.btIncluir = new System.Windows.Forms.Button();
            this.btAlterar = new System.Windows.Forms.Button();
            this.btRemover = new System.Windows.Forms.Button();
            this.btPesquisar = new System.Windows.Forms.Button();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCliente)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCliente
            // 
            this.dgvCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCliente.Location = new System.Drawing.Point(12, 12);
            this.dgvCliente.MultiSelect = false;
            this.dgvCliente.Name = "dgvCliente";
            this.dgvCliente.ReadOnly = true;
            this.dgvCliente.Size = new System.Drawing.Size(426, 220);
            this.dgvCliente.TabIndex = 0;
            // 
            // btListar
            // 
            this.btListar.Location = new System.Drawing.Point(120, 247);
            this.btListar.Name = "btListar";
            this.btListar.Size = new System.Drawing.Size(75, 23);
            this.btListar.TabIndex = 1;
            this.btListar.Text = "Listar";
            this.btListar.UseVisualStyleBackColor = true;
            this.btListar.Click += new System.EventHandler(this.btListar_Click);
            // 
            // btIncluir
            // 
            this.btIncluir.Location = new System.Drawing.Point(201, 247);
            this.btIncluir.Name = "btIncluir";
            this.btIncluir.Size = new System.Drawing.Size(75, 23);
            this.btIncluir.TabIndex = 2;
            this.btIncluir.Text = "Incluir";
            this.btIncluir.UseVisualStyleBackColor = true;
            this.btIncluir.Click += new System.EventHandler(this.btIncluir_Click);
            // 
            // btAlterar
            // 
            this.btAlterar.Location = new System.Drawing.Point(282, 247);
            this.btAlterar.Name = "btAlterar";
            this.btAlterar.Size = new System.Drawing.Size(75, 23);
            this.btAlterar.TabIndex = 3;
            this.btAlterar.Text = "Alterar";
            this.btAlterar.UseVisualStyleBackColor = true;
            this.btAlterar.Click += new System.EventHandler(this.btAlterar_Click);
            // 
            // btRemover
            // 
            this.btRemover.Location = new System.Drawing.Point(363, 247);
            this.btRemover.Name = "btRemover";
            this.btRemover.Size = new System.Drawing.Size(75, 23);
            this.btRemover.TabIndex = 4;
            this.btRemover.Text = "Remover";
            this.btRemover.UseVisualStyleBackColor = true;
            this.btRemover.Click += new System.EventHandler(this.btRemover_Click);
            // 
            // btPesquisar
            // 
            this.btPesquisar.Location = new System.Drawing.Point(352, 291);
            this.btPesquisar.Name = "btPesquisar";
            this.btPesquisar.Size = new System.Drawing.Size(75, 23);
            this.btPesquisar.TabIndex = 5;
            this.btPesquisar.Text = "Pesquisar";
            this.btPesquisar.UseVisualStyleBackColor = true;
            this.btPesquisar.Click += new System.EventHandler(this.btPesquisar_Click);
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(12, 293);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(324, 20);
            this.txtPesquisa.TabIndex = 6;
            // 
            // ConsultaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 323);
            this.Controls.Add(this.txtPesquisa);
            this.Controls.Add(this.btPesquisar);
            this.Controls.Add(this.btRemover);
            this.Controls.Add(this.btAlterar);
            this.Controls.Add(this.btIncluir);
            this.Controls.Add(this.btListar);
            this.Controls.Add(this.dgvCliente);
            this.Name = "ConsultaCliente";
            this.Text = "Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCliente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCliente;
        private System.Windows.Forms.Button btListar;
        private System.Windows.Forms.Button btIncluir;
        private System.Windows.Forms.Button btAlterar;
        private System.Windows.Forms.Button btRemover;
        private System.Windows.Forms.Button btPesquisar;
        private System.Windows.Forms.TextBox txtPesquisa;
    }
}

