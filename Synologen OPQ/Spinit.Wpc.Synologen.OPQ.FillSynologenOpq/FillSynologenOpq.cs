using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using Spinit.Wpc.Content.Data;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.FillSynologenOpq.Properties;
using Spinit.Wpc.Utility.Core;
using File=Spinit.Wpc.Synologen.OPQ.Core.Entities.File;

namespace Spinit.Wpc.Synologen.OPQ.FillSynologenOpq
{
	/// <summary>
	/// The main form.
	/// </summary>

	public partial class FillSynologenOpq : Form
	{
		// Internal data
		private readonly Core.Context _context;

		/// <summary>
		/// Default constructor 
		/// </summary>

		public FillSynologenOpq ()
		{
			InitializeComponent ();

			_context = new Core.Context (
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
				string.Empty,
				Settings.Default.SynologenAdminUserId,
				Settings.Default.SynologenAdminUserName);
		}

		#region Internal Methods

		#region Fill Trees

		/// <summary>
		/// Fills the contnet-node-tree recursive with content-nodes.
		/// </summary>
		/// <param name="parent">The parent.</param>

		private static TreeNode [] FillContentTree (int parent)
		{
			Tree tree = new Tree (Settings.Default.SynologenSourceConnection);

			ArrayList trees = tree.GetTrees (parent, 0, 0, 0, 0, 0, null, false, false, false, false, true, false, -1);

			TreeNode[] nodes = null;

			if ((trees != null) && (trees.Count > 0)) {
				nodes = new TreeNode [trees.Count];
				for (int i = 0; i < trees.Count; i++) {
					TreeRow treeRow = (TreeRow) trees [i];
					nodes [i] = new TreeNode
					{
						Text = treeRow.Name,
						Tag = treeRow,
						SelectedImageIndex = treeRow.TreeType == Tree.TreeTypes.Menu ? 0 : 1,
						ImageIndex = treeRow.TreeType == Tree.TreeTypes.Menu ? 0 : 1
					};

					TreeNode[] childs = FillContentTree (treeRow.Id);

					if ((childs != null) && (childs.Length > 0)) {
						nodes [i].Nodes.AddRange (childs);
					}
				}
			}

			return nodes;
		}

		/// <summary>
		/// Fills the opq-node-tree recursive with opq-nodes.
		/// </summary>
		/// <param name="parent">The parent node - null for root.</param>
		/// <returns>A list of nodes.</returns>

		private TreeNode [] FillOpqNodes (int? parent)
		{
			BNode bNode = new BNode (_context);

			IList<Node> nodes;

			try {
				nodes = bNode.GetNodes (parent, null, false, false, true);
			}
			catch (ObjectNotFoundException e) {
				if ((ObjectNotFoundErrors) e.ErrorCode == ObjectNotFoundErrors.NodeNotFound) {
					return null;
				}

				throw;
			}

			TreeNode[] treeNodes = null;

			if ((nodes != null) && (nodes.Count > 0)) {
				treeNodes = new TreeNode [nodes.Count];
				for (int i = 0; i < nodes.Count; i++) {
					treeNodes [i] = new TreeNode
					{
						Text = nodes [i].Name,
						Tag = nodes [i],
						SelectedImageIndex = nodes [i].IsMenu ? 0 : 1,
						ImageIndex = nodes [i].IsMenu ? 0 : 1
					};

					TreeNode[] childs = FillOpqNodes (nodes [i].Id);

					if ((childs != null) && (childs.Length > 0)) {
						treeNodes [i].Nodes.AddRange (childs);
					}

					if ((nodes [i].Documents != null) && (nodes [i].Documents.Count > 0)) {
						TreeNode[] documents = new TreeNode[nodes [i].Documents.Count];
						for (int j = 0; j < nodes [i].Documents.Count; j++) {
							documents [j] = new TreeNode
							{
								Text = nodes [i].Documents [j].DocTpeId.ToString (),
								Tag = nodes [i].Documents [j],
								SelectedImageIndex = 2,
								ImageIndex = 2
							};
						}

						treeNodes [i].Nodes.AddRange (documents);
					}
					
					if ((nodes [i].Files != null) && (nodes [i].Files.Count > 0)) {
						TreeNode [] files = new TreeNode [nodes [i].Files.Count];
						for (int j = 0; j < nodes [i].Files.Count; j++) {
							files [j] = new TreeNode
							{
								Text = nodes [i].Files [j].BaseFile.Name,
								Tag = nodes [i].Files [j],
								SelectedImageIndex = 3,
								ImageIndex = 3
							};
						}

						treeNodes [i].Nodes.AddRange (files);
					}
				}
			}

			return treeNodes;
		}

