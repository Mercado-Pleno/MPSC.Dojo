namespace DireitoECia
{
	partial class frmAdvogado
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
			this.txtId = new System.Windows.Forms.TextBox();
			this.txtNome = new System.Windows.Forms.TextBox();
			this.btnIncluir = new System.Windows.Forms.Button();
			this.dgAdvogados = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dgAdvogados)).BeginInit();
			this.SuspendLayout();
			// 
			// txtId
			// 
			this.txtId.Location = new System.Drawing.Point(13, 13);
			this.txtId.Name = "txtId";
			this.txtId.Size = new System.Drawing.Size(100, 20);
			this.txtId.TabIndex = 0;
			// 
			// txtNome
			// 
			this.txtNome.Location = new System.Drawing.Point(120, 12);
			this.txtNome.Name = "txtNome";
			this.txtNome.Size = new System.Drawing.Size(232, 20);
			this.txtNome.TabIndex = 1;
			// 
			// btnIncluir
			// 
			this.btnIncluir.Location = new System.Drawing.Point(358, 12);
			this.btnIncluir.Name = "btnIncluir";
			this.btnIncluir.Size = new System.Drawing.Size(75, 23);
			this.btnIncluir.TabIndex = 2;
			this.btnIncluir.Text = "Incluir";
			this.btnIncluir.UseVisualStyleBackColor = true;
			this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
			// 
			// dgAdvogados
			// 
			this.dgAdvogados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgAdvogados.Location = new System.Drawing.Point(13, 49);
			this.dgAdvogados.Name = "dgAdvogados";
			this.dgAdvogados.Size = new System.Drawing.Size(462, 213);
			this.dgAdvogados.TabIndex = 3;
			// 
			// frmAdvogado
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 274);
			this.Controls.Add(this.dgAdvogados);
			this.Controls.Add(this.btnIncluir);
			this.Controls.Add(this.txtNome);
			this.Controls.Add(this.txtId);
			this.Name = "frmAdvogado";
			this.Text = "Advogados";
			((System.ComponentModel.ISupportInitialize)(this.dgAdvogados)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.TextBox txtNome;
		private System.Windows.Forms.Button btnIncluir;
		private System.Windows.Forms.DataGridView dgAdvogados;
	}
}

