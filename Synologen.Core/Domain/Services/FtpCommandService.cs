using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Spinit.Wpc.Synologen.Core.Domain.EventArgs;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public class FtpCommandService : IFtpCommandService
	{
		private Socket _socket;
		private bool _isDisposed;
		private const int ReadBufferBlockSize = 512;
		private const string EndOfFile = "\r\n";
		private const int DefaultFtpPort = 21;
		public event EventHandler<OnCommandSentEventArgs> OnCommandSent;
		public event EventHandler<OnResponseReceivedEventArgs> OnResponseReceived;

		public virtual void Open(string domainName)
		{
			_isDisposed = false;
			var ipAddress = ResolveDnsAddress(domainName);
			Open(ipAddress);
		}

		public virtual void Open(IPAddress ipAddress)
		{
			_isDisposed = false;
			var serverIP = new IPEndPoint(ipAddress, DefaultFtpPort);
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_socket.Connect(serverIP);
		}

		public virtual string Execute(string command)
		{
			return Execute(command, true);
		}

		public void ExecuteNoReply(string command) 
		{ 
			Execute(command, false);
		}

		protected virtual string Execute(string command, bool readReply)
		{
			var bytesSent = Encoding.ASCII.GetBytes(command.TrimEnd(EndOfFile.ToCharArray()) + EndOfFile);
			_socket.Send(bytesSent, bytesSent.Length, SocketFlags.None);
			if(OnCommandSent != null)
			{
				OnCommandSent(this, new OnCommandSentEventArgs(command));	
			}
			return readReply ? ReadReply() : String.Empty;
		}

		private string ReadReply()
		{
			Thread.Sleep(300);
			var buffer = new Byte[ReadBufferBlockSize];
			var message = string.Empty;
			while (true)
			{
				var bytes = _socket.Receive(buffer, buffer.Length, SocketFlags.None);
				message += Encoding.ASCII.GetString(buffer, 0, bytes);
				if (bytes < buffer.Length) break;
			}
			if(OnResponseReceived != null)
			{
				OnResponseReceived(this, new OnResponseReceivedEventArgs(message));
			}
			return message;
		}

		private static IPAddress ResolveDnsAddress(string url)
		{
			if(url.Contains("/"))
			{
				var domainName = GetDomainNameFromUrl(url);
				return Dns.GetHostEntry(domainName).AddressList.First();
			}
			return Dns.GetHostEntry(url).AddressList.First();
		}

		private static string GetDomainNameFromUrl(string url)
		{
			var urlWithoutSchema = Regex.Replace(url, @"^(\w+):\/\/", String.Empty);
			return Regex.Replace(urlWithoutSchema, "/.+", String.Empty);
		}

		public virtual void Close()
		{
			if(!_isDisposed) Dispose();
		}

		public virtual void Dispose() 
		{
			_socket.Disconnect(false);
			_isDisposed = true;
		}
	}
}