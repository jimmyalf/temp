using Spinit.Wpc.Core.UI;

namespace Spinit.Wpc.Synologen.Presentation.Site
{
	public class Bootstrapper : IWpcComponentBootstrapper
	{
		public void Bootstrap(WpcBootstrapperContext context)
		{
			Spinit.Wpc.Core.UI.Bootstrapper.Bootstrap(Dependency.NHibernate, context);
		}
	}
}