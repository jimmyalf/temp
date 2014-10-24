namespace Synologen.Service.Client.Invoicing.ChildForms {
	partial class Settings {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
			this.label1 = new System.Windows.Forms.Label();
			this.txtCommonFilePath = new System.Windows.Forms.TextBox();
			this.txtCommonCompanyPath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCheckVismaConnection = new System.Windows.Forms.Button();
			this.btnCheckWPCConnection = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSelectCommonFilesFolder = new System.Windows.Forms.Button();
			this.btnSelectCompanyNameFolder = new System.Windows.Forms.Button();
			this.txtWebserviceAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtReportEmail = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "SPCS Sökväg - Gemensamma filer";
			// 
			// txtCommonFilePath
			// 
			this.txtCommonFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCommonFilePath.Location = new System.Drawing.Point(12, 33);
			this.txtCommonFilePath.Name = "txtCommonFilePath";
			this.txtCommonFilePath.Size = new System.Drawing.Size(307, 20);
			this.txtCommonFilePath.TabIndex = 1;
			// 
			// txtCommonCompanyPath
			// 
			this.txtCommonCompanyPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCommonCompanyPath.Location = new System.Drawing.Point(12, 76);
			this.txtCommonCompanyPath.Name = "txtCommonCompanyPath";
			this.txtCommonCompanyPath.Size = new System.Drawing.Size(307, 20);
			this.txtCommonCompanyPath.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "SPCS Sökväg - Företagskatalog";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(277, 322);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Spara";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCheckVismaConnection
			// 
			this.btnCheckVismaConnection.Location = new System.Drawing.Point(12, 102);
			this.btnCheckVismaConnection.Name = "btnCheckVismaConnection";
			this.btnCheckVismaConnection.Size = new System.Drawing.Size(148, 23);
			this.btnCheckVismaConnection.TabIndex = 5;
			this.btnCheckVismaConnection.Text = "Testa Anslutning mot SPCS";
			this.btnCheckVismaConnection.UseVisualStyleBackColor = true;
			this.btnCheckVismaConnection.Click += new System.EventHandler(this.btnCheckVismaConnection_Click);
			// 
			// btnCheckWPCConnection
			// 
			this.btnCheckWPCConnection.Location = new System.Drawing.Point(12, 212);
			this.btnCheckWPCConnection.Name = "btnCheckWPCConnection";
			this.btnCheckWPCConnection.Size = new System.Drawing.Size(151, 23);
			this.btnCheckWPCConnection.TabIndex = 10;
			this.btnCheckWPCConnection.Text = "Testa Anslutning mot WPC";
			this.btnCheckWPCConnection.UseVisualStyleBackColor = true;
			this.btnCheckWPCConnection.Click += new System.EventHandler(this.btnCheckWPCConnection_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(196, 322);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Avbryt";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSelectCommonFilesFolder
			// 
			this.btnSelectCommonFilesFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectCommonFilesFolder.Location = new System.Drawing.Point(326, 33);
			this.btnSelectCommonFilesFolder.Name = "btnSelectCommonFilesFolder";
			this.btnSelectCommonFilesFolder.Size = new System.Drawing.Size(26, 20);
			this.btnSelectCommonFilesFolder.TabIndex = 12;
			this.btnSelectCommonFilesFolder.Text = "...";
			this.btnSelectCommonFilesFolder.UseVisualStyleBackColor = true;
			this.btnSelectCommonFilesFolder.Click += new System.EventHandler(this.btnCommonFiles_Click);
			// 
			// btnSelectCompanyNameFolder
			// 
			this.btnSelectCompanyNameFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectCompanyNameFolder.Location = new System.Drawing.Point(326, 76);
			this.btnSelectCompanyNameFolder.Name = "btnSelectCompanyNameFolder";
			this.btnSelectCompanyNameFolder.Size = new System.Drawing.Size(26, 20);
			this.btnSelectCompanyNameFolder.TabIndex = 13;
			this.btnSelectCompanyNameFolder.Text = "...";
			this.btnSelectCompanyNameFolder.UseVisualStyleBackColor = true;
			this.btnSelectCompanyNameFolder.Click += new System.EventHandler(this.btnCompanyName_Click);
			// 
			// txtWebserviceAddress
			// 
			this.txtWebserviceAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtWebserviceAddress.Enabled = false;
			this.txtWebserviceAddress.Location = new System.Drawing.Point(12, 186);
			this.txtWebserviceAddress.Name = "txtWebserviceAddress";
			this.txtWebserviceAddress.Size = new System.Drawing.Size(307, 20);
			this.txtWebserviceAddress.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 170);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "WPC- Webserviceadress";
			// 
			// txtReportEmail
			// 
			this.txtReportEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtReportEmail.Location = new System.Drawing.Point(15, 296);
			this.txtReportEmail.Name = "txtReportEmail";
			this.txtReportEmail.Size = new System.Drawing.Size(307, 20);
			this.txtReportEmail.TabIndex = 17;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 280);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(127, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Raporterings-epostadress";
			// 
			// Settings
			// 
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(367, 358);
			this.Controls.Add(this.txtReportEmail);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtWebserviceAddress);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnSelectCompanyNameFolder);
			this.Controls.Add(this.btnSelectCommonFilesFolder);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnCheckWPCConnection);
			this.Controls.Add(this.btnCheckVismaConnection);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtCommonCompanyPath);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtCommonFilePath);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Settings";
			this.Text = "Inställningar";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtCommonFilePath;
		private System.Windows.Forms.TextBox txtCommonCompanyPath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCheckVismaConnection;
		private System.Windows.Forms.Button btnCheckWPCConnection;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSelectCommonFilesFolder;
		private System.Windows.Forms.Button btnSelectCompanyNameFolder;
		private System.Windows.Forms.TextBox txtWebserviceAddress;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtReportEmail;
		private System.Windows.Forms.Label label4;
	}
}