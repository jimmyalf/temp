using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public class BGPaymentToSend : Entity
	{
		public virtual PaymentPeriodCode PeriodCode{ get; set; }
		public virtual decimal Amount { get; set; }
		public virtual DateTime PaymentDate { get; set; }
		public virtual string Reference { get; set; }
		public virtual string CustomerNumber { get; set; }
		public virtual PaymentType Type { get; set; }
		public virtual DateTime? SendDate { get; set; }
		public virtual bool HasBeenSent { get { return SendDate.HasValue; } }
	}
}