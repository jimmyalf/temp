using System;
using System.Text;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public static class WpcConfirmation
	{
		
		public static string WpcConfirmationDialog(this HtmlHelper helper, string identifier, string message)
		{
			return new StringBuilder()
				.AppendLine("<script type=\"text/javascript\">")
				.AppendLine("	$(function() {")
				.AppendFormat("		$('{0}').click(function() {{{1}", identifier, Environment.NewLine)
				.AppendFormat("			return confirm('{0}');{1}", message, Environment.NewLine)
				.AppendLine("		});")
				.AppendLine("	});")
				.AppendLine("</script>")
				.ToString();
		}
		public static string WpcConfirmationDialog(this HtmlHelper helper, string message)
		{
			return WpcConfirmationDialog(helper, "input.delete", message);
		}
		public static string WpcConfirmationDialog(this HtmlHelper helper)
		{
			return WpcConfirmationDialog(helper, Resources.WpcConfirmationDialog.DefaultMessage);
		}

	}
}