using Spinit.Wpc.Synologen.Core.Domain.Model;
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
		}
	}
}