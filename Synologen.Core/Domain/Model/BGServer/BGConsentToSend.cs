using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public class BGConsentToSend : Entity
	{
		public ConsentType Type { get; set; }
		public string CustomerNumber { get; set; }
		public DateTime? SendDate { get; set; }
		public Account Account { get; set; }
		public string OrgNumber { get; set; }
		public string PersonalIdNumber { get; set; }
	}
}