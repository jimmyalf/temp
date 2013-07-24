using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen 
{
	public partial class ContractCompanies : SynologenPage 
    {
		private int _contractId = -1;
		//private int _pageSize = -1;
		private Contract _selectedContract = new Contract();

		public Contract SelectedContract 
        {
			get { return _selectedContract; }
		}

		protected void Page_Load(object sender, EventArgs e) 
        {
			if (Request.Params["id"] != null) 
            {
				_contractId = Convert.ToInt32(Request.Params["id"]);
				_selectedContract = Provider.GetContract(_contractId);
			}
			plFilterByContract.Visible = (_contractId > 0);
			if (Page.IsPostBack) return;
			PopulateContractCompanies();
		}

        public string GetInvoiceMethodText(int invoiceMethodId)
        {
            return ((InvoicingMethod)invoiceMethodId).GetEnumDisplayName();
        }

		private void PopulateContractCompanies()
        {
			//Set sorting
			SortExpression = SessionContext.ContractCompanies.SortExpression;
			SortAscending = SessionContext.ContractCompanies.SortAscending;
			var companies = Provider.GetCompanies(0, _contractId, "cId", ActiveFilter.Both);

			gvContractCompanies.DataSource = companies;
			gvContractCompanies.DataBind();
			Code.Utility.SetActiveGridViewControl(gvContractCompanies,companies,"cActive","imgActive","Active", "Inactive");

		}

		#region Category Events

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e) 
        {
			ClientConfirmation cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort företaget?");
		}

		protected void gvContractCompanies_Editing(object sender, GridViewEditEventArgs e) 
        {
			var index = e.NewEditIndex;

			var contractCompanyId = Convert.ToInt32(gvContractCompanies.DataKeys[index].Value); 
			if (!IsInRole(MemberRoles.Roles.Edit)) 
            {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else 
            {
				if(SelectedContract.Id>0) 
                {
					Response.Redirect(ComponentPages.EditContractCompany +"?id=" + contractCompanyId + "&contractId=" + SelectedContract.Id, true);
				}
				Response.Redirect(ComponentPages.EditContractCompany +"?id=" + contractCompanyId, true);
				
			}
		}

		protected void gvContractCompanies_Deleting(object sender, GridViewDeleteEventArgs e) 
        {
			int index = e.RowIndex;
			int companyId = (int)gvContractCompanies.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else 
            {
				if (Provider.CompanyHasConnectedOrders(companyId)) {
					DisplayMessage("Företaget kan inte raderas då det finns kopplade ordrar.", true);
					return;
				}
				Company company = new Company();
				company.Id = companyId;
				Provider.AddUpdateDeleteCompany(Enumerations.Action.Delete, ref company);
				Response.Redirect(ComponentPages.ContractCompanies);
			}
		}

		protected void gvContractCompanies_Sorting(object sender, GridViewSortEventArgs e)
        {
			if (e.SortExpression == SortExpression) SortAscending = !SortAscending;
			else SortAscending = true;

			SortExpression = e.SortExpression;
			SessionContext.ContractCompanies.SortExpression = SortExpression;
			SessionContext.ContractCompanies.SortAscending = SortAscending;

			PopulateContractCompanies();
		}

		protected void gvContractCompanies_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
			SessionContext.ContractCompanies.PageIndex = e.NewPageIndex;
			gvContractCompanies.PageIndex = e.NewPageIndex;
			DataBind();
		}

		#endregion

	}

}
