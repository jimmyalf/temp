using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ShopDetails
{
    public class SearchShopsCriteria : IActionCriteria
    {
        public string Search { get; set; }
        public int CategoryId { get; set; }

        public SearchShopsCriteria(string search, int categoryId)
        {
            Search = search;
            CategoryId = categoryId;
        }
    }
}
