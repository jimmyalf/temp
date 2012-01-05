using Spinit.Wpc.Core.UI;

namespace Spinit.Wpc.Synologen.UI.Mvc.Site
{
    public class SynologenBootstrapper : IWpcComponentBootstrapper
    {
        public void Bootstrap(WpcBootstrapperContext context)
        {
            Bootstrapper.Bootstrap(Dependency.NHibernate, context);
        }
    }
}