using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ShopDetails
{
    public class ActiveShopsCriteria : IActionCriteria
    {
        public int CategoryId { get; set; }

        public ActiveShopsCriteria(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
