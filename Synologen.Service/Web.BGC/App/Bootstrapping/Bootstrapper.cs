using Spinit.Data;
using StructureMap;
using Synologen.Service.Web.BGC.App.IoC;

namespace Synologen.Service.Web.BGC.App.Bootstrapping
{
	public static class Bootstrapper
	{
		public static void Boostrap()
		{
			InitializeDependencyInjection();
			InitializeCritieriaConverters();
		}

		public static void InitializeDependencyInjection()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<WebServiceRegistry>());
		}

		public static void InitializeCritieriaConverters()
		{
			ActionCriteriaExtensions.ConstructConvertersUsing(ObjectFactory.GetInstance);
		}
	}
}