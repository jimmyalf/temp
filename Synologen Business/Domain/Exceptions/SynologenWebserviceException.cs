using System;
using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Exceptions{
	[DataContract]
	public class SynologenWebserviceException : Exception {
		public SynologenWebserviceException(string message) : base(message) { }
		public SynologenWebserviceException(string message, Exception innerException) : base(message, innerException) { }
	}
}