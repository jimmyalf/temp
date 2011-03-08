using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Synologen.LensSubscription.BGWebService.App.Bootstrapping;

namespace Synologen.LensSubscription.BGWebService.App.IoC
{
	public class BGServiceHostFactory : ServiceHostFactory
	{
		public BGServiceHostFactory()
		{
			Bootstrapper.Boostrap();
		}

		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return new BGServiceHost(serviceType, baseAddresses);
		}
	}
}