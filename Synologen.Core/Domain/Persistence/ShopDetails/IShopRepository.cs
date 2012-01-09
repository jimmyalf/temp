using System.Collections.Generic;
using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.ShopDetails
{
    public interface IShopRepository : IReadonlyRepository<Shop>
    {
        IEnumerable<Shop> GetClosestShops(Coordinates coordinates);
    }
}
