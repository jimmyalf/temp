using System;
using System.Net;
using Spinit.Wpc.Synologen.Core.Domain.EventArgs;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFtpCommandService : IDisposable
	{
		void Open(string domainName);
		void Open(IPAddress ipAddress);
		string Execute(string command);
		void ExecuteNoReply(string command);
		void Close();
		event EventHandler<OnCommandSentEventArgs> OnCommandSent;
		event EventHandler<OnResponseReceivedEventArgs> OnResponseReceived;
	}

	public interface IFtpAsyncCommandService : IDisposable
	{
		void Open(string domainName);
		void Open(IPAddress ipAddress);
		void Execute(string command, Action<string> readResponse);
		void Close();
		event EventHandler<OnCommandSentEventArgs> OnCommandSent;
		event EventHandler<OnResponseReceivedEventArgs> OnResponseReceived;
	}
}