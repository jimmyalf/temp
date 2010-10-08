namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class CustomerAddress
	{
		public string AddressLineOne { get; set; }
		public string AddressLineTwo { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public Country Country { get; set; }
	}
}