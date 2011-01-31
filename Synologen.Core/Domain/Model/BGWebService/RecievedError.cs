namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class RecievedError
	{
		public int PayerId { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
		public ErrorType CommentCode { get; set; }
	}

	public enum ErrorType
	{
		ConsentMissing = 01,
		AccountNotYetApproved = 02,
		ConsentStopped = 03,
		NotYetDebitable = 07	
	}
}