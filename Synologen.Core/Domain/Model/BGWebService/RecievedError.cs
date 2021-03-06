using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class RecievedError
	{
		public int ErrorId { get; set; }
		public int PayerNumber { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
		public ErrorCommentCode CommentCode { get; set; }
		public DateTime PaymentDate { get; set; }
	}
}