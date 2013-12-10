namespace MP.LBJC.Util
{
	using System;

	partial class AtualizadorHorario
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
			GC.Collect();
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// EnviadorSMS
			// 
			this.CanPauseAndContinue = true;
			this.CanShutdown = true;
			this.ServiceName = "MP PlenoSMS";

		}

		#endregion
	}
}
