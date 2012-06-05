using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Exceptions;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Site.Code;
using File=Spinit.Wpc.Synologen.OPQ.Core.Entities.File;

namespace Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen
{
	public partial class OpqAdmin : OpqControlPage
	{
		protected global::Spinit.Wpc.Wysiwyg.WpcWysiwyg _wysiwyg;
		public int _nodeId;
		private Enumerations.AdminActions _action = Enumerations.AdminActions.NotSet;


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsInSynologenRole(SynologenRoles.Roles.OpqShopAdmin) || MemberShopId <= 0)
			{
				if (!IsInSynologenRole(SynologenRoles.Roles.OpqShopAdmin))
				{
					ShowNegativeFeedBack(userMessageManager, "NoShopAdminException");
				}
				else if (MemberShopId <= 0) 
				{
					ShowNegativeFeedBack(userMessageManager, "NoShopException");
				}
			}
			else
			{
				ReadParameters();
				SetupLayout();
				SetupWysiwyg();
				if (!Page.IsPostBack)
				{
					try
					{
						PopulateRoutineFiles (_nodeId, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
						PopulateRoutine (_nodeId, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
						PopulateFiles (_nodeId, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
					}
					catch (BaseCodeException ex)
					{
						ExceptionHandler.HandleException(ex, userMessageManager);
					}
				}
			}
		}

		private void SetupWysiwyg()
		{
			_wysiwyg.CommonFilePath = new [] { DocumentPath };
		}

		private void PopulateRoutineFiles(int nodeId, int? shopId, int? shopGroupId)
		{
			if (nodeId <= 0) return;
			if (shopId <= 0) return;
			var bFile = new BFile(_context);
			var files = bFile.GetFiles(nodeId, shopId, null, shopGroupId, FileCategories.ShopRoutineDocuments, true, true, true);
			rptFilesRoutine.DataSource = null;
			rptFilesRoutine.DataSource = files;
			rptFilesRoutine.DataBind();
		}

		private void PopulateFiles(int nodeId, int? shopId, int? shopGroupId)
		{
			if (nodeId <= 0) return;
			if (shopId <= 0) return;
			var bFile = new BFile(_context);
			var files = bFile.GetFiles(nodeId, shopId, null, shopGroupId, FileCategories.ShopDocuments, true, true, true);
			rptFiles.DataSource = null;
			rptFiles.DataSource = files;
			rptFiles.DataBind();
		}

		private void SetupLayout()
		{
			if (_nodeId <= 0) return;
			phEditRoutine.Visible = true;
			phEditDocuments.Visible = true;

			//switch (_action)
			//{
			//    case Enumerations.AdminActions.NotSet:
			//        break;
			//    case Enumerations.AdminActions.EditRoutine:
			//        ltAdminHeader.Text = "Redigera egen rutin";
			//        phEditRoutine.Visible = true;
			//        phEditDocuments.Visible = false;
			//        break;
			//    case Enumerations.AdminActions.EditFiles:
			//        ltAdminHeader.Text = "Redigera egna dokument";
			//        phEditDocuments.Visible = true;
			//        phEditRoutine.Visible = false;
			//        break;
			//    default:
			//        throw new ArgumentOutOfRangeException();
			//}
		}

		private void PopulateRoutine(int nodeId, int? shopId, int? shopGroupId)
		{
			if (nodeId <= 0) return;
			var bDocument = new BDocument(_context);
			var documents =
				bDocument.GetDocuments(nodeId, shopId, null, shopGroupId, DocumentTypes.Routine, null, true, false, false);
			if (documents.Count == 0) return;
			var document = documents[0];
			bDocument.Lock(document.Id);
			DocumentId = document.Id;
			_wysiwyg.Html = document.DocumentContent;
		}

		/// <summary>
		/// Read params from querystring
		/// </summary>
		private void ReadParameters()
		{
			int.TryParse(Request.QueryString["nodeId"], out _nodeId);
			try
			{
				_action = (Enumerations.AdminActions)Enum.Parse(typeof(Enumerations.AdminActions), Request.QueryString["action"]);
			}
			catch{}
		}

		#region Properties

		public string ReturnPageUrl { get; set; }

		public int DocumentId
		{
			get
			{
				object o = ViewState["OpqDocumentId"];
				if (o == null)
					return -1;
				else
					return Convert.ToInt32(o);
			}
			set { ViewState["OpqDocumentId"] = value; }
		}

		#endregion

		#region Control Events

		protected void btnSave_Click(object sender, EventArgs e)
		{
			var btnSave = (Button)sender;
			var typeOfSave = (Enumerations.DocumentSaveActions)Enum.Parse(typeof(Enumerations.DocumentSaveActions), btnSave.CommandName);
			string content = _wysiwyg.Html;
			switch (typeOfSave)
			{
				case Enumerations.DocumentSaveActions.SaveForLater:
					SaveForLater (content, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
					break;
				case Enumerations.DocumentSaveActions.SaveAndPublish:
					SaveAndPublish (content, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected void rptFilesRoutine_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var documentFile = (File)e.Item.DataItem;
				var ltFile = (Literal)e.Item.FindControl("ltFile");
				var ltFileDate = (Literal)e.Item.FindControl("ltFileDate");
				if (documentFile == null) return;
				if (ltFile != null)
				{
					if (documentFile.BaseFile != null)
					{
						const string tag = "<a href=\"{0}\">{1}</a>";
						string link = string.Concat(Utility.Business.Globals.FilesUrl, documentFile.BaseFile.Name);
						string fileName = documentFile.BaseFile.Description.IsNotNullOrEmpty()
											?
												documentFile.BaseFile.Description.Substring(documentFile.BaseFile.Description.LastIndexOf("/") + 1)
											:
												documentFile.BaseFile.Name.Substring(documentFile.BaseFile.Name.LastIndexOf("/") + 1);
						ltFile.Text = string.Format(tag, link, fileName);
					}
				}
				if (ltFileDate != null)
				{
					ltFileDate.Text = documentFile.CreatedDate.ToShortDateString();
				}
			}
		}

		protected void rptFilesRoutine_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				int fileId;
				var action = Enumerations.FileActions.NotSet;
				try
				{
					action = (Enumerations.FileActions)Enum.Parse(typeof(Enumerations.FileActions), e.CommandName);
				}
				catch { }
				int.TryParse(e.CommandArgument.ToString(), out fileId);
				if ((fileId <= 0) || (action == Enumerations.FileActions.NotSet)) return;
				switch (action)
				{
					case Enumerations.FileActions.Delete:
						DeleteFile(e.Item, fileId);
						break;
					case Enumerations.FileActions.MoveUp:
						MoveFileUp(e.Item, fileId);
						break;
					case Enumerations.FileActions.MoveDown:
						MoveFileDown(e.Item, fileId);
						break;
					default:
						throw new ArgumentOutOfRangeException();

				}
				PopulateRoutineFiles (_nodeId, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
			}
		}

		protected void btnUploadRoutine_Click(object sender, EventArgs e)
		{
			if (UploadFile (_nodeId, DocumentPath, uplFileRoutine, FileCategories.ShopRoutineDocuments, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId))
			{
				ShowPositiveFeedBack(userMessageManager, "UploadSuccess");
				PopulateRoutineFiles (_nodeId, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
			}
		}

		protected void rptFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var documentFile = (File)e.Item.DataItem;
				var ltFile = (Literal)e.Item.FindControl("ltFile");
				var ltFileDate = (Literal)e.Item.FindControl("ltFileDate");
				if (documentFile == null) return;
				if (ltFile != null)
				{
					if (documentFile.BaseFile != null)
					{
						const string tag = "<a href=\"{0}\">{1}</a>";
						string link = string.Concat(Utility.Business.Globals.FilesUrl, documentFile.BaseFile.Name);
						string fileName = documentFile.BaseFile.Description.IsNotNullOrEmpty()
											?
												documentFile.BaseFile.Description.Substring(documentFile.BaseFile.Description.LastIndexOf("/") + 1)
											:
												documentFile.BaseFile.Name.Substring(documentFile.BaseFile.Name.LastIndexOf("/") + 1);
						ltFile.Text = string.Format(tag, link, fileName);
					}
				}
				if (ltFileDate != null)
				{
					ltFileDate.Text = documentFile.CreatedDate.ToShortDateString();
				}
			}
		}

		protected void rptFiles_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				int fileId;
				var action = Enumerations.FileActions.NotSet;
				try
				{
					action = (Enumerations.FileActions)Enum.Parse(typeof(Enumerations.FileActions), e.CommandName);
				}
				catch { }
				int.TryParse(e.CommandArgument.ToString(), out fileId);
				if ((fileId <= 0) || (action == Enumerations.FileActions.NotSet)) return;
				switch (action)
				{
					case Enumerations.FileActions.Delete:
						DeleteFile(e.Item, fileId);
						break;
					case Enumerations.FileActions.MoveUp:
						MoveFileUp(e.Item, fileId);
						break;
					case Enumerations.FileActions.MoveDown:
						MoveFileDown(e.Item, fileId);
						break;
					default:
						throw new ArgumentOutOfRangeException();

				}
				PopulateFiles (_nodeId, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
			}
		}

		protected void btnUpload_Click(object sender, EventArgs e)
		{
			if (UploadFile (_nodeId, DocumentPath, uplFile, FileCategories.ShopDocuments, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId))
			{
				ShowPositiveFeedBack(userMessageManager, "UploadSuccess");
				PopulateFiles (_nodeId, MemberShopGroupId != null ? null : MemberShopId, MemberShopGroupId);
			}
		}


		#endregion

		#region Content Actions

		private void SaveAndPublish(string content, int? shopId, int? shopGroupId)
		{
			if ((_nodeId <= 0) || (shopId <= 0)) return;
			var bDocument = new BDocument(_context);
			Document document = DocumentId > 0 ? 
				bDocument.ChangeDocument(DocumentId, content) : 
				bDocument.CreateDocument(_nodeId, shopId, shopGroupId, DocumentTypes.Routine, content);
			if (document != null)
			{
				bDocument.Publish(document.Id);
				bDocument.UnLock(document.Id);
			}
		}

		private void SaveForLater(string content, int? shopId, int? shopGroupId)
		{
			if ((_nodeId <= 0) || (shopId <= 0)) return;
			var bDocument = new BDocument(_context);
			Document document = DocumentId > 0 ?
				bDocument.ChangeDocument(DocumentId, content) :
				bDocument.CreateDocument(_nodeId, shopId, shopGroupId, DocumentTypes.Routine, content);
			if (document != null)
			{
				bDocument.UnPublish(document.Id);
				bDocument.Lock(document.Id);
			}
		}

		#endregion

		#region FileActions

		private void MoveFileDown(RepeaterItem item, int fileId)
		{
			var bFile = new BFile(_context);
			if (fileId <= 0) return;
			bFile.MoveFile(FileMoveActions.MoveDown, fileId);
		}

		private void MoveFileUp(RepeaterItem item, int fileId)
		{
			var bFile = new BFile(_context);
			if (fileId <= 0) return;
			bFile.MoveFile(FileMoveActions.MoveUp, fileId);
		}

		private void DeleteFile(RepeaterItem item, int fileId)
		{
			var bFile = new BFile(_context);
			if (fileId <= 0) return;
			bFile.DeleteFile(fileId, true);
		}

		private bool UploadFile (int nodeId, string virtualUploadDir, FileUpload uploadControl, FileCategories category, int? shopId, int? shopGroupId)
		{
			try {
				if (!uploadControl.HasFile) {
					ShowNegativeFeedBack (userMessageManager, "NoFileException");
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
					ShowNegativeFeedBack (userMessageManager, "ExtensionException", Configuration.UploadAllowedExtensions);
					return false;
				}
				int fileSize = uploadControl.PostedFile.ContentLength;
				if (fileSize > Configuration.UploadMaxFileSize) {
					ShowNegativeFeedBack (userMessageManager, "FileSizeException", Configuration.UploadMaxFileSize.ToString ());
					return false;
				}

				//nameUrl is stored from root of fileurl path
				string nameUrl = Path.Combine (virtualUploadDir, fileName).Replace (Utility.Business.Globals.FilesUrl, string.Empty);
				string extensionInfo = Path.GetExtension (uploadControl.FileName).ToLower ().Replace (".", string.Empty);

				var dFile = new Base.Data.File (Configuration.GetConfiguration (_context).ConnectionString);

				// Save file to BaseFile
				int baseFileId = dFile.AddFile (
					nameUrl,
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
				nameUrl = Path.Combine (virtualUploadDir, fileName).Replace (Utility.Business.Globals.FilesUrl, string.Empty);

				//Update with new id
				dFile.UpdateFile (baseFileId, nameUrl, _context.UserName);

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
				ShowNegativeFeedBack (userMessageManager, "UnexpectedUploadException");
			}
			return false;
		}

		#endregion

	}
}