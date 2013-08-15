using System;

namespace Spinit.Wpc.Synologen.Invoicing.Test.App
{
	public class AssertionFailedException : Exception
	{
		public AssertionFailedException() { }
		public AssertionFailedException(string message) : base(message) { }
	}
}