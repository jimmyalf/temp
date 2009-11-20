using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spinit.Exceptions;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	public class ExceptionHandler
	{
		public static void HandleException(BaseCodeException ex, UserMessageManager manager)
		{
			manager.NegativeMessage = Business.BUtilities.GetResourceString(ex.LocalizationKey, SessionContext.CurrentOpq);
			manager.Visible = true;
		}
	}
}
