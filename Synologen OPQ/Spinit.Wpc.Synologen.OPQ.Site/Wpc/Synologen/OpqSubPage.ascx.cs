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
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Site.Code;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen
{
	public partial class OpqSubPage : OpqControlPage
	{
		private int _nodeId = -1;
		private string _fileCssClass = "new-window";
		const string Filetag = "<a href=\"{0}\" class=\"{1}\">{2}</a>";

		protected void Page_Load(object sender, EventArgs e)
		{
			int.TryParse(Request.QueryString["nodeId"], out _nodeId);
			SetupLayout();
			try
			{
				if (!Page.IsPostBack)
				{
					Node node = GetCurrentNode();
					if (node != null)
					{
						PopulateParent(node);
						PopulateCentralRoutine(node);
						PopulateShopRoutine(node, MemberShopId);
						PopulateCentralDocuments(node);
						PopulateShopRoutineDocuments(node, MemberShopId);
						PopulateShopDocuments(node, MemberShopId);
					}
				}
			}
			catch (BaseCodeException ex)
			{				
				ExceptionHandler.HandleException(ex, userMessageManager);
			}
		}

		private void SetupLayout()
		{
			if (!IsInSynologenRole(SynologenRoles.Roles.OpqShopAdmin))
			{
				phEditShopRoutine.Visible = false;
				phEditShopRoutineDocument.Visible = false;
				phEditShopDocument.Visible = false;
			}
		}

		private void PopulateParent(Node node)
		{
			if (node == null) return;
			if (node.Parent == null) return;
			var bNode = new BNode(_context);
			var parentNode = bNode.GetNode((int) node.Parent, false);
			ltParent.Text = parentNode.Name;
		}

		private void PopulateShopRoutineDocuments(Node node, int shopId)
		{
			if (node == null) return;
			if (shopId <= 0) return;
			var bFile = new BFile(_context);
			var files = bFile.GetFiles(node.Id, shopId, null, FileCategories.ShopRoutineDocuments, true, true, true);
			rptShopRoutineDocuments.DataSource = files;
			rptShopRoutineDocuments.DataBind();
		}

		private void PopulateShopDocuments(Node node, int shopId)
		{
			if (node == null) return;
			if (shopId <= 0) return;
			var bFile = new BFile(_context);
			var files = bFile.GetFiles(node.Id, shopId, null, FileCategories.ShopDocuments, true, true, true);
			rptShopDocuments.DataSource = files;
			rptShopDocuments.DataBind();
		}

		private void PopulateCentralDocuments(Node node)
		{
			if (node == null) return;
			var bFile = new BFile(_context);
			var files = bFile.GetFiles(node.Id, null, null, FileCategories.SystemRoutineDocuments, true, true, true);
			rptCentralDocuments.DataSource = files;
			rptCentralDocuments.DataBind();
		}

		private void PopulateShopRoutine(Node node, int shopId)
		{
			if (node == null) return;
			if (shopId <= 0) return;
			var bDocument = new BDocument(_context);
			var documents =
				bDocument.GetActiveDocuments(node.Id, shopId, null, DocumentTypes.Routine, null, false);
			if (documents.Count == 0) return;
			Document document = documents[0];
			ltShopRoutine.Text = document.DocumentContent;
		}

		private void PopulateCentralRoutine(Node node)
		{
			if (node == null) return;
			var bDocument = new BDocument(_context);
			var documents = 
				bDocument.GetActiveDocuments(node.Id, null, null, DocumentTypes.Routine, null, false);
			if (documents.Count == 0) return;
			Document document = documents[0];
			ltCentralRoutine.Text = document.DocumentContent;
			DateTime latestTouchedDate = document.CreatedDate;
			string latestTouchedBy = document.CreatedByName;
			if (document.ChangedDate != null)
			{
				latestTouchedDate = (DateTime) document.ChangedDate;
				latestTouchedBy = document.ChangedByName;
			}
			ltCentralRoutineChangedDate.Text = latestTouchedDate.ToShortDateString();
			ltCentralRoutineChangedBy.Text = latestTouchedBy;
		}

		private Node GetCurrentNode()
		{
			Node node = null;
			var bNode = new BNode(_context);
			int nodeId = 0;
			int.TryParse(Request.QueryString["nodeId"], out nodeId);
			if (nodeId > 0)
			{
				node = bNode.GetNode(nodeId, false);
			}
			return node;
		}


		#region Control Events

		protected void rptCentralDocuments_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var documentFile = (File) e.Item.DataItem;
				var ltCentralDocumentDate = (Literal) e.Item.FindControl("ltCentralDocumentDate");
				var ltCentralDocument = (Literal) e.Item.FindControl("ltCentralDocument");
				if (documentFile == null) return;
				if (ltCentralDocumentDate != null)
				{
					ltCentralDocumentDate.Text = documentFile.CreatedDate.ToShortDateString();
				}
				if (ltCentralDocument != null)
				{
					if (documentFile.BaseFile != null)
					{
						string link = string.Concat(Utility.Business.Globals.FilesUrl, documentFile.BaseFile.Name);
						string fileName = documentFile.BaseFile.Description.IsNotNullOrEmpty()
						                  	?
						                  		documentFile.BaseFile.Description.Substring(documentFile.BaseFile.Description.LastIndexOf("/") + 1)
						                  	:
						                  		documentFile.BaseFile.Name.Substring(documentFile.BaseFile.Name.LastIndexOf("/") + 1);
						ltCentralDocument.Text = string.Format(Filetag, link, FileCssClass, fileName);
					}
				}
			}
		}

		protected void rptShopRoutineDocuments_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var documentFile = (File)e.Item.DataItem;
				var ltShopRoutineDocumentDate = (Literal)e.Item.FindControl("ltShopRoutineDocumentDate");
				var ltShopRoutineDocument = (Literal)e.Item.FindControl("ltShopRoutineDocument");
				if (documentFile == null) return;
				if (ltShopRoutineDocumentDate != null)
				{
					ltShopRoutineDocumentDate.Text = documentFile.CreatedDate.ToShortDateString();
				}
				if (ltShopRoutineDocument != null)
				{
					if (documentFile.BaseFile != null)
					{
						string link = string.Concat(Utility.Business.Globals.FilesUrl, documentFile.BaseFile.Name);
						string fileName = documentFile.BaseFile.Description.IsNotNullOrEmpty()
						                  	?
						                  		documentFile.BaseFile.Description.Substring(documentFile.BaseFile.Description.LastIndexOf("/") + 1)
						                  	:
						                  		documentFile.BaseFile.Name.Substring(documentFile.BaseFile.Name.LastIndexOf("/") + 1);
						ltShopRoutineDocument.Text = string.Format(Filetag, link, FileCssClass, fileName);
					}
				}
			}
		}

		protected void rptShopDocuments_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var documentFile = (File)e.Item.DataItem;
				var ltShopDocumentDate = (Literal)e.Item.FindControl("ltShopDocumentDate");
				var ltShopDocument = (Literal)e.Item.FindControl("ltShopDocument");
				if (documentFile == null) return;
				if (ltShopDocumentDate != null)
				{
					ltShopDocumentDate.Text = documentFile.CreatedDate.ToShortDateString();
				}
				if (ltShopDocument != null)
				{
					if (documentFile.BaseFile != null)
					{
						string link = string.Concat(Utility.Business.Globals.FilesUrl, documentFile.BaseFile.Name);
						string fileName = documentFile.BaseFile.Description.IsNotNullOrEmpty()
											?
												documentFile.BaseFile.Description.Substring(documentFile.BaseFile.Description.LastIndexOf("/") + 1)
											:
												documentFile.BaseFile.Name.Substring(documentFile.BaseFile.Name.LastIndexOf("/") + 1);
						ltShopDocument.Text = string.Format(Filetag, link, FileCssClass, fileName);
					}
				}
			}
		}

		protected void btnSend_Click(object sender, EventArgs e)
		{
			if (_nodeId <= 0) return;
			if (txtImprovements.Text.IsNullOrEmpty())
			{
				ShowNegativeFeedBack(userMessageManager, "Improvement_Empty");
				return;
			}
			var bDocument = new BDocument(_context);

			var document = (MemberShopId > 0)
			               	?
			               		bDocument.CreateDocument(_nodeId, MemberShopId, DocumentTypes.Proposal, txtImprovements.Text)
			               	:
			               		bDocument.CreateDocument(_nodeId, null, DocumentTypes.Proposal, txtImprovements.Text);
			if (document != null)
			{
				bDocument.Publish(document.Id);
				bDocument.UnLock(document.Id);
				ShowPositiveFeedBack(userMessageManager, "Feedback_Improvement_Posted");
				txtImprovements.Text = string.Empty;
			}
		}

		#endregion

		#region Properties

		public string AdminPageUrl { get; set; }

		protected int NodeId
		{
			get { return _nodeId; }
			set { _nodeId = value; }
		}

		public string FileCssClass
		{
			get { return _fileCssClass; }
			set { _fileCssClass = value; }
		}

		#endregion

	}
}