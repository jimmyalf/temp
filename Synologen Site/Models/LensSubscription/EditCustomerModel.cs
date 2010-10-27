using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class EditCustomerModel
	{

		public EditCustomerModel()
		{
			List = new List<CountryListItemModel> { };
		}

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

		public IEnumerable<CountryListItemModel> List { get; set; }
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
	}

	public class SubscriptionListItemModel
	{
		public string CreatedDate { get; set; }
		public string Status { get; set; }
		public string EditSubscriptionPageUrl { get; set; }
	}
}
