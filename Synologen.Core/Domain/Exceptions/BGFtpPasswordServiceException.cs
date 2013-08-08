using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Exceptions
{
	public class BGFtpPasswordServiceException : Exception
	{
		public BGFtpPasswordServiceException(string message) : base(message) {  }
		public BGFtpPasswordServiceException(string format, params object[] parameters) : base(string.Format(format, parameters)) {  }
	}
}