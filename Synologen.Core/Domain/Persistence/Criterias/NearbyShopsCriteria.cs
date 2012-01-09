using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias
{
    public class NearbyShopsCriteria : IActionCriteria
    {
        public Coordinates Coordinates { get; set; }
    }
}
