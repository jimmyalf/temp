namespace Spinit.Wpc.Synologen.Presentation.Helpers
{
	public interface IWpcActionMessage {
		WpcActionMessageType Type { get; set; }
		string Message { get; set; }
	}
}