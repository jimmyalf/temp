using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Presentation.Site.Models;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers
{
	public static class PlaceHolderExtensions
	{
		public static void SetupTemplate(this PlaceHolder templatePlaceHolder, ITemplate template)
		{
			if(template == null) return;
			var messageContainer = new MessageContainer();
            template.InstantiateIn(messageContainer);
			templatePlaceHolder.Controls.Add(messageContainer);
		}
	}
}