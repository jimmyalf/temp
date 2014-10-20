namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class PendingPayment : Entity
	{
		public virtual decimal TaxedAmount { get; set; }
		public virtual decimal TaxFreeAmount { get; set; }
	}
}