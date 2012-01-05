using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site.Models
{
    public class SearchShopView
    {
        public string Search { get; set; }
        public int NrOfResults { get; set; }
        public IEnumerable<ShopListItem> Shops { get; set; }
    }
}