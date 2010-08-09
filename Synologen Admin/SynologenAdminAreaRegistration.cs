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

			context.MapRoute(AreaName + "FramesAdd", urlPrefix + "frames/add", new { controller = "Frame", action = "Add" });

			context.MapRoute(AreaName + "FramesEdit", urlPrefix + "frames/edit/{id}", new { controller = "Frame", action = "Edit" } );

			context.MapRoute(AreaName + "FramesIndex", urlPrefix + "frames", new { controller = "Frame", action = "Index" } );

			context.MapRoute(AreaName + "FrameColorsAdd", urlPrefix + "framecolors/add", new { controller = "FrameColor", action = "Add" });

			context.MapRoute(AreaName + "FrameColorsDelete", urlPrefix + "framecolors/delete/{id}", new { controller = "FrameColor", action = "Delete" } );

			context.MapRoute(AreaName + "FrameColorsEdit", urlPrefix + "framecolors/edit/{id}", new { controller = "FrameColor", action = "Edit" } );

			context.MapRoute(AreaName + "FrameColorsIndex", urlPrefix + "framecolors", new { controller = "FrameColor", action = "Index" } );

			context.MapRoute(AreaName + "FrameBrandsAdd", urlPrefix + "framebrands/add", new { controller = "FrameBrand", action = "Add" });

			context.MapRoute(AreaName + "FrameBrandsDelete", urlPrefix + "framebrands/delete/{id}", new { controller = "FrameBrand", action = "Delete" } );

			context.MapRoute(AreaName + "FrameBrandsEdit", urlPrefix + "framebrands/edit/{id}", new { controller = "FrameBrand", action = "Edit" } );

			context.MapRoute(AreaName + "FrameBrandsIndex", urlPrefix + "framebrands", new { controller = "FrameBrand", action = "Index" } );

		}

		public override string AreaName
		{
			get { return "SynologenAdmin"; }
		}
	}
}