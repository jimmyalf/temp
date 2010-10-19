namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class CustomerAddress
	{
		public string AddressLineOne { get; set; }
		public string AddressLineTwo { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public Country Country { get; set; }

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