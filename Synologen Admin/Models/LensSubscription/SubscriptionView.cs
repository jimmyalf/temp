using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Models.LensSubscription
{
	public class SubscriptionView
	{
		public string Activated { get; set; }
		public string Created { get; set; }
		public string AddressLineOne { get; set; }
		public string AddressLineTwo { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string Email { get; set; }
		public string MobilePhone { get; set; }
		public string Phone { get; set; }
		public string CustomerName { get; set; }
		public string PersonalIdNumber { get; set; }
		public string ShopName { get; set; }
		public IEnumerable<TransactionListItemView> TransactionList { get; set; }
		public string AccountNumber { get; set; }
		public string ClearingNumber { get; set; }
		public string MonthlyAmount { get; set; }
		public string Status { get; set; }
	}
}