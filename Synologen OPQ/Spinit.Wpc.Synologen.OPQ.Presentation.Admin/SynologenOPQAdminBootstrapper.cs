using Spinit.Wpc.Core.UI;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.OPQ.Presentation.Admin.App;

namespace Spinit.Wpc.Synologen.OPQ.Presentation.Admin
{
	public class SynologenOPQAdminBootstrapper : IWpcComponentBootstrapper
	{
		public void Bootstrap(WpcBootstrapperContext context)
		{
			AddEventListeners();
		}

		private void AddEventListeners()
		{
			var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
			eventAggregator.AddListener(new HandleShopMove());
		}
	}
}