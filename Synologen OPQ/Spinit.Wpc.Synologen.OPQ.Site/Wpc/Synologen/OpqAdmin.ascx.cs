using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Exceptions;
using Spinit.Wpc.Synologen.Business;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Site.Code;

namespace Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen
{
	public partial class OpqAdmin : OpqControlPage
	{
		protected global::Spinit.Wpc.Wysiwyg.WpcWysiwyg _wysiwyg;
		private int _nodeId;
		private Enumerations.AdminActions _action = Enumerations.AdminActions.NotSet;


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsInSynologenRole(SynologenRoles.Roles.OpqShopAdmin) || MemberShopId <= 0)
			{
				if (!IsInSynologenRole(SynologenRoles.Roles.OpqShopAdmin))
				{
					userMessageManager.NegativeMessage = "Du måste vara butiksadminstratör på denna sida!";
				}
				else if (MemberShopId <= 0) 
				{
					userMessageManager.NegativeMessage = "Det finns ingen butik kopplad till din användarprofil!";					
				}
			}
			else
			{
				ReadParameters();
				SetupLayout();
				if (!Page.IsPostBack)
				{
					try
					{
						switch (_action)
						{
							case Enumerations.AdminActions.NotSet:
								break;
							case Enumerations.AdminActions.EditRoutine:
								PopulateRoutine(_nodeId, MemberShopId);
								break;
							case Enumerations.AdminActions.EditFiles:
								PopulateFiles(_nodeId, MemberShopId);
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
					catch (BaseCodeException ex)
					{
						ExceptionHandler.HandleException(ex, userMessageManager);
					}
				}
			}
		}

		private void PopulateFiles(int nodeId, int shopId)
		{
			if (nodeId <= 0) return;
			if (shopId <= 0) return;
			var bFile = new BFile(_context);
			var files = bFile.GetFiles(nodeId, shopId, null, null, true, true, true);
			rptFiles.DataSource = files;
			rptFiles.DataBind();
		}

		private void SetupLayout()
		{
			switch (_action)
			{
				case Enumerations.AdminActions.NotSet:
					break;
				case Enumerations.AdminActions.EditRoutine:
					phEditRoutine.Visible = true;
					phEditDocuments.Visible = false;
					break;
				case Enumerations.AdminActions.EditFiles:
					phEditDocuments.Visible = true;
					phEditRoutine.Visible = false;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void PopulateRoutine(int nodeId, int shopId)
		{
			if (nodeId <= 0) return;
			var bDocument = new BDocument(_context);
			var documents =
				bDocument.GetDocuments(nodeId, shopId, null, DocumentTypes.Routine, null, true, false, false);
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
					SaveForLater(content, MemberShopId);
					break;
				case Enumerations.DocumentSaveActions.SaveAndPublish:
					SaveAndPublish(content, MemberShopId);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#endregion

		#region Control Actions

		private void SaveAndPublish(string content, int shopId)
		{
			if ((_nodeId <= 0) || (shopId <= 0)) return;
			var bDocument = new BDocument(_context);
			Document document = DocumentId > 0 ? 
				bDocument.ChangeDocument(DocumentId, content) : 
				bDocument.CreateDocument(_nodeId, shopId, DocumentTypes.Routine, content);
			if (document != null)
			{
				bDocument.Publish(document.Id);
				bDocument.UnLock(document.Id);
			}
		}

		private void SaveForLater(string content, int shopId)
		{
			if ((_nodeId <= 0) || (shopId <= 0)) return;
			var bDocument = new BDocument(_context);
			Document document = DocumentId > 0 ?
				bDocument.ChangeDocument(DocumentId, content) :
				bDocument.CreateDocument(_nodeId, shopId, DocumentTypes.Routine, content);
			if (document != null)
			{
				bDocument.UnPublish(document.Id);
				bDocument.Lock(document.Id);
			}
		}

		#endregion

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
						string fileName = documentFile.BaseFile.Name.Substring(documentFile.BaseFile.Name.LastIndexOf("/") + 1);
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
				catch {}
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
			}

		}

		#region FileActions

		private void MoveFileDown(RepeaterItem item, int fileId)
		{
			var bFile = new BFile(_context);
			if (fileId <= 0) return;
			bFile.MoveFile(NodeMoveActions.MoveDown, fileId);
		}

		private void MoveFileUp(RepeaterItem item, int fileId)
		{
			var bFile = new BFile(_context);
			if (fileId <= 0) return;
			bFile.MoveFile(NodeMoveActions.MoveUp, fileId);
		}

		private void DeleteFile(RepeaterItem item, int fileId)
		{
			var bFile = new BFile(_context);
			if (fileId <= 0) return;
			bFile.DeleteFile(fileId, true);
		}

		#endregion
	}
}