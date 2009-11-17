using System;
using System.Windows.Forms;
using Spinit.Wpc.Synologen.OPQ.FillSynologenOpq.Properties;

namespace Spinit.Wpc.Synologen.OPQ.FillSynologenOpq
{
	/// <summary>
	/// The main form.
	/// </summary>

	public partial class FillSynologenOpq : Form
	{
		/// <summary>
		/// Default constructor 
		/// </summary>

		public FillSynologenOpq ()
		{
			InitializeComponent ();
		}

		#region Events

		/// <summary>
		/// Fires when the dialog loads.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void FillSynologenOpq_Load (object sender, EventArgs e)
		{
			txtStartNode.Text = Settings.Default.StartNode.ToString ();
			txtUploadFilePath.Text = Settings.Default.UploadFileRootPath;
			txtWpcLibrary.Text = Settings.Default.FileRootPath;
		}

		/// <summary>
		/// Fills the nodes from content into tree.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnFillNode_Click (object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Changes the upload file path.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnNewUploadFile_Click (object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty (txtUploadFilePath.Text)) {
				diaBrowseFolder.SelectedPath = txtUploadFilePath.Text;
			}

			if (diaBrowseFolder.ShowDialog (this) == DialogResult.OK) {
				txtUploadFilePath.Text = diaBrowseFolder.SelectedPath;
			}
		}

		/// <summary>
		/// Loads the files into the the tree.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnLoadFiles_Click (object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Changes the output file path.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnNewWpcLibrary_Click (object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty (txtWpcLibrary.Text)) {
				diaBrowseFolder.SelectedPath = txtWpcLibrary.Text;
			}

			if (diaBrowseFolder.ShowDialog (this) == DialogResult.OK) {
				txtWpcLibrary.Text = diaBrowseFolder.SelectedPath;
			}
		}

		/// <summary>
		/// Converts nodes, documents and files.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnConvert_Click (object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Cleans nodes, documents and files.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnClean_Click (object sender, EventArgs e)
		{

		}

		#endregion


	}
}
