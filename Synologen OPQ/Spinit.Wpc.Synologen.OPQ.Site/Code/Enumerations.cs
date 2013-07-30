using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	public class Enumerations
	{
		public enum AdminActions
		{
			NotSet = 0,
			EditRoutine = 1,
			EditFiles = 2,
		}

		public enum DocumentSaveActions
		{
			NotSet = 0,
			SaveForLater = 1,
			SaveAndPublish = 2
		}

		public enum FileActions
		{
			NotSet = 0,
			Delete = 1,
			MoveUp = 2,
			MoveDown = 3,
		}
	}
}
