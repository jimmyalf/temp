using System;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Utility;
using Spinit.Wpc.Synologen.Presentation.Code;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen 
{
	public partial class ViewSettlementDetails : SynologenPage 
	{
		private int _settlementId;
		private float _totalValueIncludingVAT;
		private float _totalValueExcludingVAT;


		protected void Page_Load(object sender, EventArgs e) 
		{
			if (Request.Params["settlementId"] != null)
			{
				_settlementId = Convert.ToInt32(Request.Params["settlementId"]);
				Settlement = Provider.GetSettlement(_settlementId);
			}
			if (Page.IsPostBack) return;
			PopulateContractCustomerArticles();
		}

		private void PopulateContractCustomerArticles() 
		{
			var customerArticles = Provider.GetSettlementDetailsDataSet(_settlementId, out _totalValueIncludingVAT, out _totalValueExcludingVAT, null);
			gvSettlementDetails.DataSource = customerArticles;
			gvSettlementDetails.DataBind();
		}

		public Settlement Settlement { get; private set; }
		public string GetSettlementPeriodNumber(DateTime dateTimeValue) { return General.GetSettlementPeriodNumber(dateTimeValue); }
		public float TotalValueExcludingVAT { get { return _totalValueExcludingVAT; } }
		public float TotalValueIncludingVAT { get { return _totalValueIncludingVAT; } }
	}
}
