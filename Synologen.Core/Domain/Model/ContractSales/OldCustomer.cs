namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class OldCustomer : Entity
	{
		public virtual Shop Shop { get; set; }
		public virtual string FirstName { get; set;}
		public virtual string LastName { get; set; }
	}
}