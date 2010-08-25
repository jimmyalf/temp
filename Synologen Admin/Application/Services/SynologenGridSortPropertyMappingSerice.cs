using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class SynologenGridSortPropertyMappingSerice : BaseGridSortPropertyMappingService
	{
		public SynologenGridSortPropertyMappingSerice()
		{
			Map<FrameController, FrameListItemView, Frame>(x => x.Color, x => x.Color.Name);
			Map<FrameController, FrameListItemView, Frame>(x => x.Brand, x => x.Brand.Name);
			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.Frame, x => x.Frame.Name);
			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.GlassType, x => x.GlassType.Name);
			Map<FrameController, FrameOrderListItemView, FrameOrder>(x => x.Shop, x => x.OrderingShop.Name);
		}
	}
}