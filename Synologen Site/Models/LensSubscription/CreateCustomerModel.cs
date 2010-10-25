using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class CreateCustomerModel
	{
		public IEnumerable<CountryListItemModel> List { get; set; }
		public bool ShopDoesNotHaveAccessToLensSubscriptions { get; set; }
		public bool DisplayForm
		{
			get { return ShopDoesNotHaveAccessToLensSubscriptions == false; }
		}

		public CreateCustomerModel()
		{
			List = new List<CountryListItemModel> { };
		}
	}

	public class CountryListItemModel
	{
		public string Value { get; set; }
		public string Text { get; set; }
	}
}
