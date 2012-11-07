using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ShopSettlement
	{
		public virtual int Id { get; set; }
		public virtual Shop Shop { get; set; }
		public virtual IEnumerable<SaleItem> SaleItems { get; set;}
		public virtual decimal ContractSalesValueIncludingVAT { get; set; }
		public virtual decimal OldTransactionValueIncludingVAT { get; set; }
		//public virtual decimal NewTransactionValueIncludingVAT { get; set; }
		//public virtual decimal NewTransactionTaxedValue 
		//{ 
		//    get { return NewTransactions == null ? 0 : NewTransactions.Where(x => x.PendingPayment != null).Sum(x => x.PendingPayment.TaxedAmount); }
		//}
		//public virtual decimal NewTransactionTaxFreeValue 
		//{ 
		//    get { return NewTransactions == null ? 0 : NewTransactions.Where(x => x.PendingPayment != null).Sum(x => x.PendingPayment.TaxFreeAmount); }
		//}
		public virtual SubscriptionAmount GetNewTransactionsValue()
		{
			return NewTransactions == null ? new SubscriptionAmount() : NewTransactions.Select(x => x.GetAmount()).Sum();
		}

		public virtual IEnumerable<OldTransaction> OldTransactions { get; set; }
		public virtual IEnumerable<NewTransaction> NewTransactions { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public bool AllContractSalesHaveBeenMarkedAsPayed { get; set; }
	}
}