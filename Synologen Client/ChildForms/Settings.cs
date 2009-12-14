using System;
using System.IO;
using System.Windows.Forms;
using Synologen.Client.Common;


namespace Synologen.Client.ChildForms {
	internal partial class Settings : FormBase {

		public Settings() {
			InitializeComponent();
			PopulateSettings();
		}

		private void PopulateSettings() {
			txtCommonFilePath.Text = Properties.Settings.Default.SPCSCommonFilesPath;
			txtCommonCompanyPath.Text = Properties.Settings.Default.SPCSCompanyPath;
			txtWebserviceAddress.Text = Spinit.Wpc.Synologen.ServiceLibrary.ConfigurationSettings.Client.GetEndpointAddress();
			txtReportEmail.Text = Properties.Settings.Default.ReportEmailAddress;
		}



		private void btnSave_Click(object sender, EventArgs e) {
			Properties.Settings.Default.SPCSCommonFilesPath = txtCommonFilePath.Text;
			Properties.Settings.Default.SPCSCompanyPath = txtCommonCompanyPath.Text;
			Properties.Settings.Default.ReportEmailAddress = txtReportEmail.Text;
			Properties.Settings.Default.Save();
			LogCurrentSettings();
			Close();
		}
		private void btnCheckVismaConnection_Click(object sender, EventArgs e) {
			Cursor = Cursors.WaitCursor;
			var connectionOK = ClientUtility.CheckSPCSConnection(txtCommonFilePath.Text, txtCommonCompanyPath.Text);
			Cursor = Cursors.Default;
			if(connectionOK) {
				MessageBox.Show(
					"Anslutningen fungerar",
					"SPCS - Anslutningstest",
					MessageBoxButtons.OK,
					MessageBoxIcon.Asterisk);
			}
			else {
				MessageBox.Show(
					"Anslutningen misslyckades",
					"SPCS - Anslutningstest",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		private void btnCheckWPCConnection_Click(object sender, EventArgs e) {
			Cursor = Cursors.WaitCursor;
			var connectionOK = ClientUtility.CheckWPCConnection();
			Cursor = Cursors.Default;
			if(connectionOK) {
				MessageBox.Show(
					"Anslutningen fungerar",
					"WPC - Anslutningstest",
					MessageBoxButtons.OK,
					MessageBoxIcon.Asterisk);
			}
			else {
				MessageBox.Show(
					"Anslutningen misslyckades",
					"WPC - Anslutningstest",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void btnCompanyName_Click(object sender, EventArgs e) {
			var folderBrowser = new FolderBrowserDialog {
			                                            	ShowNewFolderButton = false,
			                                            	SelectedPath = GetCurrentOrParentFolderPath(txtCommonCompanyPath.Text),
			                                            };
			if(folderBrowser.ShowDialog() != DialogResult.OK) return;
			txtCommonCompanyPath.Text = folderBrowser.SelectedPath;
		}

		private void btnCommonFiles_Click(object sender, EventArgs e) {
			var folderBrowser = new FolderBrowserDialog {
			                                            	ShowNewFolderButton = false,
			                                            	SelectedPath = GetCurrentOrParentFolderPath(txtCommonFilePath.Text),
			                                            };
			if(folderBrowser.ShowDialog() != DialogResult.OK)return;
			txtCommonFilePath.Text = folderBrowser.SelectedPath;
		}

		private static string GetCurrentOrParentFolderPath(string originalPath ) {
			if (Directory.Exists(originalPath)) {
				var dir = new DirectoryInfo(originalPath);
				return dir.FullName;
			}
			var lastBackSlashPosition = originalPath.LastIndexOf('\\');
			var newPath =  originalPath.Substring(0, lastBackSlashPosition);
			return Directory.Exists(newPath) ? newPath : "C:\\";
		}

		private void LogCurrentSettings() {
			var message = "Client Saved Settings:" + Environment.NewLine;
			message += "Common file path: " + txtCommonFilePath.Text + Environment.NewLine;
			message += "Company path: " + txtCommonCompanyPath.Text + Environment.NewLine;
			ClientUtility.TryLogInformation(message);
		}

		private void Settings_Resize(object sender, EventArgs e) {

		}
	}
}