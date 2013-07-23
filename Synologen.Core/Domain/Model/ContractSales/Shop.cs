namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
	public class Shop : Entity
	{
		public virtual string Name { get; private set; }
		public virtual string Number { get; private set; }
		public virtual string BankGiroNumber { get; private set; }
	}
}