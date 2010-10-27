namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class CustomerAddress
	{
		public virtual string AddressLineOne { get; set; }
		public virtual string AddressLineTwo { get; set; }
		public virtual string City { get; set; }
		public virtual string PostalCode { get; set; }
		public virtual Country Country { get; set; }

		public override bool Equals(object obj)
		{
			var entity = obj as CustomerAddress;
			return entity != null
				&& Equals(AddressLineOne, entity.AddressLineOne)
				&& Equals(AddressLineTwo, entity.AddressLineTwo)
				&& Equals(City, entity.City)
				&& Equals(Country, entity.Country)
				&& Equals(PostalCode, entity.PostalCode);
		}
	}
}