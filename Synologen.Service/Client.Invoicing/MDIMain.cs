using System;
using System.Drawing;
using System.Windows.Forms;
using Synologen.Service.Client.Invoicing.ChildForms;
using Synologen.Service.Client.Invoicing.Common;

namespace Synologen.Service.Client.Invoicing {
	public partial class MDIMain : Form {

		public MDIMain() {
			InitializeComponent();
		}

		public void SetStatus(Enumeration.ConnectionStatus status, string message) {
			toolStripStatusLabel.Text = message;
			Image image;
			switch(status) {
				case Enumeration.ConnectionStatus.Disconnected:
				image = Properties.Resources.label_red;
				break;
				case Enumeration.ConnectionStatus.Connecting:
				image = Properties.Resources.label_yellow;
				break;
				case Enumeration.ConnectionStatus.Connected:
				image = Properties.Resources.label_green;
				break;
				case Enumeration.ConnectionStatus.Disconnecting:
				image = Properties.Resources.label_yellow;
				break;
				default:
				throw new ArgumentOutOfRangeException("status");
			}
			toolStripStatusLabel.Image = image;
			Refresh();
		}

		#region Events

		private void ExitToolsStripMenuItem_Click(object sender, EventArgs e) {
			Close();
		}

		private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e) {
			LayoutMdi(MdiLayout.ArrangeIcons);
		}

		private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e) {
			foreach(var childForm in MdiChildren) {
				childForm.Close();
			}
		}

		private void ToolStripMenuItem_ImportOrders_Click(object sender, EventArgs e) {
			var childForm = new ImportExportOrders {MdiParent = this};
			childForm.Show();
			Refresh();
		}

		private void ToolStripMenuItem_Options_Click(object sender, EventArgs e) {
			var childForm = new Settings{ MdiParent = this};
			//childForm.WindowState = FormWindowState.Normal;
			childForm.Show();
			//childForm.WindowState = FormWindowState.Maximized;
			//childForm.Refresh();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			var form = new AboutBox();
			form.ShowDialog();
		}
		#endregion

		private void ToolStripMenuItem_SynchronizeInvoices_Click(object sender, EventArgs e) {
			var childForm = new StatusSync { MdiParent = this };
			childForm.Show();

		}

	}
}