using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve
{
	public class Consent
	{
		public Payer Transmitter { get; set; }
		public string AccountNumber { get; set; }
		public string ClearingNumber { get; set; }
		public string PersonalIdNumber { get; set; }
		public string OrgNumber { get; set; }
		public ConsentInformationCode InformationCode { get; set; }
		public ConsentCommentCode CommentCode { get; set; }
		public string RecieverBankgiroNumber { get; set; }
		public DateTime ActionDate { get; set; }
		public DateTime? ConsentValidForDate { get; set; }
	}
}