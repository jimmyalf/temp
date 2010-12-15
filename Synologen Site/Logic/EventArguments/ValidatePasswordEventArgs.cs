using System;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments
{
	public class ValidatePasswordEventArgs : EventArgs
	{
		public ValidatePasswordEventArgs() {  }
		public ValidatePasswordEventArgs(string password) { Password = password;  }
		public string Password { get; set; }
	}
}