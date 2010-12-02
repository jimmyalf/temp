namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class SaleItem : Entity
	{
		public virtual ContractSale Sale { get; set; }
		public virtual Article Article { get; set; }
		public virtual int Quantity { get; set; }
		public virtual decimal SingleItemPriceExcludingVAT { get; set; }
		public virtual bool IsVATFree { get; set; }
		public virtual decimal TotalPriceExcludingVAT() 
		{
			return SingleItemPriceExcludingVAT* Quantity;
		}
	}
}