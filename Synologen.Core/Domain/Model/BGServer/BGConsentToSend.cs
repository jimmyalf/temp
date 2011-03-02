using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public class BGConsentToSend : Entity
	{
		public virtual ConsentType Type { get; set; }
		//public virtual string PayerNumber { get; set; }
		public virtual AutogiroPayer Payer { get; set;} 
		public virtual DateTime? SendDate { get; set; }
		public virtual Account Account { get; set; }
		public virtual string OrgNumber { get; set; }
		public virtual string PersonalIdNumber { get; set; }
	}
}