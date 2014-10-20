using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class HandleErrorEventArgs : EventArgs
	{
		public int ErrorId { get; set; }

		public HandleErrorEventArgs(int errorId)
		{
			ErrorId = errorId;
		}
	}
}