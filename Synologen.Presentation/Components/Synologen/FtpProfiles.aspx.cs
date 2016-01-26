using System;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Data.Queries.ContractSales;
using Spinit.Wpc.Synologen.Presentation.Application;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen 
{
	public partial class FtpProfiles : SynologenPage 
	{
		public FtpProfiles()
		{
		}

		protected void Page_Load(object sender, EventArgs e) 
		{
			if (Page.IsPostBack) return;
			PopulateFtpProfiles();
		}

		protected override void OnInit(EventArgs e) 
		{
			pager.IndexChanged += RepopulateFtpProfiles;
			pager.IndexButtonChanged += RepopulateFtpProfiles;
			pager.PageSizeChanged += RepopulateFtpProfiles;
			base.OnInit(e);
		}

		private void RepopulateFtpProfiles(object sender, EventArgs e)
		{
			SessionContext.ContractSalesArticles.PageIndex = pager.PageIndex;
			SessionContext.ContractSalesArticles.PageSize = pager.PageSize;
            PopulateFtpProfiles();
		}

		private void PopulateFtpProfiles()
		{
			var pageSize = SessionContext.ContractSalesArticles.PageSize;
			var pageIndex = SessionContext.ContractSalesArticles.PageIndex;
			var items = new ContractFtpProfileQuery(ConnectionString).Execute();
			gvFtpProfiles.DataSource = items;
            gvFtpProfiles.DataBind();
			pager.PageSize = pageSize;
			pager.TotalRecords = items.Count;
			pager.TotalPages = pager.CalculateTotalPages();

		}

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) {
			var cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort profilen?");
		}

		protected void gvFtpProfile_Editing(object sender, GridViewEditEventArgs e) {
			var index = e.NewEditIndex;

			var articleId = (int)gvFtpProfiles.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Edit)) 
			{
				Response.Redirect(ComponentPages.NoAccess);
			}
			else 
			{
				var url = RouteTable.Routes.GetRoute("ContractSales", "EditFtpProfile", new RouteValueDictionary {{"id", articleId}});
				Response.Redirect(url);
			}
		}

		protected void gvFtpProfile_Deleting(object sender, GridViewDeleteEventArgs e) {
			var index = e.RowIndex;
			var ftpProfileId = (int)gvFtpProfiles.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
                var ftpProfileInUse = new ContractFtpProfileConnectionsQuery(ftpProfileId, ConnectionString).Execute();
			    if (ftpProfileInUse)
			    {
			        DisplayMessage("Profilen kan inte raderas då den används.", true);
			        return;
			    }
			    new DeleteContractFtpProfileCommand(ftpProfileId, ConnectionString).Execute();
                Response.Redirect(ComponentPages.FtpProfiles);
            }
        }
	}
}
