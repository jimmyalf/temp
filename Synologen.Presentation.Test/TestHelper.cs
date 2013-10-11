using System.Collections.Generic;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Presentation.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
	public static class TestHelper
	{
		public static IList<IWpcActionMessage> GetWpcActionMessages(this ControllerBase controller)
		{
			const string tempDataKey = Helpers.Extensions.WpcActionMessageExtensions.DefaultActionMessagesTempDataKey;
			return controller.TempData[tempDataKey] as IList<IWpcActionMessage>;
		}
	}
}