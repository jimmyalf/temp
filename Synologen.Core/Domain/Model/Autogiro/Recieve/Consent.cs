using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve
{
	public class Consent
	{
		public Payer Transmitter { get; set; }
		public PaymentReciever Reciever { get; set; }
		public string AccountAndClearingNumber { get; set; }
		public string PersonalIdNumber { get; set; }
		public string OrgNumber { get; set; }
		public ConsentInformationCode InformationCode { get; set; }
		public ConsentCommentCode CommentCode { get; set; }
	}
}