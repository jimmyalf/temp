using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Exceptions
{
    public class AutogiroFileSplitException : Exception
    {
        public AutogiroFileSplitException() { }
        public AutogiroFileSplitException (string message) : base(message) { }
		public AutogiroFileSplitException (string message, Exception ex) : base(message, ex) { }
    }
}
