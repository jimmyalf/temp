using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias;

namespace Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters
{
    public class NearbyShopsCriteriaConverter : NHibernateActionCriteriaConverter<NearbyShopsCriteria, Shop>
    {
        public NearbyShopsCriteriaConverter(ISession session) : base(session)
        {
        }

        public override ICriteria Convert(NearbyShopsCriteria source)
        {
            return Session.CreateCriteriaOf<Shop>().SetMaxResults(10);
        }
    }
}
