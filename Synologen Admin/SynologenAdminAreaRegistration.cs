using System.Web.Mvc;
using Spinit.Wpc.Core.UI.Mvc;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation
{
	public class SynologenAdminAreaRegistration : AreaRegistration
	{
		public SynologenAdminAreaRegistration()
		{
			ActionCriteriaExtensions.ConstructConvertersUsing(ServiceLocation.Resolve);
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			const string prefix = "components/synologen/";

			context.MapRoute(
				"SynologenAdminFramesAdd",
                prefix + "frames/add",
				new { controller = "Frame", action = "Add" }
				);

			context.MapRoute(
				"SynologenAdminDefault",
                prefix + "frames",
				new { controller = "Frame", action = "Index" }
				);

			context.MapRoute(
				"SynologenAdminDefaultSearch",
                prefix + "frames/{search}/{page}",
				new { controller = "Frame", action = "Index", search = "", page = 1 }
				);
		}

		public override string AreaName
		{
			get { return "SynologenAdmin"; }
		}
	}
}