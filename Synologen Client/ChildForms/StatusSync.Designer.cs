namespace Synologen.Client.ChildForms {
	partial class StatusSync {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusSync));
			this.btnCopyToClipBoard = new System.Windows.Forms.Button();
			this.orderList = new System.Windows.Forms.ListView();
			this.label1 = new System.Windows.Forms.Label();
			this.btnFetch = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnCopyToClipBoard
			// 
			this.btnCopyToClipBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopyToClipBoard.Location = new System.Drawing.Point(17, 175);
			this.btnCopyToClipBoard.Name = "btnCopyToClipBoard";
			this.btnCopyToClipBoard.Size = new System.Drawing.Size(94, 23);
			this.btnCopyToClipBoard.TabIndex = 12;
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
			this.orderList.Size = new System.Drawing.Size(347, 134);
			this.orderList.TabIndex = 11;
			this.orderList.UseCompatibleStateImageBehavior = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(123, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Fakturor att synkronisera";
			// 
			// btnFetch
			// 
			this.btnFetch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFetch.AutoSize = true;
			this.btnFetch.Location = new System.Drawing.Point(236, 175);
			this.btnFetch.Name = "btnFetch";
			this.btnFetch.Size = new System.Drawing.Size(128, 23);
			this.btnFetch.TabIndex = 9;
			this.btnFetch.Text = "Sök efter uppdateringar";
			this.btnFetch.UseVisualStyleBackColor = true;
			this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
			// 
			// StatusSync
			// 
			this.ClientSize = new System.Drawing.Size(378, 209);
			this.Controls.Add(this.btnCopyToClipBoard);
			this.Controls.Add(this.orderList);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnFetch);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StatusSync";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCopyToClipBoard;
		private System.Windows.Forms.ListView orderList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnFetch;
	}
}
