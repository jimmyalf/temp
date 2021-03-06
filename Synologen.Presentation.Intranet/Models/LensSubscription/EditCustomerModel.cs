﻿using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
{
	public class EditCustomerModel
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
		public int CountryId { get; set; }
		public string Notes { get; set; }
		public IEnumerable<SubscriptionListItemModel> Subscriptions { get; set; }
		public bool ShopDoesNotHaveAccessToLensSubscriptions { get; set; }
		public bool ShopDoesNotHaveAccessGivenCustomer { get; set; }
		public bool DisplayForm
		{
			get
			{
				return ShopDoesNotHaveAccessToLensSubscriptions == false
				&& ShopDoesNotHaveAccessGivenCustomer == false;
			}
		}

		public string CreateSubscriptionPageUrl { get; set; }
	}
}
