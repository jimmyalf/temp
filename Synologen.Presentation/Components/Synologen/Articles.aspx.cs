using System;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Application;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen 
{
	public partial class Articles : SynologenPage 
	{
		protected void Page_Load(object sender, EventArgs e) 
		{
			if (Page.IsPostBack) return;
			PopulateArticles();
		}

		private void PopulateArticles() 
		{
			gvArticles.DataSource = Provider.GetAllArticles("cId");
			gvArticles.DataBind();
		}

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
			var cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort artikeln?");
		}

		protected void gvArticles_Editing(object sender, GridViewEditEventArgs e) {
			var index = e.NewEditIndex;

			var articleId = (int)gvArticles.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Edit)) 
			{
				Response.Redirect(ComponentPages.NoAccess);
			}
			else 
			{
				var url = RouteTable.Routes.GetRoute("ContractSales", "EditArticle", new RouteValueDictionary {{"id", articleId}});
				Response.Redirect(url);
			}
		}

		protected void gvArticles_Deleting(object sender, GridViewDeleteEventArgs e) {
			var index = e.RowIndex;
			var articleId = (int)gvArticles.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				if(Provider.ArticleHasConnectedContracts(articleId)) {
					DisplayMessage("Artikeln kan inte raderas då det finns kopplade avtal.", true);
					return;
				}
				if(Provider.ArticleHasConnectedOrders(articleId)) {
					DisplayMessage("Artikeln kan inte raderas då det finns kopplade ordrar.", true);
					return;
				}
				var article = new Article {Id = articleId};
				Provider.AddUpdateDeleteArticle(Enumerations.Action.Delete, ref article);
				Response.Redirect(ComponentPages.Articles);
			}
		}

		protected override SmartMenu.ItemCollection InitializeSubMenu()
		{
			var itemCollection = new SmartMenu.ItemCollection();
			itemCollection.AddItem(
				"new-article" /*id*/, 
				null /*staticId*/, 
				"Ny" /*text*/, 
				"Skapa ny artikel" /*tooltop*/,
				null /*cssClass*/, 
				RouteTable.Routes.GetRoute("ContractSales", "AddArticle") /*navigateUrl*/,
				null /*rel*/, 
				null /*urlAliasCollection*/, 
				false /*selected*/, 
				true /*enabled*/
			);
			return itemCollection;
		}
	}
}