		/// <summary>
		/// Fills the file-tree recursive.
		/// </summary>
		/// <param name="parent">The parent directory.</param>
		/// <returns>A list of nodes.</returns>

		private static TreeNode [] FillFiles (string parent)
		{
			string[] directories = Directory.GetDirectories (parent);

			TreeNode[] nodes = null;
			if ((directories != null) && (directories.Length > 0)) {
				nodes = new TreeNode[directories.Length];
				for (int i = 0; i < directories.Length; i++) {
					nodes [i] = new TreeNode
					{
						Text = Path.GetFileName (directories [i]), 
						Tag = directories [i],
						SelectedImageIndex = 0,
						ImageIndex = 0
					};
					
					TreeNode[] childs = FillFiles (directories [i]);

					if ((childs != null) && (childs.Length > 0)) {
						nodes [i].Nodes.AddRange (childs);
					}
				}
			}

			string[] files = Directory.GetFiles (parent);
			if ((files != null) && (files.Length > 0)) {
				nodes = new TreeNode [files.Length];
				for (int i = 0; i < files.Length; i++) {
					nodes [i] = new TreeNode
					{
						Text = Path.GetFileName (files [i]),
						Tag = files [i],
						SelectedImageIndex = 1,
						ImageIndex = 1
					};
				}
			}

			return nodes;
		}

		#endregion

		#region Convert

		/// <summary>
		/// Converts the a content-tree to a Opq node recursive.
		/// </summary>
		/// <param name="treeNode">The tree-node.</param>
		/// <param name="parent">The node-parent.</param>

		private void ConvertNodes (TreeNode treeNode, int? parent)
		{
			TreeRow treeRow = (TreeRow) treeNode.Tag;

			BNode bNode = new BNode (_context);

			if (chkConvertOnlyChecked.Checked && !treeNode.Checked) {
				try {
					Node searchNode = bNode.GetNode (parent, treeRow.Name, true);

					if (treeNode.Nodes.Count > 0) {
						foreach (TreeNode childNode in treeNode.Nodes) {
							ConvertNodes (childNode, searchNode.Id);
						}
					}

					return;
				}
				catch (ObjectNotFoundException e) {
					if ((ObjectNotFoundErrors) e.ErrorCode == ObjectNotFoundErrors.NodeNotFound) {
						return;
					}

					throw;
				}
			}

			Node node = bNode.CreateNode (parent, treeRow.Name, treeRow.TreeType == Tree.TreeTypes.Menu ? true : false);
			bNode.UnLock (node.Id);
			bNode.Publish (node.Id);

			foreach (TreeNode childNode in treeNode.Nodes) {
				ConvertNodes (childNode, node.Id);
			}
		}

		/// <summary>
		/// Converts all content-pages to opq-document-routines.
		/// </summary>
		/// <param name="treeNode">The tree-node.</param>
		/// <param name="parent">The node-parent.</param>

		public void ConvertDocuments (TreeNode treeNode, int? parent)
		{
			TreeRow treeRow = (TreeRow) treeNode.Tag;

			BNode bNode = new BNode (_context);

			if ((chkConvertOnlyChecked.Checked && treeNode.Checked) || !chkConvertOnlyChecked.Checked) {
				try {
					Node node = bNode.GetNode (parent, treeRow.Name, true);

					if (treeRow.TreeType != Tree.TreeTypes.Menu) {
						Page page = new Page (Settings.Default.SynologenSourceConnection);

						IContentPageRow pageRow = page.GetPageContent (treeRow.Page);

						BDocument bDocument = new BDocument (_context);

						Document document = bDocument.CreateDocument (node.Id, null, DocumentTypes.Routine, pageRow.Content);
						bDocument.UnLock (document.Id);
						bDocument.Publish (document.Id);
					}

					if (treeNode.Nodes.Count > 0) {
						foreach (TreeNode childNode in treeNode.Nodes) {
							ConvertDocuments (childNode, node.Id);
						}
					}

					return;
				}
				catch (ObjectNotFoundException e) {
					if ((ObjectNotFoundErrors) e.ErrorCode == ObjectNotFoundErrors.NodeNotFound) {
						return;
					}

					throw;
				}
			}
		}

