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
			return Equals(entity);
		}

		public virtual bool Equals(CustomerAddress other)
		{
			return other != null
				&& Equals(AddressLineOne, other.AddressLineOne)
				&& Equals(AddressLineTwo, other.AddressLineTwo)
				&& Equals(City, other.City)
				&& Equals(Country, other.Country)
				&& Equals(PostalCode, other.PostalCode);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = (AddressLineOne != null ? AddressLineOne.GetHashCode() : 0);
				result = (result * 397) ^ (AddressLineTwo != null ? AddressLineTwo.GetHashCode() : 0);
				result = (result * 397) ^ (City != null ? City.GetHashCode() : 0);
				result = (result * 397) ^ (PostalCode != null ? PostalCode.GetHashCode() : 0);
				result = (result * 397) ^ (Country != null ? Country.GetHashCode() : 0);
				return result;
			}
		}
	}
}