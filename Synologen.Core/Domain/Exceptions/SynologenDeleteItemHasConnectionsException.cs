using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Exceptions
{
	public class SynologenDeleteItemHasConnectionsException : Exception
	{
		public SynologenDeleteItemHasConnectionsException(string message) : base(message) {  }
		public SynologenDeleteItemHasConnectionsException(string message, Exception innerException) : base(message, innerException) {  }
	}
}