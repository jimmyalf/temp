using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Synologen.Presentation.Helpers.Extensions
{
	public static class WebFormsExtensions
	{
	
		public static IEnumerable<ListItem> GetSelectedItems(this CheckBoxList control)
		{
			if(control == null || control.Items == null) yield break;
			foreach (ListItem item in control.Items)
			{
				if (item.Selected) yield return item;
			}
			yield break;
		}
	}
}