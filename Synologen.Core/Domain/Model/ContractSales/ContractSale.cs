namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class ContractSale
	{
		public virtual int Id { get; private set; }
		public virtual Shop Shop { get; private set; }
		public virtual decimal TotalAmountIncludingVAT { get; private set; }
		public virtual decimal TotalAmountExcludingVAT { get; private set; }
	}
}