using System;
using System.ServiceModel;

namespace Synologen.LensSubscription.BGWebService.App.IoC
{
	public class BGServiceHost : ServiceHost
	{
		public BGServiceHost() { }

		public BGServiceHost(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses) { }

		protected override void OnOpening()
		{
			Description.Behaviors.Add(new BGServiceBehavior());
			base.OnOpening();
		}
	}
}