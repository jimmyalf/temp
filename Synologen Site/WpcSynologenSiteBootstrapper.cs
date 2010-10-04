using Spinit.Wpc.Core.UI;

namespace Spinit.Wpc.Synologen.Presentation.Site
{
	public class WpcSynologenSiteBootstrapper : IWpcComponentBootstrapper
	{
		public void Bootstrap(WpcBootstrapperContext context)
		{
			Bootstrapper.Bootstrap(Dependency.NHibernate, context);
		}
	}
}