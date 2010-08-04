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

			context.MapRoute("SynologenAdminFramesAdd", prefix + "frames/add", new { controller = "Frame", action = "Add" });
			context.MapRoute("SynologenAdminFramesEdit", prefix + "frames/edit/{id}", new { controller = "Frame", action = "Edit" } );
			context.MapRoute("SynologenAdminDefault", prefix + "frames", new { controller = "Frame", action = "Index" } );

		}

		public override string AreaName
		{
			get { return "SynologenAdmin"; }
		}
	}
}