namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class RecievedError
	{
		public int PayerId { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
		public ErrorType CommentCode { get; set; }
	}
}