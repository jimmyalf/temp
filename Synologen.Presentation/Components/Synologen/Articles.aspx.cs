using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Application;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class Articles : SynologenPage {
		private int _articleId = -1;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null)
				_articleId = Convert.ToInt32(Request.Params["id"]);
			if (Page.IsPostBack) return;
			PopulateArticles();
			if (_articleId > 0)
				SetupForEdit();
		}

		private void SetupForEdit() {
			ltHeading.Text = "Redigera artikel";
			btnSave.Text = "Ändra";
			var article = Provider.GetArticle(_articleId);
			txtName.Text = article.Name;
			txtDescription.Text = article.Description;
			txtArticleNumber.Text = article.Number;
		}

		private void PopulateArticles() {
			gvArticles.DataSource = Provider.GetAllArticles("cId");//, base.DefaultLanguageId);
			gvArticles.DataBind();
			ltHeading.Text = "Lägg till artikel";
			btnSave.Text = "Spara";
		}

		#region Category Events

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
			var cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort artikeln?");
		}

		protected void gvArticles_Editing(object sender, GridViewEditEventArgs e) {
			int index = e.NewEditIndex;

			int articleId = (int)gvArticles.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Response.Redirect(ComponentPages.Articles+"?id=" + articleId, true);
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

		protected void btnSave_Click(object sender, EventArgs e) {
			var article = new Article();
			var action = Enumerations.Action.Create;
			if (_articleId > 0) {
				article = Provider.GetArticle(_articleId);
				action = Enumerations.Action.Update;
			}
			article.Name = txtName.Text;
			article.Number = txtArticleNumber.Text;
			article.Description = txtDescription.Text;

			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Provider.AddUpdateDeleteArticle(action, ref article);
				Response.Redirect(ComponentPages.Articles);
			}
		}


		#endregion

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
