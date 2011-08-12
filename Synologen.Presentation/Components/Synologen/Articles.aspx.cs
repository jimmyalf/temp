using System;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Spinit.Wpc.Core.UI;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Application;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen 
{
	public partial class Articles : SynologenPage 
	{
		private readonly IArticleRepository _articleRepository;

		public Articles()
		{
			_articleRepository = ServiceLocator.Current.GetInstance<IArticleRepository>();
		}

		protected void Page_Load(object sender, EventArgs e) 
		{
			if (Page.IsPostBack) return;
			PopulateArticles();
		}

		protected override void OnInit(EventArgs e) 
		{
			pager.IndexChanged += RepopulateArticles;
			pager.IndexButtonChanged += RepopulateArticles;
			pager.PageSizeChanged += RepopulateArticles;
			base.OnInit(e);
		}

		private void RepopulateArticles(object sender, EventArgs e)
		{
			SessionContext.ContractSalesArticles.PageIndex = pager.PageIndex;
			SessionContext.ContractSalesArticles.PageSize = pager.PageSize;
			PopulateArticles();
		}

		private void PopulateArticles()
		{
			var pageSize = SessionContext.ContractSalesArticles.PageSize;
			var pageIndex = SessionContext.ContractSalesArticles.PageIndex;
			var criteria = new AllArticlesMatchingCriteria
			{
				OrderBy = null, 
				Page = pageIndex + 1, 
				PageSize = pageSize, 
				SortAscending = true
			};
			var items = _articleRepository.FindBy(criteria) as IExtendedEnumerable<Article>;
			gvArticles.DataSource = items;
			gvArticles.DataBind();

			pager.PageSize = pageSize;
			pager.TotalRecords = (int) items.TotalCount;
			pager.TotalPages = pager.CalculateTotalPages();
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
				var article = new Business.Domain.Entities.Article {Id = articleId};
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
