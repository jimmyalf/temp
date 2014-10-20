using Spinit.Data;
using Spinit.Data.NHibernate;
using StructureMap.Configuration.DSL;
using Synologen.LensSubscription.BGData.CriteriaConverters;

namespace Synologen.LensSubscription.BGData.Test.IoC
{
	public class TestRegistry : Registry
	{
		public TestRegistry()
		{
			// Register criteria converters
			Scan(x =>
			{
				x.AssemblyContainingType<AllUnhandledReceivedConsentFileSectionsCriteriaConverter>();
				x.Assembly(typeof(NHibernateActionCriteriaConverter<,>).Assembly.FullName);
				x.ConnectImplementationsToTypesClosing(typeof(IActionCriteriaConverter<,>));
			});
		}
	}
}