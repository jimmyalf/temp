namespace Spinit.Wpc.Synologen.Core.Domain.EventArgs
{
	public class OnCommandSentEventArgs : System.EventArgs
	{
		public OnCommandSentEventArgs(string command) { Command = command; }
		public string Command { get; private set; }
	}
}