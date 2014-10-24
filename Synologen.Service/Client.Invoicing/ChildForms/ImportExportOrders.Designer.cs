namespace Synologen.Service.Client.Invoicing.ChildForms {
	partial class ImportExportOrders {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExportOrders));
			this.btnCopyToClipBoard = new System.Windows.Forms.Button();
			this.orderList = new System.Windows.Forms.ListView();
			this.label1 = new System.Windows.Forms.Label();
			this.btnFetchOrders = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnCopyToClipBoard
			// 
			this.btnCopyToClipBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopyToClipBoard.Location = new System.Drawing.Point(17, 67);
			this.btnCopyToClipBoard.Name = "btnCopyToClipBoard";
			this.btnCopyToClipBoard.Size = new System.Drawing.Size(94, 23);
			this.btnCopyToClipBoard.TabIndex = 8;
			this.btnCopyToClipBoard.Text = "Kopiera lista";
			this.btnCopyToClipBoard.UseVisualStyleBackColor = true;
			this.btnCopyToClipBoard.Click += new System.EventHandler(this.btnCopyToClipBoard_Click);
			// 
			// orderList
			// 
			this.orderList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.orderList.Location = new System.Drawing.Point(17, 27);
			this.orderList.Name = "orderList";
			this.orderList.Size = new System.Drawing.Size(240, 26);
			this.orderList.TabIndex = 7;
			this.orderList.UseCompatibleStateImageBehavior = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Ordrar att importera";
			// 
			// btnFetchOrders
			// 
			this.btnFetchOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFetchOrders.AutoSize = true;
			this.btnFetchOrders.Location = new System.Drawing.Point(115, 67);
			this.btnFetchOrders.Name = "btnFetchOrders";
			this.btnFetchOrders.Size = new System.Drawing.Size(142, 23);
			this.btnFetchOrders.TabIndex = 5;
			this.btnFetchOrders.Text = "Hämta ordrar";
			this.btnFetchOrders.UseVisualStyleBackColor = true;
			this.btnFetchOrders.Click += new System.EventHandler(this.btnFetchOrders_Click);
			// 
			// ImportExportOrders
			// 
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(271, 101);
			this.Controls.Add(this.btnCopyToClipBoard);
			this.Controls.Add(this.orderList);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnFetchOrders);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ImportExportOrders";
			this.ShowInTaskbar = false;
			this.Text = "Importera ordrar till SPCS";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.ImportOrders_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCopyToClipBoard;
		private System.Windows.Forms.ListView orderList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnFetchOrders;

	}
}