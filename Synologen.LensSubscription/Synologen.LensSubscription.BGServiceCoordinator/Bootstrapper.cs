using Spinit.Data;
using Spinit.Wpc.Synologen.LensSubscription.BGServiceCoordinator.IoC;
using StructureMap;

namespace Spinit.Wpc.Synologen.LensSubscription.BGServiceCoordinator
{
	public static class Bootstrapper
	{
		public static void Bootstrap()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<TaskRunnerRegistry>());
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		}
	}
}