using Spinit.Wpc.Core.UI;

namespace Spinit.Wpc.Synologen.Presentation.Intranet
{
	public class WebBootstrapper : IWpcComponentBootstrapper
	{
		public void Bootstrap(WpcBootstrapperContext context)
		{
			Bootstrapper.Bootstrap(Dependency.NHibernate, context);
		}
	}
}