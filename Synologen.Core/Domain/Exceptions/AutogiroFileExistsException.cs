using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Exceptions
{
	public class AutogiroFileExistsException : Exception
	{
		public AutogiroFileExistsException() { }
		public AutogiroFileExistsException(string message) : base(message) {  }
		public AutogiroFileExistsException(string format, params string[] parameters) : base(string.Format(format, parameters)) {  }
		public AutogiroFileExistsException(string message, Exception innerException) : base(message, innerException) {  }
		public AutogiroFileExistsException(string format, Exception innerException, params string[] parameters) : base(string.Format(format, parameters), innerException) {  }
	}
}