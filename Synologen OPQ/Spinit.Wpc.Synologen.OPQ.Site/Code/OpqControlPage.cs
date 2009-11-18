using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.Presentation.Site.Code;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	public class OpqControlPage : SynologenUserControl
	{
		protected Context _context;


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			_context = SessionContext.CurrentOpq;
		}

	}
}
