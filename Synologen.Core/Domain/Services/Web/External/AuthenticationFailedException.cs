using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public class AuthenticationFailedException : Exception
	{
		public AuthenticationFailedException(string message) :base(message) { }
	}
}