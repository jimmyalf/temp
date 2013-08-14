using Spinit.Exceptions;

namespace Spinit.Wpc.Synologen.OPQ.Presentation
{
	public class ExceptionHandler
	{
		public static void HandleException(BaseCodeException ex, UserMessageManager manager)
		{
			manager.NegativeMessage = Business.BUtilities.GetResourceString(ex.LocalizationKey, SessionContext.CurrentOpq);
			manager.Visible = true;
		}

		public static void HandleException(System.Web.UI.Page page, BaseCodeException ex)
		{
			var manager = OpqUtility.FindControlRecursive(page, "opqUserMessageManager");
			if (manager != null)
			{
				HandleException(ex, (UserMessageManager) manager);
			}
		}
	}
}
