namespace AutoCompletar
{
	partial class Exemplo
	{
		/// <summary>
		/// Variável de designer necessária.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpar os recursos que estão sendo usados.
		/// </summary>
		/// <param name="disposing">verdade se for necessário descartar os recursos gerenciados; caso contrário, falso.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region código gerado pelo Windows Form Designer

		/// <summary>
		/// Método necessário para suporte do Designer - não modifique
		/// o conteúdo deste método com o editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.cbNome = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// cbNome
			// 
			this.cbNome.Location = new System.Drawing.Point(13, 13);
			this.cbNome.MaxDropDownItems = 10;
			this.cbNome.Name = "cbNome";
			this.cbNome.Size = new System.Drawing.Size(245, 21);
			this.cbNome.TabIndex = 0;
			this.cbNome.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbNome_KeyUp);
			// 
			// Exemplo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(270, 183);
			this.Controls.Add(this.cbNome);
			this.Name = "Exemplo";
			this.Text = "Exemplo Auto-Completar";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cbNome;
	}
}

