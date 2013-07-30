using Spinit.Wpc.Core.UI.Mvp;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Presentation.Site.App
{
	public class WebRegistry : WpcRegistry
	{
		public WebRegistry()
		{
			For<ISqlProvider>().Singleton().Use(Factory.GetSqlProvider);
		}
	}
}