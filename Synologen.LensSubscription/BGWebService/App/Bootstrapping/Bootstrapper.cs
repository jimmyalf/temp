using StructureMap;
using Synologen.LensSubscription.BGWebService.App.IoC;

namespace Synologen.LensSubscription.BGWebService.App.Bootstrapping
{
	public static class Bootstrapper
	{
		public static void Boostrap()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<WebServiceRegistry>());
		}
	}
}