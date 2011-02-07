using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription
{
	public class ListCustomersModel
	{
		public IEnumerable<ListCustomersItemModel> List { get; set; }
		public string FirstNameSortUrl { get; set; }
		public string LastNameSortUrl { get; set; }
		public string PersonNumberSortUrl { get; set; }
		public string SearchTerm { get; set; }
		
		public ListCustomersModel()
		{
			List = new ListCustomersItemModel[] { }; 
		}
	}
}
