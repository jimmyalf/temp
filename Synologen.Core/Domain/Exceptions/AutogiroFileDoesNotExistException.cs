using System;
using System.IO;

namespace Spinit.Wpc.Synologen.Core.Domain.Exceptions
{
	public class AutogiroFileDoesNotExistException : FileNotFoundException
	{
		public AutogiroFileDoesNotExistException() { }
		public AutogiroFileDoesNotExistException(string message) : base(message) {  }
		public AutogiroFileDoesNotExistException(string format, params string[] formatParameters) : base(string.Format(format, formatParameters)) {  }
		public AutogiroFileDoesNotExistException(string message, Exception innerException) : base(message, innerException) {  }
		public AutogiroFileDoesNotExistException(string format, Exception innerException, params string[] formatParameters) : base(string.Format(format, formatParameters), innerException) {  }
		public AutogiroFileDoesNotExistException(string message, string fileName) : base(message, fileName) {  }
		public AutogiroFileDoesNotExistException(string format, string fileName, params string[] formatParameters) : base(string.Format(format, formatParameters), fileName) {  }
		public AutogiroFileDoesNotExistException(string message, string fileName, Exception innerException) : base(message, fileName, innerException) {  }
		public AutogiroFileDoesNotExistException(string format, string fileName, Exception innerException, params string[] formatParameters) : base(string.Format(format, formatParameters), fileName, innerException) {  }
	}
}