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
			const string urlPrefix = "components/synologen/";

			context.MapRoute(AreaName + "FramesAdd", 
				urlPrefix + "frames/add", new { controller = "Frame", action = "Add" });

			context.MapRoute(AreaName + "FramesEdit", 
				urlPrefix + "frames/edit/{id}", new { controller = "Frame", action = "Edit" } );

			context.MapRoute(AreaName + "FramesIndex", 
				urlPrefix + "frames", new { controller = "Frame", action = "Index" } );

			context.MapRoute(AreaName + "FrameColorsIndex", 
				urlPrefix + "framecolors", new { controller = "FrameColor", action = "Index" } );

		}

		public override string AreaName
		{
			get { return "SynologenAdmin"; }
		}
	}
}