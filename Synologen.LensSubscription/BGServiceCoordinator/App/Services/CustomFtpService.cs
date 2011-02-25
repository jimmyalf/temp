using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class CustomFtpService : IDisposable
	{
		private readonly Socket _socket;
		private const int ReadBufferBlockSize = 512;
		private const string EndOfFile = "\r\n";
		public event EventHandler<OnCommandExecutedEventArgs> OnCommandExecuted;
		public event EventHandler<OnResponseReceivedEventArgs> OnResponseReceived;

		public CustomFtpService(string address) : this(ResolveDnsAddress(address)) { }

		public CustomFtpService(IPAddress ipAddress)
		{
			var serverIP = new IPEndPoint(ipAddress, 21);
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_socket.Connect(serverIP);
		}

		public string Execute(string command)
		{
			var bytesSent = Encoding.ASCII.GetBytes(command.TrimEnd('\r','\n') + EndOfFile);
			_socket.Send(bytesSent, bytesSent.Length, SocketFlags.None);
			OnCommandExecuted.TryInvoke(this,new OnCommandExecutedEventArgs(command));
			return ReadReply();
		}

		private string ReadReply()
		{
			var buffer = new Byte[ReadBufferBlockSize];
			var message = string.Empty;
			while (true)
			{
				var bytes = _socket.Receive(buffer, buffer.Length, SocketFlags.None);
				message += Encoding.ASCII.GetString(buffer, 0, bytes);
				if (bytes < buffer.Length) break;
			}
			OnResponseReceived.TryInvoke(this,new OnResponseReceivedEventArgs(message));
			return message;
		}

		private static IPAddress ResolveDnsAddress(string dnsAddress)
		{
			return Dns.GetHostEntry(dnsAddress).AddressList.First();
		}

		public void Dispose() 
		{
			_socket.Disconnect(false);
		}
	}

	public class OnResponseReceivedEventArgs : EventArgs
	{
		public OnResponseReceivedEventArgs(string response) { Response = response; }
		public string Response { get; private set; }
	}

	public class OnCommandExecutedEventArgs : EventArgs
	{
		public OnCommandExecutedEventArgs(string command) { Command = command; }
		public string Command { get; private set; }
	}
}