using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription
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
