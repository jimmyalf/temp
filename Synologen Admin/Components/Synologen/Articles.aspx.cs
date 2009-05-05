using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

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
			ArticleRow article = Provider.GetArticle(_articleId);
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
			ClientConfirmation cc = new ClientConfirmation();
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
			int index = e.RowIndex;
			int articleId = (int)gvArticles.DataKeys[index].Value;
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
				ArticleRow articleRow = new ArticleRow();
				articleRow.Id = articleId;
				Provider.AddUpdateDeleteArticle(Enumerations.Action.Delete, ref articleRow);
				Response.Redirect(ComponentPages.Articles);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			ArticleRow articleRow = new ArticleRow();
			Enumerations.Action action = Enumerations.Action.Create;
			if (_articleId > 0) {
				articleRow = Provider.GetArticle(_articleId);
				action = Enumerations.Action.Update;
			}
			articleRow.Name = txtName.Text;
			articleRow.Number = txtArticleNumber.Text;
			articleRow.Description = txtDescription.Text;

			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Provider.AddUpdateDeleteArticle(action, ref articleRow);
				Response.Redirect(ComponentPages.Articles);
			}
		}


		#endregion

	}
}
