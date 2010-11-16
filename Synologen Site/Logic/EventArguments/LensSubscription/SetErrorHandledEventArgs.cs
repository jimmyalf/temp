using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription
{
	public class SetErrorHandledEventArgs : EventArgs
	{
		public int ErrorId { get; set; }
	}
}
