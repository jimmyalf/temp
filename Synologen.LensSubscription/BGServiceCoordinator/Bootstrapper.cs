using Spinit.Data;
using StructureMap;
using Synologen.LensSubscription.BGServiceCoordinator.App.IoC;

namespace Synologen.LensSubscription.BGServiceCoordinator
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