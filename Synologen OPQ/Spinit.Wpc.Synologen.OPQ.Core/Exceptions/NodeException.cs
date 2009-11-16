using Spinit.Exceptions;

namespace Spinit.Wpc.Synologen.OPQ.Core.Exceptions
{
	public class NodeException : BaseCodeException
	{
		private readonly NodeErrors _errorCode = NodeErrors.None;

		public NodeException (string message, NodeErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}
		
		public override string LocalizationKey { get { return string.Concat ("NodeErrors_", _errorCode.ToString ()); } }
	}
}
