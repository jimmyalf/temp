using Spinit.Data;
using StructureMap;
using Synologen.Service.Client.BGCTaskRunner.App.IoC;

namespace Synologen.Service.Client.BGCTaskRunner
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