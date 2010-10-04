using Spinit.Wpc.Core.UI;

namespace Spinit.Wpc.Synologen.Presentation
{
	public class WpcSynologenAdminBootstrapper : IWpcComponentBootstrapper
	{
		public void Bootstrap(WpcBootstrapperContext context)
		{
			Bootstrapper.Bootstrap(Dependency.NHibernate, context);
		}
	}
}