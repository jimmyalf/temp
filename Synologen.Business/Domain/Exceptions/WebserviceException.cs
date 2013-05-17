using System;
using System.Runtime.Serialization;

namespace Spinit.Wpc.Synologen.Business.Domain.Exceptions{
	[DataContract]
	public class WebserviceException : Exception {
		public WebserviceException(string message) : base(message) { }
		public WebserviceException(string message, Exception innerException) : base(message, innerException) { }
	}
}