namespace Spinit.Wpc.Synologen.Opq.Core.Exceptions
{
	public class NodeException : BaseException
	{
		private readonly NodeErrors _errorCode = NodeErrors.None;

		public NodeException (string message, NodeErrors errorCode) : base (message, (int) errorCode)
		{
			_errorCode = errorCode;
		}
		
		public override string LocalizationKey { get { return string.Concat ("NodeErrors-", _errorCode.ToString ()); } }
	}
}
