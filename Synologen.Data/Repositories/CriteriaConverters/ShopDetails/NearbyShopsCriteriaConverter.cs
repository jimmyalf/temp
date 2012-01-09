using System;
using System.Globalization;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.ShopDetails;
using Spinit.Wpc.Synologen.Data.Extensions;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ShopDetails
{
    public class NearbyShopsCriteriaConverter : NHibernateActionCriteriaConverter<NearbyShopsCriteria, Shop>
    {
        public NearbyShopsCriteriaConverter(ISession session) : base(session) { }

        public override ICriteria Convert(NearbyShopsCriteria source)
        {
            const string unformattedOrder = "6371 * acos(cos(radians({0})) * cos(radians(cLatitude)) * cos(radians(cLongitude) - radians({1})) + sin(radians({0})) * sin(radians(cLatitude)))";
            var order = String.Format(unformattedOrder, source.Coordinates.Latitude.ToString(CultureInfo.InvariantCulture), source.Coordinates.Longitude.ToString(CultureInfo.InvariantCulture));

            return Session.CreateCriteriaOf<Shop>()
                .FilterEqual(x => x.Active, true)
                .Add(Restrictions.IsNotNull("Coordinates.Latitude"))
                .AddOrder(OrderBySqlFormula.AddOrder(order, true))
                .SetMaxResults(10);
        }
    }
}
