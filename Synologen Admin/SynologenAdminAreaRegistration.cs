using System.Web.Mvc;
using Spinit.Wpc.Core.UI.Mvc;
using Spinit.Wpc.Synologen.Core.Persistence;
using Spinit.Wpc.Synologen.Presentation.Application.ModelBinders;

namespace Spinit.Wpc.Synologen.Presentation
{
	public class SynologenAdminAreaRegistration : AreaRegistration
	{
		public SynologenAdminAreaRegistration()
		{
			ActionCriteriaExtensions.ConstructConvertersUsing(ServiceLocation.Resolve);
			ModelBinders.Binders.DefaultBinder = new GridSortPropertyMappingModelBinder();
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			const string urlPrefix = "components/synologen/";

			context.MapRoute(AreaName + "FrameAdd", urlPrefix + "frames/add", new { controller = "Frame", action = "Add" });
			context.MapRoute(AreaName + "FrameDelete", urlPrefix + "frames/delete/{id}", new { controller = "Frame", action = "Delete" } );
			context.MapRoute(AreaName + "FrameEdit", urlPrefix + "frames/edit/{id}", new { controller = "Frame", action = "Edit" } );
			context.MapRoute(AreaName + "FrameIndex", urlPrefix + "frames", new { controller = "Frame", action = "Index" } );


			context.MapRoute(AreaName + "FrameAddColor", urlPrefix + "frames/addcolor", new { controller = "Frame", action = "AddColor" });
			context.MapRoute(AreaName + "FrameDeleteColor", urlPrefix + "frames/deletecolor/{id}", new { controller = "Frame", action = "DeleteColor" } );
			context.MapRoute(AreaName + "FrameEditColor", urlPrefix + "frames/editcolor/{id}", new { controller = "Frame", action = "EditColor" } );
			context.MapRoute(AreaName + "FrameColors", urlPrefix + "frames/colors", new { controller = "Frame", action = "Colors" } );


			context.MapRoute(AreaName + "FrameAddBrand", urlPrefix + "frames/addbrand", new { controller = "Frame", action = "AddBrand" });
			context.MapRoute(AreaName + "FrameDeleteBrand", urlPrefix + "frames/deletebrand/{id}", new { controller = "Frame", action = "DeleteBrand" } );
			context.MapRoute(AreaName + "FrameEditBrand", urlPrefix + "frames/editbrand/{id}", new { controller = "Frame", action = "EditBrand" } );
			context.MapRoute(AreaName + "FrameBrands", urlPrefix + "frames/brands", new { controller = "Frame", action = "Brands" } );


			context.MapRoute(AreaName + "FrameAddGlassType", urlPrefix + "frames/addglasstype", new { controller = "Frame", action = "AddGlassType" });
			context.MapRoute(AreaName + "FrameDeleteGlassType", urlPrefix + "frames/deleteglasstype/{id}", new { controller = "Frame", action = "DeleteGlassType" } );
			context.MapRoute(AreaName + "FrameEditGlassType", urlPrefix + "frames/editglasstype/{id}", new { controller = "Frame", action = "EditGlassType" } );
			context.MapRoute(AreaName + "FrameGlassTypes", urlPrefix + "frames/glasstypes", new { controller = "Frame", action = "GlassTypes" } );


			context.MapRoute(AreaName + "FrameOrders", urlPrefix + "frames/orders", new { controller = "Frame", action = "FrameOrders" } );

		}

		public override string AreaName
		{
			get { return "SynologenAdmin"; }
		}
	}
}