namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models
{
	public class ListItem
	{
		public ListItem() {  }
		public ListItem(string text, string value)
		{
			Text = text;
			Value = value;
		}
		public ListItem(string text, int value)
		{
			Text = text;
			Value = value.ToString();
		}
		public string Text { get; set; }
		public string Value { get; set; }
	}
}