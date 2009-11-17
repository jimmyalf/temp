namespace Spinit.Wpc.Synologen.OPQ.FillSynologenOpq
{
	partial class FillSynologenOpq
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager (typeof (FillSynologenOpq));
			this.cntMainBack = new System.Windows.Forms.SplitContainer ();
			this.cntLeftTree = new System.Windows.Forms.SplitContainer ();
			this.lblTreeNodesTitle = new System.Windows.Forms.Label ();
			this.treNodes = new System.Windows.Forms.TreeView ();
			this.cntRight = new System.Windows.Forms.SplitContainer ();
			this.cntRightTree = new System.Windows.Forms.SplitContainer ();
			this.lblFilesTitle = new System.Windows.Forms.Label ();
			this.treFiles = new System.Windows.Forms.TreeView ();
			this.diaBrowseFolder = new System.Windows.Forms.FolderBrowserDialog ();
			this.tbProperties = new System.Windows.Forms.TabControl ();
			this.tpNodes = new System.Windows.Forms.TabPage ();
			this.tpFiles = new System.Windows.Forms.TabPage ();
			this.pnlTbNodesTop = new System.Windows.Forms.Panel ();
			this.lblStartNodeTitle = new System.Windows.Forms.Label ();
			this.txtStartNode = new System.Windows.Forms.TextBox ();
			this.btnFillNode = new System.Windows.Forms.Button ();
			this.grpNodeContent = new System.Windows.Forms.GroupBox ();
			this.pnlNodeName = new System.Windows.Forms.Panel ();
			this.lblNodeName = new System.Windows.Forms.Label ();
			this.txtNodeName = new System.Windows.Forms.TextBox ();
			this.chkNodeIsMenu = new System.Windows.Forms.CheckBox ();
			this.pnlNodeContent = new System.Windows.Forms.Panel ();
			this.lblNodeRoutine = new System.Windows.Forms.Label ();
			this.txtRoutine = new System.Windows.Forms.RichTextBox ();
			this.grpUpload = new System.Windows.Forms.GroupBox ();
			this.lblFileUploadPath = new System.Windows.Forms.Label ();
			this.txtUploadFilePath = new System.Windows.Forms.TextBox ();
			this.btnNewUploadFile = new System.Windows.Forms.Button ();
			this.btnLoadFiles = new System.Windows.Forms.Button ();
			this.grpWpcLibrary = new System.Windows.Forms.GroupBox ();
			this.lblWpcPath = new System.Windows.Forms.Label ();
			this.txtWpcLibrary = new System.Windows.Forms.TextBox ();
			this.btnNewWpcLibrary = new System.Windows.Forms.Button ();
			this.grpConvert = new System.Windows.Forms.GroupBox ();
			this.chkConvertNodes = new System.Windows.Forms.CheckBox ();
			this.chkConvertDocuments = new System.Windows.Forms.CheckBox ();
			this.chkConvertFiles = new System.Windows.Forms.CheckBox ();
			this.btnConvert = new System.Windows.Forms.Button ();
			this.grpClean = new System.Windows.Forms.GroupBox ();
			this.btnClean = new System.Windows.Forms.Button ();
			this.chkFiles = new System.Windows.Forms.CheckBox ();
			this.chkCleanDocuments = new System.Windows.Forms.CheckBox ();
			this.chkCleanNodes = new System.Windows.Forms.CheckBox ();
			this.cntMainBack.Panel1.SuspendLayout ();
			this.cntMainBack.Panel2.SuspendLayout ();
			this.cntMainBack.SuspendLayout ();
			this.cntLeftTree.Panel1.SuspendLayout ();
			this.cntLeftTree.Panel2.SuspendLayout ();
			this.cntLeftTree.SuspendLayout ();
			this.cntRight.Panel1.SuspendLayout ();
			this.cntRight.Panel2.SuspendLayout ();
			this.cntRight.SuspendLayout ();
			this.cntRightTree.Panel1.SuspendLayout ();
			this.cntRightTree.Panel2.SuspendLayout ();
			this.cntRightTree.SuspendLayout ();
			this.tbProperties.SuspendLayout ();
			this.tpNodes.SuspendLayout ();
			this.tpFiles.SuspendLayout ();
			this.pnlTbNodesTop.SuspendLayout ();
			this.grpNodeContent.SuspendLayout ();
			this.pnlNodeName.SuspendLayout ();
			this.pnlNodeContent.SuspendLayout ();
			this.grpUpload.SuspendLayout ();
			this.grpWpcLibrary.SuspendLayout ();
			this.grpConvert.SuspendLayout ();
			this.grpClean.SuspendLayout ();
			this.SuspendLayout ();
			// 
			// cntMainBack
			// 
			this.cntMainBack.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cntMainBack.Location = new System.Drawing.Point (0, 0);
			this.cntMainBack.Name = "cntMainBack";
			// 
			// cntMainBack.Panel1
			// 
			this.cntMainBack.Panel1.Controls.Add (this.cntLeftTree);
			// 
			// cntMainBack.Panel2
			// 
			this.cntMainBack.Panel2.Controls.Add (this.cntRight);
			this.cntMainBack.Size = new System.Drawing.Size (1053, 514);
			this.cntMainBack.SplitterDistance = 208;
			this.cntMainBack.TabIndex = 0;
			// 
			// cntLeftTree
			// 
			this.cntLeftTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cntLeftTree.IsSplitterFixed = true;
			this.cntLeftTree.Location = new System.Drawing.Point (0, 0);
			this.cntLeftTree.Name = "cntLeftTree";
			this.cntLeftTree.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// cntLeftTree.Panel1
			// 
			this.cntLeftTree.Panel1.Controls.Add (this.lblTreeNodesTitle);
			// 
			// cntLeftTree.Panel2
			// 
			this.cntLeftTree.Panel2.Controls.Add (this.treNodes);
			this.cntLeftTree.Size = new System.Drawing.Size (208, 514);
			this.cntLeftTree.SplitterDistance = 25;
			this.cntLeftTree.TabIndex = 1;
			// 
			// lblTreeNodesTitle
			// 
			this.lblTreeNodesTitle.AutoSize = true;
			this.lblTreeNodesTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblTreeNodesTitle.Font = new System.Drawing.Font ("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblTreeNodesTitle.Location = new System.Drawing.Point (0, 0);
			this.lblTreeNodesTitle.Name = "lblTreeNodesTitle";
			this.lblTreeNodesTitle.Size = new System.Drawing.Size (60, 20);
			this.lblTreeNodesTitle.TabIndex = 0;
			this.lblTreeNodesTitle.Text = "Nodes";
			// 
			// treNodes
			// 
			this.treNodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treNodes.Location = new System.Drawing.Point (0, 0);
			this.treNodes.Name = "treNodes";
			this.treNodes.Size = new System.Drawing.Size (208, 485);
			this.treNodes.TabIndex = 0;
			// 
			// cntRight
			// 
			this.cntRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cntRight.Location = new System.Drawing.Point (0, 0);
			this.cntRight.Name = "cntRight";
			// 
			// cntRight.Panel1
			// 
			this.cntRight.Panel1.Controls.Add (this.cntRightTree);
			// 
			// cntRight.Panel2
			// 
			this.cntRight.Panel2.Controls.Add (this.tbProperties);
			this.cntRight.Size = new System.Drawing.Size (841, 514);
			this.cntRight.SplitterDistance = 245;
			this.cntRight.TabIndex = 0;
			// 
			// cntRightTree
			// 
			this.cntRightTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cntRightTree.Location = new System.Drawing.Point (0, 0);
			this.cntRightTree.Name = "cntRightTree";
			this.cntRightTree.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// cntRightTree.Panel1
			// 
			this.cntRightTree.Panel1.Controls.Add (this.lblFilesTitle);
			// 
			// cntRightTree.Panel2
			// 
			this.cntRightTree.Panel2.Controls.Add (this.treFiles);
			this.cntRightTree.Size = new System.Drawing.Size (245, 514);
			this.cntRightTree.SplitterDistance = 25;
			this.cntRightTree.TabIndex = 0;
			// 
			// lblFilesTitle
			// 
			this.lblFilesTitle.AutoSize = true;
			this.lblFilesTitle.Font = new System.Drawing.Font ("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFilesTitle.Location = new System.Drawing.Point (0, 0);
			this.lblFilesTitle.Name = "lblFilesTitle";
			this.lblFilesTitle.Size = new System.Drawing.Size (47, 20);
			this.lblFilesTitle.TabIndex = 0;
			this.lblFilesTitle.Text = "Files";
			// 
			// treFiles
			// 
			this.treFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treFiles.Location = new System.Drawing.Point (0, 0);
			this.treFiles.Name = "treFiles";
			this.treFiles.Size = new System.Drawing.Size (245, 485);
			this.treFiles.TabIndex = 0;
			// 
			// tbProperties
			// 
			this.tbProperties.Controls.Add (this.tpNodes);
			this.tbProperties.Controls.Add (this.tpFiles);
			this.tbProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbProperties.Location = new System.Drawing.Point (0, 0);
			this.tbProperties.Name = "tbProperties";
			this.tbProperties.SelectedIndex = 0;
			this.tbProperties.Size = new System.Drawing.Size (592, 514);
			this.tbProperties.TabIndex = 0;
			// 
			// tpNodes
			// 
			this.tpNodes.Controls.Add (this.grpNodeContent);
			this.tpNodes.Controls.Add (this.pnlTbNodesTop);
			this.tpNodes.Location = new System.Drawing.Point (4, 22);
			this.tpNodes.Name = "tpNodes";
			this.tpNodes.Padding = new System.Windows.Forms.Padding (3);
			this.tpNodes.Size = new System.Drawing.Size (584, 488);
			this.tpNodes.TabIndex = 0;
			this.tpNodes.Text = "Nodes";
			this.tpNodes.UseVisualStyleBackColor = true;
			// 
			// tpFiles
			// 
			this.tpFiles.Controls.Add (this.grpClean);
			this.tpFiles.Controls.Add (this.grpConvert);
			this.tpFiles.Controls.Add (this.grpWpcLibrary);
			this.tpFiles.Controls.Add (this.grpUpload);
			this.tpFiles.Location = new System.Drawing.Point (4, 22);
			this.tpFiles.Name = "tpFiles";
			this.tpFiles.Padding = new System.Windows.Forms.Padding (3);
			this.tpFiles.Size = new System.Drawing.Size (584, 488);
			this.tpFiles.TabIndex = 1;
			this.tpFiles.Text = "Files & Convert";
			this.tpFiles.UseVisualStyleBackColor = true;
			// 
			// pnlTbNodesTop
			// 
			this.pnlTbNodesTop.Controls.Add (this.btnFillNode);
			this.pnlTbNodesTop.Controls.Add (this.txtStartNode);
			this.pnlTbNodesTop.Controls.Add (this.lblStartNodeTitle);
			this.pnlTbNodesTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTbNodesTop.Location = new System.Drawing.Point (3, 3);
			this.pnlTbNodesTop.Name = "pnlTbNodesTop";
			this.pnlTbNodesTop.Size = new System.Drawing.Size (578, 37);
			this.pnlTbNodesTop.TabIndex = 0;
			// 
			// lblStartNodeTitle
			// 
			this.lblStartNodeTitle.AutoSize = true;
			this.lblStartNodeTitle.Location = new System.Drawing.Point (3, 9);
			this.lblStartNodeTitle.Name = "lblStartNodeTitle";
			this.lblStartNodeTitle.Size = new System.Drawing.Size (59, 13);
			this.lblStartNodeTitle.TabIndex = 0;
			this.lblStartNodeTitle.Text = "Start node:";
			// 
			// txtStartNode
			// 
			this.txtStartNode.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtStartNode.Location = new System.Drawing.Point (68, 6);
			this.txtStartNode.Name = "txtStartNode";
			this.txtStartNode.Size = new System.Drawing.Size (423, 20);
			this.txtStartNode.TabIndex = 1;
			// 
			// btnFillNode
			// 
			this.btnFillNode.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFillNode.Location = new System.Drawing.Point (497, 4);
			this.btnFillNode.Name = "btnFillNode";
			this.btnFillNode.Size = new System.Drawing.Size (75, 23);
			this.btnFillNode.TabIndex = 2;
			this.btnFillNode.Text = "Fill Nodes";
			this.btnFillNode.UseVisualStyleBackColor = true;
			this.btnFillNode.Click += new System.EventHandler (this.btnFillNode_Click);
			// 
			// grpNodeContent
			// 
			this.grpNodeContent.Controls.Add (this.pnlNodeContent);
			this.grpNodeContent.Controls.Add (this.pnlNodeName);
			this.grpNodeContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpNodeContent.Location = new System.Drawing.Point (3, 40);
			this.grpNodeContent.Name = "grpNodeContent";
			this.grpNodeContent.Size = new System.Drawing.Size (578, 445);
			this.grpNodeContent.TabIndex = 1;
			this.grpNodeContent.TabStop = false;
			this.grpNodeContent.Text = "Node Content";
			// 
			// pnlNodeName
			// 
			this.pnlNodeName.Controls.Add (this.chkNodeIsMenu);
			this.pnlNodeName.Controls.Add (this.txtNodeName);
			this.pnlNodeName.Controls.Add (this.lblNodeName);
			this.pnlNodeName.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlNodeName.Location = new System.Drawing.Point (3, 16);
			this.pnlNodeName.Name = "pnlNodeName";
			this.pnlNodeName.Size = new System.Drawing.Size (572, 61);
			this.pnlNodeName.TabIndex = 0;
			// 
			// lblNodeName
			// 
			this.lblNodeName.AutoSize = true;
			this.lblNodeName.Location = new System.Drawing.Point (3, 11);
			this.lblNodeName.Name = "lblNodeName";
			this.lblNodeName.Size = new System.Drawing.Size (38, 13);
			this.lblNodeName.TabIndex = 0;
			this.lblNodeName.Text = "Name:";
			// 
			// txtNodeName
			// 
			this.txtNodeName.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtNodeName.Location = new System.Drawing.Point (47, 8);
			this.txtNodeName.Name = "txtNodeName";
			this.txtNodeName.Size = new System.Drawing.Size (512, 20);
			this.txtNodeName.TabIndex = 1;
			// 
			// chkNodeIsMenu
			// 
			this.chkNodeIsMenu.AutoSize = true;
			this.chkNodeIsMenu.Location = new System.Drawing.Point (6, 34);
			this.chkNodeIsMenu.Name = "chkNodeIsMenu";
			this.chkNodeIsMenu.Size = new System.Drawing.Size (64, 17);
			this.chkNodeIsMenu.TabIndex = 2;
			this.chkNodeIsMenu.Text = "Is Menu";
			this.chkNodeIsMenu.UseVisualStyleBackColor = true;
			// 
			// pnlNodeContent
			// 
			this.pnlNodeContent.Controls.Add (this.txtRoutine);
			this.pnlNodeContent.Controls.Add (this.lblNodeRoutine);
			this.pnlNodeContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlNodeContent.Location = new System.Drawing.Point (3, 77);
			this.pnlNodeContent.Name = "pnlNodeContent";
			this.pnlNodeContent.Size = new System.Drawing.Size (572, 365);
			this.pnlNodeContent.TabIndex = 1;
			// 
			// lblNodeRoutine
			// 
			this.lblNodeRoutine.AutoSize = true;
			this.lblNodeRoutine.Location = new System.Drawing.Point (3, 9);
			this.lblNodeRoutine.Name = "lblNodeRoutine";
			this.lblNodeRoutine.Size = new System.Drawing.Size (76, 13);
			this.lblNodeRoutine.TabIndex = 0;
			this.lblNodeRoutine.Text = "Node Routine:";
			// 
			// txtRoutine
			// 
			this.txtRoutine.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtRoutine.Location = new System.Drawing.Point (6, 25);
			this.txtRoutine.Name = "txtRoutine";
			this.txtRoutine.Size = new System.Drawing.Size (563, 337);
			this.txtRoutine.TabIndex = 1;
			this.txtRoutine.Text = "";
			// 
			// grpUpload
			// 
			this.grpUpload.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpUpload.Controls.Add (this.btnLoadFiles);
			this.grpUpload.Controls.Add (this.btnNewUploadFile);
			this.grpUpload.Controls.Add (this.txtUploadFilePath);
			this.grpUpload.Controls.Add (this.lblFileUploadPath);
			this.grpUpload.Location = new System.Drawing.Point (6, 8);
			this.grpUpload.Name = "grpUpload";
			this.grpUpload.Size = new System.Drawing.Size (570, 43);
			this.grpUpload.TabIndex = 0;
			this.grpUpload.TabStop = false;
			this.grpUpload.Text = "The upload library";
			// 
			// lblFileUploadPath
			// 
			this.lblFileUploadPath.AutoSize = true;
			this.lblFileUploadPath.Location = new System.Drawing.Point (6, 16);
			this.lblFileUploadPath.Name = "lblFileUploadPath";
			this.lblFileUploadPath.Size = new System.Drawing.Size (68, 13);
			this.lblFileUploadPath.TabIndex = 0;
			this.lblFileUploadPath.Text = "Upload Files:";
			// 
			// txtUploadFilePath
			// 
			this.txtUploadFilePath.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtUploadFilePath.Location = new System.Drawing.Point (80, 13);
			this.txtUploadFilePath.Name = "txtUploadFilePath";
			this.txtUploadFilePath.Size = new System.Drawing.Size (362, 20);
			this.txtUploadFilePath.TabIndex = 1;
			// 
			// btnNewUploadFile
			// 
			this.btnNewUploadFile.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNewUploadFile.Location = new System.Drawing.Point (447, 11);
			this.btnNewUploadFile.Name = "btnNewUploadFile";
			this.btnNewUploadFile.Size = new System.Drawing.Size (36, 23);
			this.btnNewUploadFile.TabIndex = 2;
			this.btnNewUploadFile.Text = "...";
			this.btnNewUploadFile.UseVisualStyleBackColor = true;
			this.btnNewUploadFile.Click += new System.EventHandler (this.btnNewUploadFile_Click);
			// 
			// btnLoadFiles
			// 
			this.btnLoadFiles.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadFiles.Location = new System.Drawing.Point (488, 11);
			this.btnLoadFiles.Name = "btnLoadFiles";
			this.btnLoadFiles.Size = new System.Drawing.Size (76, 23);
			this.btnLoadFiles.TabIndex = 3;
			this.btnLoadFiles.Text = "Load Files";
			this.btnLoadFiles.UseVisualStyleBackColor = true;
			this.btnLoadFiles.Click += new System.EventHandler (this.btnLoadFiles_Click);
			// 
			// grpWpcLibrary
			// 
			this.grpWpcLibrary.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpWpcLibrary.Controls.Add (this.btnNewWpcLibrary);
			this.grpWpcLibrary.Controls.Add (this.txtWpcLibrary);
			this.grpWpcLibrary.Controls.Add (this.lblWpcPath);
			this.grpWpcLibrary.Location = new System.Drawing.Point (6, 57);
			this.grpWpcLibrary.Name = "grpWpcLibrary";
			this.grpWpcLibrary.Size = new System.Drawing.Size (572, 48);
			this.grpWpcLibrary.TabIndex = 1;
			this.grpWpcLibrary.TabStop = false;
			this.grpWpcLibrary.Text = "The Wpc Synologen File Library";
			// 
			// lblWpcPath
			// 
			this.lblWpcPath.AutoSize = true;
			this.lblWpcPath.Location = new System.Drawing.Point (6, 16);
			this.lblWpcPath.Name = "lblWpcPath";
			this.lblWpcPath.Size = new System.Drawing.Size (82, 13);
			this.lblWpcPath.TabIndex = 0;
			this.lblWpcPath.Text = "The File Library:";
			// 
			// txtWpcLibrary
			// 
			this.txtWpcLibrary.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtWpcLibrary.Location = new System.Drawing.Point (94, 16);
			this.txtWpcLibrary.Name = "txtWpcLibrary";
			this.txtWpcLibrary.Size = new System.Drawing.Size (428, 20);
			this.txtWpcLibrary.TabIndex = 1;
			// 
			// btnNewWpcLibrary
			// 
			this.btnNewWpcLibrary.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNewWpcLibrary.Location = new System.Drawing.Point (528, 14);
			this.btnNewWpcLibrary.Name = "btnNewWpcLibrary";
			this.btnNewWpcLibrary.Size = new System.Drawing.Size (36, 23);
			this.btnNewWpcLibrary.TabIndex = 2;
			this.btnNewWpcLibrary.Text = "...";
			this.btnNewWpcLibrary.UseVisualStyleBackColor = true;
			this.btnNewWpcLibrary.Click += new System.EventHandler (this.btnNewWpcLibrary_Click);
			// 
			// grpConvert
			// 
			this.grpConvert.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpConvert.Controls.Add (this.btnConvert);
			this.grpConvert.Controls.Add (this.chkConvertFiles);
			this.grpConvert.Controls.Add (this.chkConvertDocuments);
			this.grpConvert.Controls.Add (this.chkConvertNodes);
			this.grpConvert.Location = new System.Drawing.Point (6, 111);
			this.grpConvert.Name = "grpConvert";
			this.grpConvert.Size = new System.Drawing.Size (572, 50);
			this.grpConvert.TabIndex = 2;
			this.grpConvert.TabStop = false;
			this.grpConvert.Text = "Convert";
			// 
			// chkConvertNodes
			// 
			this.chkConvertNodes.AutoSize = true;
			this.chkConvertNodes.Checked = true;
			this.chkConvertNodes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkConvertNodes.Location = new System.Drawing.Point (9, 19);
			this.chkConvertNodes.Name = "chkConvertNodes";
			this.chkConvertNodes.Size = new System.Drawing.Size (57, 17);
			this.chkConvertNodes.TabIndex = 0;
			this.chkConvertNodes.Text = "Nodes";
			this.chkConvertNodes.UseVisualStyleBackColor = true;
			// 
			// chkConvertDocuments
			// 
			this.chkConvertDocuments.AutoSize = true;
			this.chkConvertDocuments.Checked = true;
			this.chkConvertDocuments.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkConvertDocuments.Location = new System.Drawing.Point (72, 19);
			this.chkConvertDocuments.Name = "chkConvertDocuments";
			this.chkConvertDocuments.Size = new System.Drawing.Size (80, 17);
			this.chkConvertDocuments.TabIndex = 1;
			this.chkConvertDocuments.Text = "Documents";
			this.chkConvertDocuments.UseVisualStyleBackColor = true;
			// 
			// chkConvertFiles
			// 
			this.chkConvertFiles.AutoSize = true;
			this.chkConvertFiles.Checked = true;
			this.chkConvertFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkConvertFiles.Location = new System.Drawing.Point (158, 19);
			this.chkConvertFiles.Name = "chkConvertFiles";
			this.chkConvertFiles.Size = new System.Drawing.Size (47, 17);
			this.chkConvertFiles.TabIndex = 2;
			this.chkConvertFiles.Text = "Files";
			this.chkConvertFiles.UseVisualStyleBackColor = true;
			// 
			// btnConvert
			// 
			this.btnConvert.Location = new System.Drawing.Point (211, 15);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size (75, 23);
			this.btnConvert.TabIndex = 3;
			this.btnConvert.Text = "Convert";
			this.btnConvert.UseVisualStyleBackColor = true;
			this.btnConvert.Click += new System.EventHandler (this.btnConvert_Click);
			// 
			// grpClean
			// 
			this.grpClean.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpClean.Controls.Add (this.btnClean);
			this.grpClean.Controls.Add (this.chkFiles);
			this.grpClean.Controls.Add (this.chkCleanDocuments);
			this.grpClean.Controls.Add (this.chkCleanNodes);
			this.grpClean.Location = new System.Drawing.Point (6, 167);
			this.grpClean.Name = "grpClean";
			this.grpClean.Size = new System.Drawing.Size (572, 50);
			this.grpClean.TabIndex = 3;
			this.grpClean.TabStop = false;
			this.grpClean.Text = "Convert";
			// 
			// btnClean
			// 
			this.btnClean.Location = new System.Drawing.Point (211, 15);
			this.btnClean.Name = "btnClean";
			this.btnClean.Size = new System.Drawing.Size (75, 23);
			this.btnClean.TabIndex = 3;
			this.btnClean.Text = "Convert";
			this.btnClean.UseVisualStyleBackColor = true;
			this.btnClean.Click += new System.EventHandler (this.btnClean_Click);
			// 
			// chkFiles
			// 
			this.chkFiles.AutoSize = true;
			this.chkFiles.Checked = true;
			this.chkFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFiles.Location = new System.Drawing.Point (158, 19);
			this.chkFiles.Name = "chkFiles";
			this.chkFiles.Size = new System.Drawing.Size (47, 17);
			this.chkFiles.TabIndex = 2;
			this.chkFiles.Text = "Files";
			this.chkFiles.UseVisualStyleBackColor = true;
			// 
			// chkCleanDocuments
			// 
			this.chkCleanDocuments.AutoSize = true;
			this.chkCleanDocuments.Checked = true;
			this.chkCleanDocuments.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCleanDocuments.Location = new System.Drawing.Point (72, 19);
			this.chkCleanDocuments.Name = "chkCleanDocuments";
			this.chkCleanDocuments.Size = new System.Drawing.Size (80, 17);
			this.chkCleanDocuments.TabIndex = 1;
			this.chkCleanDocuments.Text = "Documents";
			this.chkCleanDocuments.UseVisualStyleBackColor = true;
			// 
			// chkCleanNodes
			// 
			this.chkCleanNodes.AutoSize = true;
			this.chkCleanNodes.Checked = true;
			this.chkCleanNodes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCleanNodes.Location = new System.Drawing.Point (9, 19);
			this.chkCleanNodes.Name = "chkCleanNodes";
			this.chkCleanNodes.Size = new System.Drawing.Size (57, 17);
			this.chkCleanNodes.TabIndex = 0;
			this.chkCleanNodes.Text = "Nodes";
			this.chkCleanNodes.UseVisualStyleBackColor = true;
			// 
			// FillSynologenOpq
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size (1053, 514);
			this.Controls.Add (this.cntMainBack);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject ("$this.Icon")));
			this.Name = "FillSynologenOpq";
			this.Text = "Fill Synologen OPQ";
			this.Load += new System.EventHandler (this.FillSynologenOpq_Load);
			this.cntMainBack.Panel1.ResumeLayout (false);
			this.cntMainBack.Panel2.ResumeLayout (false);
			this.cntMainBack.ResumeLayout (false);
			this.cntLeftTree.Panel1.ResumeLayout (false);
			this.cntLeftTree.Panel1.PerformLayout ();
			this.cntLeftTree.Panel2.ResumeLayout (false);
			this.cntLeftTree.ResumeLayout (false);
			this.cntRight.Panel1.ResumeLayout (false);
			this.cntRight.Panel2.ResumeLayout (false);
			this.cntRight.ResumeLayout (false);
			this.cntRightTree.Panel1.ResumeLayout (false);
			this.cntRightTree.Panel1.PerformLayout ();
			this.cntRightTree.Panel2.ResumeLayout (false);
			this.cntRightTree.ResumeLayout (false);
			this.tbProperties.ResumeLayout (false);
			this.tpNodes.ResumeLayout (false);
			this.tpFiles.ResumeLayout (false);
			this.pnlTbNodesTop.ResumeLayout (false);
			this.pnlTbNodesTop.PerformLayout ();
			this.grpNodeContent.ResumeLayout (false);
			this.pnlNodeName.ResumeLayout (false);
			this.pnlNodeName.PerformLayout ();
			this.pnlNodeContent.ResumeLayout (false);
			this.pnlNodeContent.PerformLayout ();
			this.grpUpload.ResumeLayout (false);
			this.grpUpload.PerformLayout ();
			this.grpWpcLibrary.ResumeLayout (false);
			this.grpWpcLibrary.PerformLayout ();
			this.grpConvert.ResumeLayout (false);
			this.grpConvert.PerformLayout ();
			this.grpClean.ResumeLayout (false);
			this.grpClean.PerformLayout ();
			this.ResumeLayout (false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer cntMainBack;
		private System.Windows.Forms.SplitContainer cntLeftTree;
		private System.Windows.Forms.SplitContainer cntRight;
		private System.Windows.Forms.Label lblTreeNodesTitle;
		private System.Windows.Forms.TreeView treNodes;
		private System.Windows.Forms.SplitContainer cntRightTree;
		private System.Windows.Forms.Label lblFilesTitle;
		private System.Windows.Forms.TreeView treFiles;
		private System.Windows.Forms.FolderBrowserDialog diaBrowseFolder;
		private System.Windows.Forms.TabControl tbProperties;
		private System.Windows.Forms.TabPage tpNodes;
		private System.Windows.Forms.Panel pnlTbNodesTop;
		private System.Windows.Forms.Label lblStartNodeTitle;
		private System.Windows.Forms.TabPage tpFiles;
		private System.Windows.Forms.TextBox txtStartNode;
		private System.Windows.Forms.Button btnFillNode;
		private System.Windows.Forms.GroupBox grpNodeContent;
		private System.Windows.Forms.Panel pnlNodeName;
		private System.Windows.Forms.CheckBox chkNodeIsMenu;
		private System.Windows.Forms.TextBox txtNodeName;
		private System.Windows.Forms.Label lblNodeName;
		private System.Windows.Forms.Panel pnlNodeContent;
		private System.Windows.Forms.RichTextBox txtRoutine;
		private System.Windows.Forms.Label lblNodeRoutine;
		private System.Windows.Forms.GroupBox grpUpload;
		private System.Windows.Forms.Button btnLoadFiles;
		private System.Windows.Forms.Button btnNewUploadFile;
		private System.Windows.Forms.TextBox txtUploadFilePath;
		private System.Windows.Forms.Label lblFileUploadPath;
		private System.Windows.Forms.GroupBox grpWpcLibrary;
		private System.Windows.Forms.Button btnNewWpcLibrary;
		private System.Windows.Forms.TextBox txtWpcLibrary;
		private System.Windows.Forms.Label lblWpcPath;
		private System.Windows.Forms.GroupBox grpConvert;
		private System.Windows.Forms.CheckBox chkConvertFiles;
		private System.Windows.Forms.CheckBox chkConvertDocuments;
		private System.Windows.Forms.CheckBox chkConvertNodes;
		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.GroupBox grpClean;
		private System.Windows.Forms.Button btnClean;
		private System.Windows.Forms.CheckBox chkFiles;
		private System.Windows.Forms.CheckBox chkCleanDocuments;
		private System.Windows.Forms.CheckBox chkCleanNodes;
	}
}

