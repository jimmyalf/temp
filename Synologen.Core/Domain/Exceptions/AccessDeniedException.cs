using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Exceptions
{
	public class AccessDeniedException : Exception
	{
		public AccessDeniedException() { }
		public AccessDeniedException(string format, params object[] parameters) : base(string.Format(format,parameters)) { }
		public AccessDeniedException(Exception innerException, string message) : base(message, innerException) { }
		public AccessDeniedException(Exception innerException, string format, params object[] parameters) : base(string.Format(format,parameters), innerException) { }
	}
}