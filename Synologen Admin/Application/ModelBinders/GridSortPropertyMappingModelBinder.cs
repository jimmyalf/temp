using System.Web.Mvc;
using Spinit.Wpc.Core.UI.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Application.ModelBinders
{
	public class GridSortPropertyMappingModelBinder :  DefaultModelBinder
	{
		private readonly IGridSortPropertyMappingService _mappingService;

		public GridSortPropertyMappingModelBinder()
		{
			_mappingService = ServiceLocation.Resolve<IGridSortPropertyMappingService>();
		}

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) 
		{
			var returnValue = base.BindModel(controllerContext, bindingContext);
			if (returnValue is GridPageSortParameters) {
				var typedValue = (GridPageSortParameters) returnValue;
				var controllerName = GetControllerName(controllerContext);
				typedValue.Column = _mappingService.TryFindMapping(typedValue.Column, controllerName);
			}
			return returnValue;
		}

		private static string GetControllerName(ControllerContext controllerContext)
		{
			return controllerContext.Controller.GetType().Name;
		}
	}
}