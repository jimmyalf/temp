using System;
using System.Collections.Generic;
using System.Globalization;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ShopDetails;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Repositories.ShopDetailsRepositories
{
    public class ShopRepository : NHibernateReadonlyRepository<Shop>, IShopRepository
    {
        public ShopRepository(ISession session) : base(session) { }

        public IEnumerable<Shop> GetClosestShops(Coordinates coordinates)
        {
            const string unformattedOrder = "6371 * acos(cos(radians({0})) * cos(radians(cLatitude)) * cos(radians(cLongitude) - radians({1})) + sin(radians({0})) * sin(radians(cLatitude)))";
            var order = String.Format(unformattedOrder, coordinates.Latitude.ToString(CultureInfo.InvariantCulture), coordinates.Longitude.ToString(CultureInfo.InvariantCulture));

            return Session.CreateCriteriaOf<Shop>()
                .Add(Restrictions.IsNotNull("Coordinates.Latitude"))
                .AddOrder(OrderBySqlFormula.AddOrder(order, false))
                .SetMaxResults(10)
                .List<Shop>();
        }
    }
}
