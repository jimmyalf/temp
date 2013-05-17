using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments
{
	public class ValidatedEventArgs : EventArgs
	{
		public string UserName { get; set; }
		public bool UserIsValidated { get; set; }
	}
}