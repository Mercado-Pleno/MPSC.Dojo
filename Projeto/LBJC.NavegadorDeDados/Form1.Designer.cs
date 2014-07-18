namespace LBJC.NavegadorDeDados
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.scVertical = new System.Windows.Forms.SplitContainer();
			this.tvDataConnection = new System.Windows.Forms.TreeView();
			this.ssStatus = new System.Windows.Forms.StatusStrip();
			this.tsBarraFerramentas = new System.Windows.Forms.ToolStrip();
			this.btNovoDocumento = new System.Windows.Forms.ToolStripButton();
			this.tabQueryResult = new System.Windows.Forms.TabControl();
			this.btExecutar = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.scVertical)).BeginInit();
			this.scVertical.Panel1.SuspendLayout();
			this.scVertical.Panel2.SuspendLayout();
			this.scVertical.SuspendLayout();
			this.tsBarraFerramentas.SuspendLayout();
			this.SuspendLayout();
			// 
			// scVertical
			// 
			this.scVertical.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.scVertical.Location = new System.Drawing.Point(0, 28);
			this.scVertical.Name = "scVertical";
			// 
			// scVertical.Panel1
			// 
			this.scVertical.Panel1.Controls.Add(this.tvDataConnection);
			// 
			// scVertical.Panel2
			// 
			this.scVertical.Panel2.Controls.Add(this.tabQueryResult);
			this.scVertical.Size = new System.Drawing.Size(1107, 528);
			this.scVertical.SplitterDistance = 216;
			this.scVertical.TabIndex = 0;
			// 
			// tvDataConnection
			// 
			this.tvDataConnection.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvDataConnection.Location = new System.Drawing.Point(0, 0);
			this.tvDataConnection.Name = "tvDataConnection";
			this.tvDataConnection.Size = new System.Drawing.Size(216, 528);
			this.tvDataConnection.TabIndex = 0;
			// 
			// ssStatus
			// 
			this.ssStatus.Location = new System.Drawing.Point(0, 559);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new System.Drawing.Size(1107, 22);
			this.ssStatus.TabIndex = 1;
			this.ssStatus.Text = "statusStrip1";
			// 
			// tsBarraFerramentas
			// 
			this.tsBarraFerramentas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btNovoDocumento,
            this.btExecutar});
			this.tsBarraFerramentas.Location = new System.Drawing.Point(0, 0);
			this.tsBarraFerramentas.Name = "tsBarraFerramentas";
			this.tsBarraFerramentas.Size = new System.Drawing.Size(1107, 25);
			this.tsBarraFerramentas.TabIndex = 2;
			this.tsBarraFerramentas.Text = "toolStrip1";
			// 
			// btNovoDocumento
			// 
			this.btNovoDocumento.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btNovoDocumento.Image = ((System.Drawing.Image)(resources.GetObject("btNovoDocumento.Image")));
			this.btNovoDocumento.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btNovoDocumento.Name = "btNovoDocumento";
			this.btNovoDocumento.Size = new System.Drawing.Size(23, 22);
			this.btNovoDocumento.Text = "Novo";
			this.btNovoDocumento.Click += new System.EventHandler(this.btNovoDocumento_Click);
			// 
			// tabQueryResult
			// 
			this.tabQueryResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabQueryResult.Location = new System.Drawing.Point(0, 0);
			this.tabQueryResult.Name = "tabQueryResult";
			this.tabQueryResult.SelectedIndex = 0;
			this.tabQueryResult.Size = new System.Drawing.Size(887, 528);
			this.tabQueryResult.TabIndex = 0;
			// 
			// btExecutar
			// 
			this.btExecutar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btExecutar.Image = ((System.Drawing.Image)(resources.GetObject("btExecutar.Image")));
			this.btExecutar.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btExecutar.Name = "btExecutar";
			this.btExecutar.Size = new System.Drawing.Size(23, 22);
			this.btExecutar.Text = "Executar";
			this.btExecutar.Click += new System.EventHandler(this.btExecutar_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1107, 581);
			this.Controls.Add(this.tsBarraFerramentas);
			this.Controls.Add(this.ssStatus);
			this.Controls.Add(this.scVertical);
			this.Name = "Form1";
			this.Text = "Form1";
			this.scVertical.Panel1.ResumeLayout(false);
			this.scVertical.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scVertical)).EndInit();
			this.scVertical.ResumeLayout(false);
			this.tsBarraFerramentas.ResumeLayout(false);
			this.tsBarraFerramentas.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer scVertical;
		private System.Windows.Forms.TreeView tvDataConnection;
		private System.Windows.Forms.StatusStrip ssStatus;
		private System.Windows.Forms.ToolStrip tsBarraFerramentas;
		private System.Windows.Forms.ToolStripButton btNovoDocumento;
		private System.Windows.Forms.TabControl tabQueryResult;
		private System.Windows.Forms.ToolStripButton btExecutar;
	}
}

