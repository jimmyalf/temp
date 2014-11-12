using System;
using System.Data;
using Spinit.Wpc.Synologen.Business.Utility;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.ContractSales
{
	public partial class SettlementList : SynologenUserControl 
	{
		private string _shopNumber;

		protected void Page_Load(object sender, EventArgs e) {
			StorePageUrl();
			if(!Page.IsPostBack) {
				PopulateSettlements();
			}
		}

		private void StorePageUrl() 
		{
			SynologenSessionContext.SettlementListPage = Request.Url.PathAndQuery;
		}

		private void PopulateSettlements() 
		{
			if(MemberShopId<=0) return;
			rptSettlements.DataSource = Provider.GetSettlementsDataSet(0, (int) MemberShopId, "tblSynologenSettlement.cId DESC");
			rptSettlements.DataBind();
			_shopNumber = Provider.GetShop((int) MemberShopId).Number;
			rptSettlements.Visible = (rptSettlements.Items.Count > 0);
		}

		
		public string ShopNumber {
			get { return _shopNumber; }
			set { _shopNumber = value; }
		} 


		public static string GetSettlementPeriodNumber(DataRowView dataRowView) {
			string dateString = dataRowView["cCreatedDate"].ToString();
			DateTime date = Convert.ToDateTime(dateString);
			return General.GetSettlementPeriodNumber(date);
		}


	}
}