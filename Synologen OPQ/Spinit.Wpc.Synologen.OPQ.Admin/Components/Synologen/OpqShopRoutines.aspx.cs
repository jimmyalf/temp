using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Presentation;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen
{
	public partial class OpqShopRoutines : OpqPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (_nodeId <= 0)
				{
					ShowNegativeFeedBack("NoNodeException");
				}
				else
				{
					PopulateShopRoutines(_nodeId);					
				}
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
			itemCollection.AddItem("Improvments", null, "Lista förbättringsförslag", "Visar alla inkomna förbättringsåtgärder", null, "btnImprovments_OnClick", false, null);
			itemCollection.AddItem("CentralRoutine", null, "Visa central rutin", "Visar central rutin för vald nod", null, "btnShowCentralRoutine_OnClick", false, null);

			subMenu.MenuItems = itemCollection;

			m.SynologenSmartMenu.Render(subMenu, _phSynologenSubMenu);
		}


		private void PopulateShopRoutines(int nodeId)
		{
			if (nodeId <= 0) return;
			var bDocument = new BDocument(_context);
			var documents = bDocument.GetShopDocuments(nodeId, true, false, true);
			rptShops.DataSource = null;
			rptShops.DataSource = documents;
			rptShops.DataBind();
		}

		#region Page Events

		protected void btnShowCentralRoutine_OnClick(object sender, EventArgs e)
		{
			Response.Redirect(string.Format(ComponentPages.OpqStartQueryNode, _nodeId));
		}

		protected void btnImprovments_OnClick(object sender, EventArgs e)
		{
			Response.Redirect(ComponentPages.OpqImprovments);
		}


		protected void gvFiles_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var opqFile = (File) e.Row.DataItem;
				if (opqFile == null) return;
				//var opqFileId = (int)gvFiles.DataKeys[e.Row.RowIndex].Value;
				//if (opqFileId <= 0) return;
				//var bFile = new BFile(_context);
				//var opqFile = bFile.GetFile(opqFileId, true);
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

		protected void rptShops_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var document = (Document) e.Item.DataItem;
				var shopId = (int) document.ShpId;
				var ltShopName = (Literal) e.Item.FindControl("ltShopName");
				var ltRoutine = (Literal) e.Item.FindControl("ltRoutine");
				var gvFiles = (GridView) e.Item.FindControl("gvFiles");
				var hlShopLink = (HyperLink) e.Item.FindControl("hlShopLink");
				if (hlShopLink != null)
				{
					hlShopLink.NavigateUrl =
						string.Format(ComponentPages.OpqStartQueryNodeAndShop, _nodeId, shopId);
				}
				if (ltShopName != null)
				{
					ltShopName.Text = document.Shop.ShopName;
				}
				if ((ltRoutine != null) && (document.DocumentContent.IsNotNullOrEmpty()))
				{
					ltRoutine.Text = document.DocumentContent;
				}
				var bFile = new BFile(_context);
				var shopFiles = bFile.GetFiles(_nodeId, shopId, null, FileCategories.ShopRoutineDocuments, true, true, false);
				if ((gvFiles != null) && (shopFiles != null) && (shopFiles.Count > 0))
				{
					gvFiles.DataSource = null;
					gvFiles.DataSource = shopFiles;
					gvFiles.DataBind();
				}
			}
		}

		#endregion
	}
}
