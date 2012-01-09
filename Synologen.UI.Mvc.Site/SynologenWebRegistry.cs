using System.Web.Mvc;
using Spinit.Data;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Core.UI.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ShopDetails;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Repositories.CriteriaConverters.ShopDetails;
using Spinit.Wpc.Synologen.Data.Repositories.ShopDetailsRepositories;
using Spinit.Wpc.Synologen.UI.Mvc.Site.App.Services;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site
{
    public class SynologenWebRegistry : WpcRegistry
	{
        public SynologenWebRegistry()
		{
			Scan(x =>
			{
                x.AssemblyContainingType<SynologenWebRegistry>();
				x.WithDefaultConventions();
				x.AddAllTypesOf<IController>().NameBy(c => c.Name);
			});

            Scan(x =>
            {
                x.AssemblyContainingType<NearbyShopsCriteriaConverter>();
                x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
                x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
            });

            For<IGeocodingService>().Use<GeocodingService>();
            For<IShopRepository>().HybridHttpOrThreadLocalScoped().Use<ShopRepository>();
		}
	}
}