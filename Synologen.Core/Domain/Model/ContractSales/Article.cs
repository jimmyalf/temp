namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class Article : Entity
	{
		public virtual string Name { get; set; }
		public virtual string Number { get; set; }
		public virtual string SPCSAccountNumber { get; set; }
	}
}