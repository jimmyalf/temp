using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Exceptions;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Presentation;
using Spinit.Wpc.Synologen.Presentation.Components.Synologen;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;
using Enumerations=Spinit.Wpc.Synologen.OPQ.Presentation.Enumerations;
using FileCategories=Spinit.Wpc.Synologen.OPQ.Core.FileCategories;

namespace Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen
{
	public partial class OpqIndex : OpqPage
	{
		#region Internal params, Page load and Initialization

		protected void Page_Load (object sender, EventArgs e)
		{
			try {
				SetupLayout ();
				SetupWysiwyg ();
				if (!Page.IsPostBack) {
					PopulateRoutine (_nodeId);
					PopulateFiles (_nodeId);
					PopulateHistory (_nodeId);
				}
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		private void SetupLayout ()
		{
			phRoutine.Visible = false;
			if (_nodeId <= 0) return;
			var bNode = new BNode (_context);
			var node = bNode.GetNode (_nodeId, false);
			phRoutine.Visible = !node.IsMenu;
			phMenu.Visible = node.IsMenu;
			var bDocument = new BDocument (_context);
			var documents = bDocument.GetDocuments (_nodeId, _shopGroupId != null ? null : _shopId, null, _shopGroupId, DocumentTypes.Routine, null, true, false, false);
			if (documents.Count == 0) return;
			var document = documents [0];
			if (document.LockedByName == _context.UserName) {
				phPreview.Visible = false;
				phWysiwyg.Visible = true;
			}
			else {
				phWysiwyg.Visible = false;
				phPreview.Visible = true;
			}

		}

		private void SetupWysiwyg ()
		{
			_wysiwyg.CommonFilePath = new [] { Configuration.DocumentCentralRootUrl };
		}


		protected void Page_Init (object sender, EventArgs e)
		{
			RenderMemberSubMenu (Page.Master.Master);
		}

		/// <summary>
		/// Renders the submenu.
		/// </summary>
		public void RenderMemberSubMenu (MasterPage master)
		{
			var m = (SynologenMain) master;
			var _phSynologenSubMenu = m.SubMenu;
			var subMenu = new SmartMenu.Menu { ID = "SubMenu", ControlType = "ul", ItemControlType = "li", ItemWrapperElement = "span" };

			var itemCollection = new SmartMenu.ItemCollection ();
			itemCollection.AddItem ("Improvments", null, "Lista förbättringsförslag", "Visar alla inkomna förbättringsåtgärder", null, "btnImprovments_OnClick", false, null);
			itemCollection.AddItem ("ShopRoutine", null, "Visa butiksrutiner för vald nod", "Visar butiksrutiner för vald nod", null, "btnShowShopRoutine_OnClick", false, null);

			subMenu.MenuItems = itemCollection;

			m.SynologenSmartMenu.Render (subMenu, _phSynologenSubMenu);
		}

		#endregion

		#region Populaters

		private void PopulateRoutine (int nodeId)
		{
			if (nodeId <= 0) return;
			var bDocument = new BDocument (_context);
			var bNode = new BNode (_context);
			var node = bNode.GetNode (nodeId, false);
			if (node.IsMenu) {
				txtMenuName.Text = node.Name;
			}
			txtName.Text = node.Name;
			var documents = bDocument.GetDocuments (nodeId, _shopGroupId != null ? null : _shopId, null, _shopGroupId, DocumentTypes.Routine, null, true, false, false);
			if (documents.Count == 0) return;
			var document = documents [0];
			DocumentId = document.Id;
			_wysiwyg.Html = document.DocumentContent;
			ltContentPreview.Text = document.DocumentContent;
			SetDocumentStatus (document);
		}

		private void SetDocumentStatus (Document document)
		{
			if (document.LockedById > 0) {
				ltDocumentStatus.Text =
					string.Format (
						GetLocalResourceObject ("DocumentStatusLocked").ToString (),
						document.LockedByName);
			}
			else if (document.ApprovedById > 0) {
				ltDocumentStatus.Text =
					string.Format (
						GetLocalResourceObject ("DocumentStatusPublished").ToString (),
						document.ApprovedByName);
			}
		}

		private void PopulateFiles (int nodeId)
		{
			if (nodeId <= 0) {
				return;
			}
			var bFile = new BFile (_context);
			var files = bFile.GetFiles (
				nodeId,
				_shopGroupId != null ? null : _shopId,
				null,
				_shopGroupId,
				((_shopId != null) && (_shopId > 0)) || ((_shopGroupId != null) && (_shopGroupId > 0)) 
					? FileCategories.ShopRoutineDocuments 
					: FileCategories.SystemRoutineDocuments,
				true,
				true,
				true);
			gvFiles.DataSource = null;
			gvFiles.DataSource = files;
			gvFiles.DataBind ();
		}

		private void PopulateHistory (int nodeId)
		{
			if (nodeId <= 0) return;
			var bDocument = new BDocument (_context);
			var documents = bDocument.GetDocuments (nodeId, _shopGroupId != null ? null : _shopId, null, _shopGroupId, DocumentTypes.Routine, null, true, false, false);
			if (documents.Count == 0) {
				phNoHistory.Visible = true;
				phHistory.Visible = false;
				return;
			}
			var document = documents [0];

			var historyDocuments = bDocument.GetDocumentHistories (document.Id, false);
			if ((historyDocuments != null) && (historyDocuments.Count > 0)) {
				phNoHistory.Visible = false;
				phHistory.Visible = true;
				gvHistory.DataSource = null;
				gvHistory.DataSource = historyDocuments;
				gvHistory.DataBind ();
			}
			else {
				phNoHistory.Visible = true;
				phHistory.Visible = false;
			}
		}

		private void PopulateSelectedHistory (DocumentHistory document)
		{
			if (document == null) return;
			phViewHistory.Visible = true;
			ltHistory.Text = document.DocumentContent;
		}


		#endregion

		#region Page events

		protected void gvHistory_RowCommand (object sender, GridViewCommandEventArgs e)
		{
			try {

				switch (e.CommandName) {
					case "show":
						var row = (GridViewRow) ((Control) e.CommandSource).NamingContainer;
						int documentHistoryId = Convert.ToInt32 (gvHistory.DataKeys [row.RowIndex].Value);
						var date = new DateTime (long.Parse (e.CommandArgument.ToString ()));
						var bDocument = new BDocument (_context);
						var historyDocument = bDocument.GetDocumentHistory (documentHistoryId, date, false);
						PopulateSelectedHistory (historyDocument);
						break;
				}
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		protected void btnSaveMenu_Click (object sender, EventArgs e)
		{
			try {
				SaveMenu ();
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		protected void btnEdit_Click (object sender, EventArgs e)
		{
			try {
				SetupForEditPage ();
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		protected void btnSave_Click (object sender, EventArgs e)
		{
			try {
				var btnSave = (Button) sender;
				var typeOfSave =
					(Enumerations.DocumentSaveActions) Enum.Parse (typeof (Enumerations.DocumentSaveActions), btnSave.CommandName);
				string content = _wysiwyg.Html;
				switch (typeOfSave) {
					case Enumerations.DocumentSaveActions.SaveForLater:
						SaveForLater (content);
						break;
					case Enumerations.DocumentSaveActions.SaveAndPublish:
						SaveAndPublish (content);
						break;
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}

		}

		protected void gvFiles_RowCreated (object sender, GridViewRowEventArgs e)
		{
			try {
				if (e.Row.RowType == DataControlRowType.DataRow) {
					var opqFileId = (int) gvFiles.DataKeys [e.Row.RowIndex].Value;
					if (opqFileId <= 0) return;
					var bFile = new BFile (_context);
					var opqFile = bFile.GetFile (opqFileId, true);
					var ltFile = (Literal) e.Row.FindControl ("ltFile");
					var ltFileDate = (Literal) e.Row.FindControl ("ltFileDate");
					if (ltFile != null) {
						if (opqFile.BaseFile != null) {
							const string tag = "<a href=\"{0}\">{1}</a>";
							string link = string.Concat (Utility.Business.Globals.FilesUrl, opqFile.BaseFile.Name);
							string fileName = opqFile.BaseFile.Description.IsNotNullOrEmpty ()
							                  	? opqFile.BaseFile.Description.Substring (opqFile.BaseFile.Description.LastIndexOf ("/") + 1)
							                  	: opqFile.BaseFile.Name.Substring (opqFile.BaseFile.Name.LastIndexOf ("/") + 1);
							ltFile.Text = string.Format (tag, link, fileName);
						}
					}
					if (ltFileDate != null) {
						ltFileDate.Text = opqFile.CreatedDate.ToShortDateString ();
					}
				}
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		protected void gvFiles_Editing (object sender, GridViewEditEventArgs e)
		{
			try {}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		protected void gvFiles_Deleting (object sender, GridViewDeleteEventArgs e)
		{
			try {
				var fileId = (int) gvFiles.DataKeys [e.RowIndex].Value;
				if (fileId <= 0) return;
				var bFile = new BFile (_context);
				bFile.DeleteFile (fileId, false);
				PopulateFiles (_nodeId);
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		protected void gvFiles_RowCommand (object sender, GridViewCommandEventArgs e)
		{
			try {
				var row = (GridViewRow) ((Control) e.CommandSource).NamingContainer;
				int fileId = Convert.ToInt32 (gvFiles.DataKeys [row.RowIndex].Value);
				switch (e.CommandName) {
					case "overwrite":
						SetupForOverwrite (fileId);
						break;
					case "moveup":
						MoveFileUp (fileId);
						break;
					case "movedown":
						MoveFileDown (fileId);
						break;
				}
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>
		protected void btnDelete_AddConfirmDelete (object sender, EventArgs e)
		{
			try {
				var cc = new ClientConfirmation ();
				cc.AddConfirmation (ref sender, GetLocalResourceObject ("DocumentDeleteConfirmation").ToString ());
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		protected void btnUploadFile_Click (object sender, EventArgs e)
		{
			try {
				string documentRoot = (_shopGroupId != null) && (_shopGroupId > 0)
											? string.Concat (Configuration.DocumentShopGroupRootUrl, "/", _shopGroupId)
				                      	  	: (_shopId != null) && (_shopId > 0)
				                      			? string.Concat (Configuration.DocumentShopRootUrl, "/", _shopId)
				                      			: Configuration.DocumentCentralRootUrl;
				FileCategories fileCategories = ((_shopId != null) && (_shopId > 0)) || ((_shopGroupId != null) && (_shopGroupId > 0))
				                                	? FileCategories.ShopRoutineDocuments
				                                	: FileCategories.SystemRoutineDocuments;
				bool overwrite = false;
				int fileId = 0;
				if (btnUploadFile.CommandArgument.IsNotNullOrEmpty ()) {
					overwrite = true;
					fileId = Convert.ToInt32 (btnUploadFile.CommandArgument);
				}
				if ((!overwrite && UploadFile (_nodeId, documentRoot, uplFile, fileCategories, _shopGroupId != null ? null : _shopId, _shopGroupId)) ||
					(overwrite && UploadAndOverwriteFile (_nodeId, documentRoot, uplFile, fileCategories, _shopGroupId != null ? null : _shopId, _shopGroupId, fileId))) {
					ShowPositiveFeedBack ("UploadSuccess");
					PopulateFiles (_nodeId);
				}
			}
			catch (BaseCodeException ex) {
				ExceptionHandler.HandleException (Page, ex);
			}
		}

		protected void btnCancel_Click (object sender, EventArgs e)
		{
			CancelOverwrite ();
		}

		#endregion

		#region SubMenu Events

		protected void btnImprovments_OnClick (object sender, EventArgs e)
		{
			Response.Redirect (ComponentPages.OpqImprovments);
		}

		protected void btnShowShopRoutine_OnClick (object sender, EventArgs e)
		{

			Response.Redirect (string.Format (ComponentPages.OpqShopRoutines, _nodeId));
		}

		#endregion

		#region Page Actions

		private void MoveFileDown (int fileId)
		{
			var bFile = new BFile (_context);
			bFile.MoveFile (FileMoveActions.MoveDown, fileId);
			PopulateFiles (_nodeId);
		}

		private void MoveFileUp (int fileId)
		{
			var bFile = new BFile (_context);
			bFile.MoveFile (FileMoveActions.MoveUp, fileId);
			PopulateFiles (_nodeId);
		}

		private void SaveMenu ()
		{
			var bNode = new BNode (_context);
			var node = bNode.GetNode (_nodeId, true);
			bNode.ChangeNode (_nodeId, node.Parent, txtMenuName.Text);
			SessionContext.UserPositiveFeedBackResource = node.IsMenu ? "SuccessSaveMenu" : "SuccessSaveRoutine";
			Response.Redirect (string.Format (ComponentPages.OpqStartQueryNode, _nodeId));
		}

		private void SaveAndPublish (string content)
		{
			// Todo: Validation
			if (_nodeId <= 0) return;
			var bDocument = new BDocument (_context);
			Document document = DocumentId > 0
			                    	? bDocument.ChangeDocument (DocumentId, content)
									: bDocument.CreateDocument (_nodeId, _shopGroupId != null ? null : _shopId, _shopGroupId, DocumentTypes.Routine, content);
			if (document != null) {
				bDocument.Publish (document.Id);
				bDocument.UnLock (document.Id);
			}
			var bNode = new BNode (_context);
			var node = bNode.GetNode (_nodeId, true);
			bNode.ChangeNode (_nodeId, node.Parent, txtName.Text);
			SessionContext.UserPositiveFeedBackResource = node.IsMenu ? "SuccessSaveMenu" : "SuccessSaveRoutine";
			RedirectToStartPage ();
		}

		private void SaveForLater (string content)
		{
			// Todo: Validation
			if (_nodeId <= 0) return;
			var bDocument = new BDocument (_context);
			Document document = DocumentId > 0
			                    	? bDocument.ChangeDocument (DocumentId, content)
									: bDocument.CreateDocument (_nodeId, _shopGroupId != null ? null : _shopId, _shopGroupId, DocumentTypes.Routine, content);
			if (document != null) {
				bDocument.UnPublish (document.Id);
				bDocument.Lock (document.Id);
			}
			var bNode = new BNode (_context);
			var node = bNode.GetNode (_nodeId, true);
			bNode.ChangeNode (_nodeId, node.Parent, txtName.Text);
			SessionContext.UserPositiveFeedBackResource = node.IsMenu ? "SuccessSaveMenu" : "SuccessSaveRoutine";
			RedirectToStartPage ();
		}

		private bool UploadFile (int nodeId, string virtualUploadDir, FileUpload uploadControl, FileCategories category, int? shopId, int? shopGroupId)
		{
			try {
				if (!uploadControl.HasFile) {
					ShowNegativeFeedBack ("NoFileException");
					return false;
				}
				string uploadDir = Server.MapPath (virtualUploadDir);
				if (!Directory.Exists (uploadDir)) {
					Directory.CreateDirectory (uploadDir);
				}
				string fileName = string.Concat (
					OpqUtility.EncodeStringToUrl (Path.GetFileNameWithoutExtension (uploadControl.FileName)),
					Path.GetExtension (uploadControl.FileName));
				string fileDescription = uploadControl.FileName;
				if (!OpqUtility.IsAllowedExtension (uploadControl.FileName)) {
					ShowNegativeFeedBack ("ExtensionException", Configuration.UploadAllowedExtensions);
					return false;
				}
				int fileSize = uploadControl.PostedFile.ContentLength;
				if (fileSize > Configuration.UploadMaxFileSize) {
					ShowNegativeFeedBack ("FileSizeException", Configuration.UploadMaxFileSize.ToString ());
					return false;
				}

				//nameUrl is stored from root of fileurl path
				string nameUrl = Path.Combine (virtualUploadDir, fileName).Replace (Globals.FilesUrl, string.Empty);
				string extensionInfo = Path.GetExtension (uploadControl.FileName).ToLower ().Replace (".", string.Empty);

				var dFile = new Base.Data.File (Configuration.GetConfiguration (_context).ConnectionString);

				// Save file to BaseFile
				int baseFileId = dFile.AddFile (
					nameUrl.Replace ("//", "/").Replace ("\\", "/"),
					false,
					extensionInfo,
					null,
					fileDescription,
					_context.UserName);
				// Add id to filename
				fileName = string.Concat (
					OpqUtility.EncodeStringToUrl (Path.GetFileNameWithoutExtension (uploadControl.FileName)),
					"-",
					baseFileId,
					Path.GetExtension (uploadControl.FileName));
				nameUrl = Path.Combine (virtualUploadDir, fileName).Replace (Globals.FilesUrl, string.Empty);

				//Update with new id
				dFile.UpdateFile (baseFileId, nameUrl.Replace ("//", "/"), _context.UserName);

				// Save file to disk
				uploadControl.PostedFile.SaveAs (Path.Combine (uploadDir, fileName));

				// Save file to opq
				var bFile = new BFile (_context);
				var opqFile = bFile.CreateFile (_nodeId, shopId, null, shopGroupId, baseFileId, category);
				if (opqFile != null) {
					bFile.Publish (opqFile.Id);
					bFile.Unlock (opqFile.Id);
					return true;
				}
			}
			catch (Exception ex) {
				LogException (ex);
				ShowNegativeFeedBack ("UnexpectedUploadException");
			}
			return false;
		}

		private bool UploadAndOverwriteFile (
			int nodeId, 
			string virtualUploadDir, 
			FileUpload uploadControl, 
			FileCategories category, 
			int? shopId, 
			int? shopGroupId, 
			int fileId)
		{
			Page.MaintainScrollPositionOnPostBack = false;
			if (fileId <= 0) return false;
			try {
				if (!uploadControl.HasFile) {
					ShowNegativeFeedBack ("NoFileException");
					return false;
				}
				string uploadDir = Server.MapPath (virtualUploadDir);
				if (!Directory.Exists (uploadDir)) {
					Directory.CreateDirectory (uploadDir);
				}
				var bFile = new BFile (_context);
				var opqFile = bFile.GetFile (fileId, true);

				string fileName = Path.GetFileName (opqFile.BaseFile.Name);
				if (!OpqUtility.IsAllowedExtension (uploadControl.FileName)) {
					ShowNegativeFeedBack ("ExtensionException", Configuration.UploadAllowedExtensions);
					return false;
				}
				int fileSize = uploadControl.PostedFile.ContentLength;
				if (fileSize > Configuration.UploadMaxFileSize) {
					ShowNegativeFeedBack ("FileSizeException", Configuration.UploadMaxFileSize.ToString ());
					return false;
				}

				//Todo : Core file missing method for updating filesize. Should be done here!

				// overwrite file to disk
				uploadControl.PostedFile.SaveAs (Path.Combine (uploadDir, fileName));
				CancelOverwrite ();
				return true;

			}
			catch (Exception ex) {
				LogException (ex);
				ShowNegativeFeedBack ("UnexpectedUploadException");
			}
			return false;
		}

		private void SetupForEditPage ()
		{
			if (DocumentId <= 0) return;
			var bDocument = new BDocument (_context);
			bDocument.Lock (DocumentId);
			RedirectToStartPage ();
		}

		private void RedirectToStartPage ()
		{
			string redirectPage = _shopId > 0 || _shopGroupId > 0
			                      	? string.Format (ComponentPages.OpqStartQueryNodeAndShop, _nodeId, _shopId, _shopGroupId)
			                      	: string.Format (ComponentPages.OpqStartQueryNode, _nodeId);
			Response.Redirect (redirectPage);
		}

		private void SetupForOverwrite (int fileId)
		{
			var bFile = new BFile (_context);
			var opqFile = bFile.GetFile (fileId, true);
			if ((opqFile == null) && (opqFile.BaseFile == null)) return;
			btnUploadFile.CommandArgument = fileId.ToString ();
			btnUploadFile.Text = "Ladda upp & skriv över";
			string headerText = string.Format (
				"Välj fil nedan att skriva över filen \"{0}\" med",
				opqFile.BaseFile.Description);
			lblUploadFileHeader.Text = headerText;
			btnCancel.Visible = true;
			Page.MaintainScrollPositionOnPostBack = true;
		}

		private void CancelOverwrite ()
		{
			btnUploadFile.CommandArgument = string.Empty;
			btnUploadFile.Text = "Ladda upp";
			string headerText = string.Format ("Välj fil att ladda upp:");
			lblUploadFileHeader.Text = headerText;
			btnCancel.Visible = false;
			Page.MaintainScrollPositionOnPostBack = false;
		}

		#endregion

		#region Properties

		public int DocumentId
		{
			get
			{
				object o = ViewState ["OpqDocumentId"];
				if (o == null)
					return -1;
				return Convert.ToInt32 (o);
			}
			set { ViewState ["OpqDocumentId"] = value; }
		}

		#endregion

	}
}