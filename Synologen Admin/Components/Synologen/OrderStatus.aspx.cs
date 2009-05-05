using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Member.Business;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen {
	public partial class OrderStatus : SynologenPage {
		private int _orderStatusId = -1;

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.Params["id"] != null)
				_orderStatusId = Convert.ToInt32(Request.Params["id"]);
			if (Page.IsPostBack) return;
			PopulateStatus();
			if (_orderStatusId > 0)
				SetupForEdit();
		}

		private void SetupForEdit() {
			ltHeading.Text = "Redigera status";
			btnSave.Text = "Ändra";
			OrderStatusRow orderStatus = Provider.GetOrderStatusRow(_orderStatusId);
			txtName.Text = orderStatus.Name;
			txtOrderNumber.Text = orderStatus.OrderNumber.ToString();
		}

		private void PopulateStatus() {
			gvStatus.DataSource = Provider.GetOrderStatuses(0);
			gvStatus.DataBind();
			ltHeading.Text = "Lägg till status";
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
			cc.AddConfirmation(ref sender, "Vill du verkligen ta bort vald status?");
		}

		protected void gvStatus_Editing(object sender, GridViewEditEventArgs e) {
			int index = e.NewEditIndex;

			int id = (int)gvStatus.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Edit)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Response.Redirect(ComponentPages.OrderStatus + "?id=" + id, true);
			}
		}

		protected void gvStatus_Deleting(object sender, GridViewDeleteEventArgs e) {
			int index = e.RowIndex;
			int id = (int)gvStatus.DataKeys[index].Value;
			if (!IsInRole(MemberRoles.Roles.Delete)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				if (Provider.OrderStatusHasConnectedOrders(id)){
					DisplayMessage("Status kan inte raderas då det finns kopplade ordrar.", true);
					return;
				}
				OrderStatusRow orderStatus = new OrderStatusRow();
				orderStatus.Id = id;
				Provider.AddUpdateDeleteOrderStatus(Enumerations.Action.Delete, ref orderStatus);
				Response.Redirect(ComponentPages.OrderStatus);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e) {
			OrderStatusRow orderStatus = new OrderStatusRow();
			Enumerations.Action action = Enumerations.Action.Create;
			if (_orderStatusId > 0) {
				orderStatus = Provider.GetOrderStatusRow(_orderStatusId);
				action = Enumerations.Action.Update;
			}
			orderStatus.Name = txtName.Text;
			orderStatus.OrderNumber = Int32.Parse(txtOrderNumber.Text);

			if (!IsInRole(MemberRoles.Roles.Create)) {
				Response.Redirect(ComponentPages.NoAccess);
			}
			else {
				Provider.AddUpdateDeleteOrderStatus(action, ref orderStatus);
				Response.Redirect(ComponentPages.OrderStatus);
			}
		}


		#endregion
	}
}
