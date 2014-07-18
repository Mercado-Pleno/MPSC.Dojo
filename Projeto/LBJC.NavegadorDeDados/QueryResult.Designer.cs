namespace LBJC.NavegadorDeDados
{
	partial class QueryResult
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.scHorizontal = new System.Windows.Forms.SplitContainer();
			this.txtQuery = new System.Windows.Forms.TextBox();
			this.dgResult = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.scHorizontal)).BeginInit();
			this.scHorizontal.Panel1.SuspendLayout();
			this.scHorizontal.Panel2.SuspendLayout();
			this.scHorizontal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgResult)).BeginInit();
			this.SuspendLayout();
			// 
			// scHorizontal
			// 
			this.scHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scHorizontal.Location = new System.Drawing.Point(0, 0);
			this.scHorizontal.Name = "scHorizontal";
			this.scHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scHorizontal.Panel1
			// 
			this.scHorizontal.Panel1.Controls.Add(this.txtQuery);
			// 
			// scHorizontal.Panel2
			// 
			this.scHorizontal.Panel2.Controls.Add(this.dgResult);
			this.scHorizontal.Size = new System.Drawing.Size(795, 459);
			this.scHorizontal.SplitterDistance = 340;
			this.scHorizontal.TabIndex = 1;
			// 
			// txtQuery
			// 
			this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtQuery.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQuery.Location = new System.Drawing.Point(0, 0);
			this.txtQuery.Multiline = true;
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.Size = new System.Drawing.Size(795, 340);
			this.txtQuery.TabIndex = 0;
			this.txtQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyUp);
			// 
			// dgResult
			// 
			this.dgResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgResult.Location = new System.Drawing.Point(0, 0);
			this.dgResult.Name = "dgResult";
			this.dgResult.Size = new System.Drawing.Size(795, 115);
			this.dgResult.TabIndex = 0;
			this.dgResult.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgResult_Scroll);
			// 
			// QueryResult
			// 
			this.Controls.Add(this.scHorizontal);
			this.Size = new System.Drawing.Size(795, 459);
			this.scHorizontal.Panel1.ResumeLayout(false);
			this.scHorizontal.Panel1.PerformLayout();
			this.scHorizontal.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scHorizontal)).EndInit();
			this.scHorizontal.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgResult)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scHorizontal;
		private System.Windows.Forms.TextBox txtQuery;
		private System.Windows.Forms.DataGridView dgResult;
	}
}
