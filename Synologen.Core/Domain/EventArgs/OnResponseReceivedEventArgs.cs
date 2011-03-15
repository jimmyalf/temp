namespace Spinit.Wpc.Synologen.Core.Domain.EventArgs
{
	public class OnResponseReceivedEventArgs : System.EventArgs
	{
		public OnResponseReceivedEventArgs(string response) { Response = response; }
		public string Response { get; private set; }
	}
}