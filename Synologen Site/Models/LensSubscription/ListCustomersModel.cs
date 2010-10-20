using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class ListCustomersModel
	{
		public IEnumerable<ListCustomersItemModel> List { get; set; }

		public ListCustomersModel()
		{
			List = new ListCustomersItemModel[] { }; 
		}
	}
}
