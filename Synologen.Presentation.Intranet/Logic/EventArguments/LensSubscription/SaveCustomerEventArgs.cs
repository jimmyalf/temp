using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription
{
	public class SaveCustomerEventArgs : EventArgs
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PersonalIdNumber { get; set; }
		public string Email { get; set; }
		public string MobilePhone { get; set; }
		public string Phone { get; set; }
		public string AddressLineOne { get; set; }
		public string AddressLineTwo { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		//public int CountryId { get; set; }
		public string Notes { get; set; }

		//public int ShopId { get; set; }
	}
}
