using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ShopDetails
{
    public class NearbyShopsCriteria : IActionCriteria
    {
        public Coordinates Coordinates { get; set; }
        public int CategoryId { get; set; }

        public NearbyShopsCriteria(Coordinates coordinates, int categoryId)
        {
            Coordinates = coordinates;
            CategoryId = categoryId;
        }
    }
}
