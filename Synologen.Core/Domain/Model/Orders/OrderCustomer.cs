namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
    public class OrderCustomer : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string PersonalIdNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string MobilePhone { get; set; }
        public virtual string Phone { get; set; }
        public virtual string AddressLineOne { get; set; }
        public virtual string AddressLineTwo { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string Notes { get; set; }
		public virtual Shop Shop { get; set; }
    }
}
