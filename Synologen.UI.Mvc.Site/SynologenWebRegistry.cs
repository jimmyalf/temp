using System.Web.Mvc;
using StructureMap.Configuration.DSL;

namespace Synologen.UI.Mvc.Site
{
    public class SynologenWebRegistry : Registry
	{
        public SynologenWebRegistry()
		{
			Scan(x =>
			{
                x.AssemblyContainingType<SynologenWebRegistry>();
				x.WithDefaultConventions();
				x.AddAllTypesOf<IController>().NameBy(c => c.Name);
			});
		}
	}
}