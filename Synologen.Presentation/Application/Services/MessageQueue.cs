using System;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public static class MessageQueue
	{
		private static Message _message;

		public static void SetMessage(string message)
		{
			if (_message != null) throw new ApplicationException("A previous message has been set, but not read");
			_message = new Message(message);
		}
		public static void SetError(string message)
		{
			if (_message != null) throw new ApplicationException("A previous message has been set, but not read");
			_message = new Message(message) {IsError = true};
		}
		public static Message ReadMessage()
		{
			var message = _message;
			_message = null;
			return message;
		}
		public static bool HasMessages
		{
			get { return _message != null; }
		}
	}

	public class Message
	{
		public Message(string message) { Text = message; }
		public string Text { get; set; }
		public bool IsError { get; set; }
	}
}