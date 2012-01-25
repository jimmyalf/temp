namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class Shop : Entity
	{
        public virtual string Name { get; set; }
        public virtual string AddressLineOne { get; set; }
        public virtual string AddressLineTwo { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string City { get; set; }
		public virtual string ExternalAccessUsername { get; set; }
		public virtual string ExternalAccessHashedPassword { get; set; }
	}
}