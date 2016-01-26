using StructureMap;
using Synologen.LensSubscription.BGData.Test.IoC;

namespace Synologen.LensSubscription.BGData.Test
{
	public static class Bootstrapper
	{
		public static void Bootstrap()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<TestRegistry>());
		}
	}
}