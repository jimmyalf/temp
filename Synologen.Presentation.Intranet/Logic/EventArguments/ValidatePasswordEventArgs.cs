using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments
{
	public class ValidatePasswordEventArgs : EventArgs
	{
		public ValidatePasswordEventArgs() {  }
		public ValidatePasswordEventArgs(string password) { Password = password;  }
		public string Password { get; set; }
	}
}