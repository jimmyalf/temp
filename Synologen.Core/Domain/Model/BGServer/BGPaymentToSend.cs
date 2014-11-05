using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public class BGPaymentToSend : Entity
	{
		public virtual PaymentPeriodCode PaymentPeriodCode { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual DateTime PaymentDate { get; set; }
		public virtual string Reference { get; set; }
		public virtual AutogiroPayer Payer { get; set;} 
		public virtual PaymentType Type { get; set; }
		public virtual DateTime? SendDate { get; set; }
		public virtual bool HasBeenSent { get { return SendDate.HasValue; } }
	}
}