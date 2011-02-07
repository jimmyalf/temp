namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public class WpcActionMessage : IWpcActionMessage
	{
		public WpcActionMessageType Type { get; set; }
		public string Message { get; set; }
	}
}