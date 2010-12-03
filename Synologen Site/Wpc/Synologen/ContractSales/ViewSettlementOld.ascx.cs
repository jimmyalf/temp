using System;
using System.Data;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Presentation.Site.Code;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ContractSales
{
	public partial class ViewSettlementOld : SynologenUserControl 
	{
		private int _settlementId;
		private Settlement _settlement;
		private bool _allOrdersMarkedAsPayed;
		private float _totalValueExcludingVAT;
		private float _totalValueIncludingVAT;

		protected void Page_Load(object sender, EventArgs e) {
			ReadQueryParameters();
			PopulateSettlement();
			if (Page.IsPostBack) return;
			InitializeFormSettings();
			PopulateSettlementOrderItems();
		}

		private void InitializeFormSettings() {
			SynologenSessionContext.UseDetailedSettlementView = false;
			plDetailedView.Visible = SynologenSessionContext.UseDetailedSettlementView;
			plSimpleView.Visible = !plDetailedView.Visible;
		}

		private void PopulateSettlement() {
			_settlement = _settlementId > 0 ? Provider.GetSettlement(_settlementId) : new Settlement();
		}

		private void ReadQueryParameters() {
			if (Request.Params["settlementId"] != null) {
				_settlementId = Convert.ToInt32(Request.Params["settlementId"]);
			}
		}

		private void PopulateSettlementOrderItems() {
			if (MemberShopId <= 0) return;
			if (_settlementId <= 0) return;
			if(SynologenSessionContext.UseDetailedSettlementView) {
				rptSettlementOrderItemsDetailed.DataSource = GetDetailedOrderItems();
				rptSettlementOrderItemsDetailed.DataBind();
				rptSettlementOrderItemsDetailed.Visible = (rptSettlementOrderItemsDetailed.Items.Count > 0);
				btnMarkAsPayed.Enabled = !_allOrdersMarkedAsPayed;
			}
			else {
				rptSettlementOrderItemsSimple.DataSource = GetSimpleOrderItems();
				rptSettlementOrderItemsSimple.DataBind();
				rptSettlementOrderItemsSimple.Visible = (rptSettlementOrderItemsSimple.Items.Count > 0);
				btnMarkAsPayed.Enabled = !_allOrdersMarkedAsPayed;
			}
		}

		private DataSet GetSimpleOrderItems() {
			//float vatAmount = Globals.SettlementVATAmount;
			DataSet data = Provider.GetSettlementsOrderItemsDataSetSimple(MemberShopId, _settlementId, null, out _allOrdersMarkedAsPayed, out _totalValueIncludingVAT, out _totalValueExcludingVAT);
			return data;
		}

		private DataSet GetDetailedOrderItems() {
			//float vatAmount = Globals.SettlementVATAmount;
			DataSet data = Provider.GetSettlementsOrderItemsDataSetDetailed(MemberShopId,_settlementId, null, out _allOrdersMarkedAsPayed, out _totalValueIncludingVAT, out _totalValueExcludingVAT);
			return data;
		}

		protected void btnSwitchView_Click(object sender, EventArgs e) {
			SynologenSessionContext.UseDetailedSettlementView = !SynologenSessionContext.UseDetailedSettlementView;
			plDetailedView.Visible = SynologenSessionContext.UseDetailedSettlementView;
			plSimpleView.Visible = !plDetailedView.Visible;
			ChangeSwitchButtonText(plSimpleView.Visible);
			PopulateSettlementOrderItems();
		}

		private void ChangeSwitchButtonText(bool useDetailedText) {
			btnSwitchView.Text = useDetailedText ? "Visa detaljer" : "Visa enkelt";
		}

		protected void btnMarkAsPayed_Click(object sender, EventArgs e) {
			if(MemberShopId<=0) return;
			Provider.MarkOrdersInSettlementAsPayedPerShop(_settlementId, MemberShopId);
		}

		public Settlement Settlement {
			get { return _settlement; }
		}

		public string SettlementPeriodNumber {
			get {
				bool settlementHasDate = (_settlement.CreatedDate != DateTime.MinValue);
				return (settlementHasDate) ? Business.Utility.General.GetSettlementPeriodNumber(_settlement.CreatedDate) : string.Empty;
			}
		}

		public bool AllOrdersMarkedAsPayed {
			get { return _allOrdersMarkedAsPayed; }
		}

		public float TotalValueExcludingVAT {
			get { return _totalValueExcludingVAT; }
		}

		public float TotalValueIncludingVAT {
			get { return _totalValueIncludingVAT; }
		}

	}
}