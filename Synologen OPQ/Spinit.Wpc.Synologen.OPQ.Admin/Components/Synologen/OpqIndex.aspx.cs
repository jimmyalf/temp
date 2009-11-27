using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Exceptions;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Presentation;
using Spinit.Wpc.Synologen.Presentation;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen
{
	public partial class OpqIndex : OpqPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			SetupLayout();
			if (!Page.IsPostBack)
			{
				try
				{
					PopulateRoutine(_nodeId);
					PopulateFiles(_nodeId);
				}
				catch (BaseCodeException ex)
				{
					ExceptionHandler.HandleException(Page, ex);
				}
			}
		}

		private void SetupLayout()
		{
			phRoutine.Visible = false;
			if (_nodeId <= 0) return;
			var bNode = new BNode(_context);
			var node = bNode.GetNode(_nodeId, false);
			if (!node.IsMenu)
			{
				phRoutine.Visible = true;
			}
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			RenderMemberSubMenu(Page.Master.Master);
		}

		/// <summary>
		/// Renders the submenu.
		/// </summary>
		public void RenderMemberSubMenu(MasterPage master)
		{
			var m = (SynologenMain)master;
			var _phSynologenSubMenu = m.SubMenu;
			var subMenu = new SmartMenu.Menu { ID = "SubMenu", ControlType = "ul", ItemControlType = "li", ItemWrapperElement = "span" };

			var itemCollection = new SmartMenu.ItemCollection();
			itemCollection.AddItem("New", null, "Ny", "Skapa ny node", null, "btnAdd_OnClick", false, null);

			subMenu.MenuItems = itemCollection;

			m.SynologenSmartMenu.Render(subMenu, _phSynologenSubMenu);
		}




		private void PopulateRoutine(int nodeId)
		{
			if (nodeId <= 0) return;
			var bDocument = new BDocument(_context);
			var bNode = new BNode(_context);
			var node = bNode.GetNode(nodeId, false);
			txtName.Text = node.Name;
			var documents =
				bDocument.GetDocuments(nodeId, null, null, DocumentTypes.Routine, null, true, false, false);
			if (documents.Count == 0) return;
			var document = documents[0];
			bDocument.Lock(document.Id);
			DocumentId = document.Id;
			_wysiwyg.Html = document.DocumentContent;
		}

		private void PopulateFiles(int nodeId)
		{
			if (nodeId <= 0) return;
			var bFile = new BFile(_context);
			var files = bFile.GetFiles(nodeId, null, null, FileCategories.SystemRoutineDocuments, true, true, true);
			gvFiles.DataSource = null;
			gvFiles.DataSource = files;
			gvFiles.DataBind();
		}



		#region Page events

		protected void btnSave_Click(object sender, EventArgs e)
		{
			var btnSave = (Button)sender;
			var typeOfSave = (Enumerations.DocumentSaveActions)Enum.Parse(typeof(Enumerations.DocumentSaveActions), btnSave.CommandName);
			string content = _wysiwyg.Html;
			switch (typeOfSave)
			{
				case Enumerations.DocumentSaveActions.SaveForLater:
					SaveForLater(content);
					break;
				case Enumerations.DocumentSaveActions.SaveAndPublish:
					SaveAndPublish(content);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected void btnAdd_OnClick(object sender, EventArgs e)
		{
			Response.Redirect(string.Concat(ComponentPages.OpqAddNode, "?nodeId", _nodeId));
		}

		protected void gvFiles_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var opqFileId = (int)gvFiles.DataKeys[e.Row.RowIndex].Value;
				if (opqFileId <= 0) return;
				var bFile = new BFile(_context);
				var opqFile = bFile.GetFile(opqFileId, true);
				var ltFile = (Literal)e.Row.FindControl("ltFile");
				var ltFileDate = (Literal)e.Row.FindControl("ltFileDate");
				if (ltFile != null)
				{
					if (opqFile.BaseFile != null)
					{
						const string tag = "<a href=\"{0}\">{1}</a>";
						string link = string.Concat(Utility.Business.Globals.FilesUrl, opqFile.BaseFile.Name);
						string fileName = opqFile.BaseFile.Description.IsNotNullOrEmpty()
											?
												opqFile.BaseFile.Description.Substring(opqFile.BaseFile.Description.LastIndexOf("/") + 1)
											:
												opqFile.BaseFile.Name.Substring(opqFile.BaseFile.Name.LastIndexOf("/") + 1);
						ltFile.Text = string.Format(tag, link, fileName);
					}
				}
				if (ltFileDate != null)
				{
					ltFileDate.Text = opqFile.CreatedDate.ToShortDateString();
				}
			}
		}

		protected void gvFiles_Editing(object sender, GridViewEditEventArgs e)
		{
		}

		protected void gvFiles_Deleting(object sender, GridViewDeleteEventArgs e)
		{
		}

		protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
		{
		}

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e)
		{
		}

		protected void btnUploadFile_Click(object sender, EventArgs e)
		{
			if (UploadFile(_nodeId, Configuration.DocumentCentralRootUrl, uplFile, FileCategories.SystemRoutineDocuments, null))
			{
				ShowPositiveFeedBack("UploadSuccess");
				PopulateFiles(_nodeId);
			}

		}


		#endregion

		#region Page Actions

		private void SaveAndPublish(string content)
		{
			// Todo: Validation
			if (_nodeId <= 0) return;
			var bDocument = new BDocument(_context);
			Document document = DocumentId > 0 ?
				bDocument.ChangeDocument(DocumentId, content) :
				bDocument.CreateDocument(_nodeId, null, DocumentTypes.Routine, content);
			if (document != null)
			{
				bDocument.Publish(document.Id);
				bDocument.UnLock(document.Id);
			}
			var bNode = new BNode(_context);
			var node = bNode.GetNode(_nodeId, true);
			bNode.ChangeNode(_nodeId, node.Parent, txtName.Text);
			SessionContext.UserPositiveFeedBackResource = node.IsMenu ? "SuccessSaveMenu" : "SuccessSaveRoutine";
			Response.Redirect(string.Format(ComponentPages.OpqStartQueryNode,_nodeId));
		}

		private void SaveForLater(string content)
		{
			// Todo: Validation
			if (_nodeId <= 0) return;
			var bDocument = new BDocument(_context);
			Document document = DocumentId > 0 ?
				bDocument.ChangeDocument(DocumentId, content) :
				bDocument.CreateDocument(_nodeId, null, DocumentTypes.Routine, content);
			if (document != null)
			{
				bDocument.UnPublish(document.Id);
				bDocument.Lock(document.Id);
			}
			var bNode = new BNode(_context);
			var node = bNode.GetNode(_nodeId, true);
			bNode.ChangeNode(_nodeId, node.Parent, txtName.Text);
			SessionContext.UserPositiveFeedBackResource = node.IsMenu ? "SuccessSaveMenu" : "SuccessSaveRoutine";
			Response.Redirect(string.Format(ComponentPages.OpqStartQueryNode, _nodeId));
		}

		private bool UploadFile(int nodeId, string virtualUploadDir, FileUpload uploadControl, FileCategories category, int? shopId)
		{
			try
			{
				if (!uploadControl.HasFile)
				{
					ShowNegativeFeedBack("NoFileException");
					return false;
				}
				string uploadDir = Server.MapPath(virtualUploadDir);
				if (!Directory.Exists(uploadDir))
				{
					Directory.CreateDirectory(uploadDir);
				}
				string fileName = string.Concat(
					OpqUtility.EncodeStringToUrl(Path.GetFileNameWithoutExtension(uploadControl.FileName)),
					Path.GetExtension(uploadControl.FileName));
				string fileDescription = uploadControl.FileName;
				if (!OpqUtility.IsAllowedExtension(uploadControl.FileName))
				{
					ShowNegativeFeedBack("ExtensionException", Configuration.UploadAllowedExtensions);
					return false;
				}
				int fileSize = uploadControl.PostedFile.ContentLength;
				if (fileSize > Configuration.UploadMaxFileSize)
				{
					ShowNegativeFeedBack("FileSizeException", Configuration.UploadMaxFileSize.ToString());
					return false;
				}

				//nameUrl is stored from root of fileurl path
				string nameUrl = Path.Combine(virtualUploadDir, fileName).Replace(Utility.Business.Globals.FilesUrl, string.Empty);
				string extensionInfo = Path.GetExtension(uploadControl.FileName).ToLower().Replace(".", string.Empty);

				var dFile = new Base.Data.File
						(Configuration.GetConfiguration(_context).ConnectionString);

				// Save file to BaseFile
				int baseFileId = dFile.AddFile(nameUrl,
							 false,
							 extensionInfo,
							 null,
							 fileDescription,
							 _context.UserName);
				// Add id to filename
				fileName = string.Concat(
					OpqUtility.EncodeStringToUrl(Path.GetFileNameWithoutExtension(uploadControl.FileName)),
					"-",
					baseFileId,
					Path.GetExtension(uploadControl.FileName));
				nameUrl = Path.Combine(virtualUploadDir, fileName).Replace(Utility.Business.Globals.FilesUrl, string.Empty);

				//Update with new id
				dFile.UpdateFile(baseFileId, nameUrl, _context.UserName);

				// Save file to disk
				uploadControl.PostedFile.SaveAs(Path.Combine(uploadDir, fileName));

				// Save file to opq
				var bFile = new BFile(_context);
				var opqFile = bFile.CreateFile(_nodeId, shopId, null, baseFileId, category);
				if (opqFile != null)
				{
					bFile.Publish(opqFile.Id);
					bFile.Unlock(opqFile.Id);
					return true;
				}
			}
			catch (Exception ex)
			{
				LogException(ex);
				ShowNegativeFeedBack("UnexpectedUploadException");
			}
			return false;
		}



		#endregion

		#region Properties

		public int DocumentId
		{
			get
			{
				object o = ViewState["OpqDocumentId"];
				if (o == null)
					return -1;
				return Convert.ToInt32(o);
			}
			set { ViewState["OpqDocumentId"] = value; }
		}

		#endregion

	}
}