		/// <summary>
		/// Convertes a file-system-file to opq-file with category System-routine.
		/// </summary>
		/// <param name="node">The Opq-node.</param>
		/// <param name="path">The path to the directory where the files/nodes are in.</param>

		public void ConvertFiles (Node node, string path)
		{
			BNode bNode = new BNode (_context);
			Base.Data.File baseFile = new Base.Data.File (Settings.Default.SynologenDestinationConnection);
			if ((node == null) || node.IsMenu) {
				try {
					IList<Node> nodes = node == null
					                    	? bNode.GetNodes (null, null, false, false, false)
					                    	: bNode.GetNodes (node.Id, null, false, false, false);

					if ((nodes != null) && (nodes.Count > 0)) {
						foreach (Node childNode in nodes) {
							ConvertFiles (childNode, Path.Combine (path, ParsePath (childNode.Name)));
						}
					}

					return;
				}
				catch (ObjectNotFoundException e) {
					if ((ObjectNotFoundErrors) e.ErrorCode != ObjectNotFoundErrors.NodeNotFound) {
						throw;
					}
				}
			}

			if (node == null) {
				return;
			}

			BFile bFile = new BFile (_context);

			if (Directory.Exists (path)) {
				string[] files = Directory.GetFiles (path);

				if ((files != null) && (files.Length > 0)) {
					foreach (string file in files) {
						string newFileName = string.Concat (
							EncodeStringToUrl (Path.GetFileNameWithoutExtension (file)),
							"-",
							node.Id.ToString (),
							Path.GetExtension (file));

						System.IO.File.Copy (file, Path.Combine (txtWpcLibrary.Text, newFileName));

						newFileName = string.Concat (
							txtWpcLibrary.Text
								.Replace (Settings.Default.FileRootRemove, "")
								.Replace ("\\", "/"),
							"/",
							newFileName);

						int fleId = baseFile.AddFile (
							newFileName,
							false,
							null,
							null,
							Path.GetFileName (file),
							Settings.Default.SynologenAdminUserName);

						bFile.CreateFile (node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
					}
				}
			}
		}

		#endregion

		#region Clean

		/// <summary>
		/// Converts the a content-tree to a Opq node recursive.
		/// </summary>
		/// <param name="treeNode">The tree-node.</param>
		/// <param name="cleanAll">If true=>clean all down.</param>

		private void CleanNodes (TreeNode treeNode, bool cleanAll)
		{
			Node node;
			if (treeNode.Tag is Node) {
				node = (Node) treeNode.Tag;
			}
			else {
				return;
			}

			BNode bNode = new BNode (_context);

			try {
				if (treeNode.Nodes.Count > 0) {
					foreach (TreeNode childNode in treeNode.Nodes) {
						CleanNodes (childNode, cleanAll ? true : treeNode.Checked);
					}
				}

				if (!chkCleanOnlyChecked.Checked || cleanAll || (chkCleanOnlyChecked.Checked && treeNode.Checked)) {
					bNode.DeleteNode (node.Id, true);
				}
			}
			catch (ObjectNotFoundException e) {
				if ((ObjectNotFoundErrors) e.ErrorCode == ObjectNotFoundErrors.NodeNotFound) {
					return;
				}

				throw;
			}
		}

		/// <summary>
		/// Clean documents from opq-nodes.
		/// </summary>
		/// <param name="treeNode">The tree-node.</param>
		/// <param name="cleanAll">If true=>clean all down.</param>

		public void CleanDocuments (TreeNode treeNode, bool cleanAll)
		{
			if (treeNode.Tag is Node) {
				if (treeNode.Nodes.Count > 0) {
					foreach (TreeNode childNode in treeNode.Nodes) {
						CleanDocuments (childNode, cleanAll ? true : treeNode.Checked);
					}
				}
				return;
			}

			Document document;
			if (treeNode.Tag is Document) {
				document = treeNode.Tag as Document; 
			}
			else {
				return;
			}

			if (!chkCleanOnlyChecked.Checked || cleanAll || (chkCleanOnlyChecked.Checked && treeNode.Checked)) {
				try {
					BDocument bDocument = new BDocument (_context);

					bDocument.DeleteDocument (document.Id, true);

					return;
				}
				catch (ObjectNotFoundException e) {
					if ((ObjectNotFoundErrors) e.ErrorCode == ObjectNotFoundErrors.NodeNotFound) {
						return;
					}

					throw;
				}
			}
		}

		/// <summary>
		/// Cleans files from opq-nodes.
		/// </summary>
		/// <param name="treeNode">The tree-node.</param>
		/// <param name="cleanAll">If true=>clean all down.</param>

		public void CleanFiles (TreeNode treeNode, bool cleanAll)
		{
			if (treeNode.Tag is Node) {
				if (treeNode.Nodes.Count > 0) {
					foreach (TreeNode childNode in treeNode.Nodes) {
						CleanFiles (childNode, cleanAll ? true : treeNode.Checked);
					}
				}
				return;
			}

			File file;
			if (treeNode.Tag is File) {
				file = treeNode.Tag as File;
			}
			else {
				return;
			}

			if (!chkCleanOnlyChecked.Checked || cleanAll || (chkCleanOnlyChecked.Checked && treeNode.Checked)) {
				try {
					BFile bFile = new BFile (_context);

					bFile.DeleteFile (file.Id, true);

					Base.Data.File baseFile = new Base.Data.File (Settings.Default.SynologenDestinationConnection);

					if (file.FleId != 0) {
						Base.Data.FileRow fileRow = baseFile.GetFileRow (file.FleId);
						
						baseFile.DeleteFile (file.FleId);

						string name = GetFileName (fileRow.Name);

						name = Path.Combine (txtWpcLibrary.Text, GetFileName (fileRow.Name));

						System.IO.File.Delete (Path.Combine (txtWpcLibrary.Text, GetFileName (fileRow.Name)));
					}

					return;
				}
				catch (ObjectNotFoundException e) {
					if ((ObjectNotFoundErrors) e.ErrorCode == ObjectNotFoundErrors.NodeNotFound) {
						return;
					}

					throw;
				}
			}
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Parses a path for search-files.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>A parsed path.</returns>

		private static string ParsePath (string path)
		{
			if (!string.IsNullOrEmpty (path)) {
				return path.Replace ("/", "");
			}

			return path;
		}

		/// <summary>
		/// Gets the name-part from the complete file-name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The file-name.</returns>

		private static string GetFileName (string name)
		{
			if (!string.IsNullOrEmpty (name)) {
				int inx = name.LastIndexOf ("/");

				return name.Substring (inx + 1, name.Length - inx - 1);
			}

			return string.Empty;
		}

		/// <summary>
		/// Encodes a string to avoid forbidden chars in url
		/// </summary>
		/// <param name="value">The original string.</param>
		/// <returns>The encoded string.</returns>
		
		private static string EncodeStringToUrl (string value)
		{
			value = value.ToLower ();
			value = value.Replace ('å', 'a');
			value = value.Replace ('ä', 'a');
			value = value.Replace ('ö', 'o');
			value = value.Replace (' ', '-');
			value = Regex.Replace (value, "[^a-z0-9-]", "");
			value = Regex.Replace (value, "[-]+", "-");
			return value;
		}

		#endregion

		#endregion

		#region Events

		/// <summary>
		/// Fires after the node has been selected.
		/// </summary>
		/// <param name="sender">The sending-object.</param>
		/// <param name="e">The event-arguments.</param>

		private void treNodes_AfterSelect (object sender, TreeViewEventArgs e)
		{
			TreeRow treeRow = (TreeRow) e.Node.Tag;

			txtNodeName.Text = treeRow.Name;

			if ((treeRow.TreeType == Tree.TreeTypes.Page) || (treeRow.TreeType == Tree.TreeTypes.DefaultPage)) {
				Page page = new Page (Settings.Default.SynologenSourceConnection);

				IContentPageRow pageRow = page.GetPageContent (treeRow.Page);

				txtRoutine.Text = pageRow.Content;
				chkNodeIsMenu.Checked = false;
			}
			else {
				txtRoutine.Text = string.Empty;
				chkNodeIsMenu.Checked = true;
			}
		}

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
			treNodes.Nodes.Clear ();

			TreeNode [] nodes = FillContentTree (int.Parse (txtStartNode.Text));

			if ((nodes != null) && (nodes.Length > 0)) {
				treNodes.Nodes.AddRange (nodes);
			}
		}

		/// <summary>
		/// Changes the upload file path.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnNewUploadFile_Click (object sender, EventArgs e)
		{
			diaBrowseFolder.Description = "Select folder to fetch files from.";
			diaBrowseFolder.RootFolder = Environment.SpecialFolder.MyComputer;
			diaBrowseFolder.ShowNewFolderButton = false;
			 
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
			treFiles.Nodes.Clear ();
			TreeNode[] nodes = FillFiles (txtUploadFilePath.Text);

			if ((nodes != null) && (nodes.Length > 0)) {
				treFiles.Nodes.AddRange (nodes);
			}
		}

		/// <summary>
		/// Changes the output file path.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnNewWpcLibrary_Click (object sender, EventArgs e)
		{
			diaBrowseFolder.Description = "Select folder to output files to.";
			diaBrowseFolder.RootFolder = Environment.SpecialFolder.MyComputer;
			diaBrowseFolder.ShowNewFolderButton = true;

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
			if (chkConvertNodes.Checked) {
				foreach (TreeNode treeNode in treNodes.Nodes) {
					if (((TreeRow) treeNode.Tag).TreeType
					    == Tree.TreeTypes.Menu) {
						ConvertNodes (treeNode, null);
					}
				}
			}

			if (chkConvertDocuments.Checked) {
				foreach (TreeNode treeNode in treNodes.Nodes) {
					if (((TreeRow) treeNode.Tag).TreeType
					    == Tree.TreeTypes.Menu) {
						ConvertDocuments (treeNode, null);
					}
				}
			}

			if (chkConvertFiles.Checked) {
				Base.Data.File baseFile = new Base.Data.File (Settings.Default.SynologenDestinationConnection);

				baseFile.AddFile (
					txtWpcLibrary.Text
							.Replace (Settings.Default.FileRootRemove, "")
							.Replace ("\\", "/"),
					true,
					null,
					null,
					Path.GetFileNameWithoutExtension (txtWpcLibrary.Text),
					Settings.Default.SynologenAdminUserName);

				ConvertFiles (null, ParsePath (txtUploadFilePath.Text));
			}
		}

		/// <summary>
		/// Refreshes the opq-node tree.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnRefersOpqNodes_Click (object sender, EventArgs e)
		{
			treOpqNodes.Nodes.Clear ();

			TreeNode [] nodes = FillOpqNodes (null);

			if ((nodes != null) && (nodes.Length > 0)) {
				treOpqNodes.Nodes.AddRange (nodes);
			}
		}

		/// <summary>
		/// Cleans nodes, documents and files.
		/// </summary>
		/// <param name="sender">The sending object.</param>
		/// <param name="e">The event-arguments.</param>

		private void btnClean_Click (object sender, EventArgs e)
		{
			if (chkCleanNodes.Checked) {
				foreach (TreeNode treeNode in treOpqNodes.Nodes) {
					CleanNodes (treeNode, false);
				}
			}

			if (chkCleanDocuments.Checked) {
				foreach (TreeNode treeNode in treOpqNodes.Nodes) {
					CleanDocuments (treeNode, false);
				}
			}

			if (chkCleanFiles.Checked) {
				foreach (TreeNode treeNode in treOpqNodes.Nodes) {
					CleanFiles (treeNode, false);
				
					Base.Data.File baseFile = new Base.Data.File (Settings.Default.SynologenDestinationConnection);

					Base.Data.FileRow fileRow = baseFile.GetFileRow (
                     	txtWpcLibrary.Text
                     		.Replace (Settings.Default.FileRootRemove, "")
                     		.Replace ("\\", "/"));

					if (fileRow != null) {
						baseFile.DeleteFile (fileRow.Id);
					}
				}
			}
		}

		#endregion
	}
}
