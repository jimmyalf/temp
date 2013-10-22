using Spinit.Wpc.Core.UI;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Application.Services;

namespace Spinit.Wpc.Synologen.Presentation
{
	public class WpcSynologenAdminBootstrapper : IWpcComponentBootstrapper
	{
		public void Bootstrap(WpcBootstrapperContext context)
		{
			Bootstrapper.Bootstrap(Dependency.NHibernate, context);
			Bootstrapper.Bootstrap(Dependency.AutoMapper, context);
			AddEventListeners();
		}

		private void AddEventListeners()
		{
			var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
			eventAggregator.AddListener(new TestEventListener());
		}
	}
}