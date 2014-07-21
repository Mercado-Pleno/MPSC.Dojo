namespace LBJC.NavegadorDeDados.View
{
	partial class Autenticacao
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
			this.txtServidor = new System.Windows.Forms.TextBox();
			this.txtUsuario = new System.Windows.Forms.TextBox();
			this.txtSenha = new System.Windows.Forms.TextBox();
			this.cbTipoBanco = new System.Windows.Forms.ComboBox();
			this.cbBancoSchema = new System.Windows.Forms.ComboBox();
			this.blTipoBanco = new System.Windows.Forms.Label();
			this.lbServidor = new System.Windows.Forms.Label();
			this.lbUsuario = new System.Windows.Forms.Label();
			this.lbSenha = new System.Windows.Forms.Label();
			this.lbBanco = new System.Windows.Forms.Label();
			this.btConectar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtServidor
			// 
			this.txtServidor.Location = new System.Drawing.Point(110, 52);
			this.txtServidor.Name = "txtServidor";
			this.txtServidor.Size = new System.Drawing.Size(162, 20);
			this.txtServidor.TabIndex = 0;
			// 
			// txtUsuario
			// 
			this.txtUsuario.Location = new System.Drawing.Point(110, 85);
			this.txtUsuario.Name = "txtUsuario";
			this.txtUsuario.Size = new System.Drawing.Size(162, 20);
			this.txtUsuario.TabIndex = 1;
			// 
			// txtSenha
			// 
			this.txtSenha.Location = new System.Drawing.Point(110, 120);
			this.txtSenha.Name = "txtSenha";
			this.txtSenha.Size = new System.Drawing.Size(162, 20);
			this.txtSenha.TabIndex = 2;
			// 
			// cbTipoBanco
			// 
			this.cbTipoBanco.DisplayMember = "Descricao";
			this.cbTipoBanco.FormattingEnabled = true;
			this.cbTipoBanco.Location = new System.Drawing.Point(110, 16);
			this.cbTipoBanco.Name = "cbTipoBanco";
			this.cbTipoBanco.Size = new System.Drawing.Size(162, 21);
			this.cbTipoBanco.TabIndex = 3;
			this.cbTipoBanco.ValueMember = "Conexao";
			// 
			// cbBancoSchema
			// 
			this.cbBancoSchema.FormattingEnabled = true;
			this.cbBancoSchema.Location = new System.Drawing.Point(110, 154);
			this.cbBancoSchema.Name = "cbBancoSchema";
			this.cbBancoSchema.Size = new System.Drawing.Size(162, 21);
			this.cbBancoSchema.TabIndex = 4;
			// 
			// blTipoBanco
			// 
			this.blTipoBanco.AutoSize = true;
			this.blTipoBanco.Location = new System.Drawing.Point(12, 19);
			this.blTipoBanco.Name = "blTipoBanco";
			this.blTipoBanco.Size = new System.Drawing.Size(77, 13);
			this.blTipoBanco.TabIndex = 5;
			this.blTipoBanco.Text = "Tipo de Banco";
			// 
			// lbServidor
			// 
			this.lbServidor.AutoSize = true;
			this.lbServidor.Location = new System.Drawing.Point(12, 52);
			this.lbServidor.Name = "lbServidor";
			this.lbServidor.Size = new System.Drawing.Size(46, 13);
			this.lbServidor.TabIndex = 6;
			this.lbServidor.Text = "Servidor";
			// 
			// lbUsuario
			// 
			this.lbUsuario.AutoSize = true;
			this.lbUsuario.Location = new System.Drawing.Point(12, 92);
			this.lbUsuario.Name = "lbUsuario";
			this.lbUsuario.Size = new System.Drawing.Size(43, 13);
			this.lbUsuario.TabIndex = 7;
			this.lbUsuario.Text = "Usuário";
			// 
			// lbSenha
			// 
			this.lbSenha.AutoSize = true;
			this.lbSenha.Location = new System.Drawing.Point(12, 127);
			this.lbSenha.Name = "lbSenha";
			this.lbSenha.Size = new System.Drawing.Size(38, 13);
			this.lbSenha.TabIndex = 8;
			this.lbSenha.Text = "Senha";
			// 
			// lbBanco
			// 
			this.lbBanco.AutoSize = true;
			this.lbBanco.Location = new System.Drawing.Point(12, 162);
			this.lbBanco.Name = "lbBanco";
			this.lbBanco.Size = new System.Drawing.Size(88, 13);
			this.lbBanco.TabIndex = 9;
			this.lbBanco.Text = "Banco / Schema";
			// 
			// btConectar
			// 
			this.btConectar.Location = new System.Drawing.Point(197, 186);
			this.btConectar.Name = "btConectar";
			this.btConectar.Size = new System.Drawing.Size(75, 23);
			this.btConectar.TabIndex = 10;
			this.btConectar.Text = "Conectar";
			this.btConectar.UseVisualStyleBackColor = true;
			this.btConectar.Click += new System.EventHandler(this.btConectar_Click);
			// 
			// Autenticacao
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 221);
			this.Controls.Add(this.btConectar);
			this.Controls.Add(this.lbBanco);
			this.Controls.Add(this.lbSenha);
			this.Controls.Add(this.lbUsuario);
			this.Controls.Add(this.lbServidor);
			this.Controls.Add(this.blTipoBanco);
			this.Controls.Add(this.cbBancoSchema);
			this.Controls.Add(this.cbTipoBanco);
			this.Controls.Add(this.txtSenha);
			this.Controls.Add(this.txtUsuario);
			this.Controls.Add(this.txtServidor);
			this.Name = "Autenticacao";
			this.Text = "Autenticacao";
			this.Load += new System.EventHandler(this.Autenticacao_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtServidor;
		private System.Windows.Forms.TextBox txtUsuario;
		private System.Windows.Forms.TextBox txtSenha;
		private System.Windows.Forms.ComboBox cbTipoBanco;
		private System.Windows.Forms.ComboBox cbBancoSchema;
		private System.Windows.Forms.Label blTipoBanco;
		private System.Windows.Forms.Label lbServidor;
		private System.Windows.Forms.Label lbUsuario;
		private System.Windows.Forms.Label lbSenha;
		private System.Windows.Forms.Label lbBanco;
		private System.Windows.Forms.Button btConectar;
	}
}