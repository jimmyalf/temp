using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spinit.Wpc.Synologen.OPQ.Presentation
{
	public class ComponentPages
	{
		private const string PageBase = "~/Components/Synologen/";

		public const string OpqStart = PageBase + "OpqIndex.aspx";
		public const string OpqStartQueryNode = PageBase + "OpqIndex.aspx?nodeId={0}";
		public const string OpqImprovments = PageBase + "OpqImprovments.aspx";
		public const string OpqShopRoutines = PageBase + "OpqShopRoutines.aspx";
	}
}
