using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
	public class BGPaymentToSend : Entity
	{
		public PaymentPeriodCode PeriodCode{ get; set; }
		public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public string Reference { get; set; }
		public string CustomerNumber { get; set; }
		public PaymentType Type { get; set; }
		public DateTime? SendDate { get; set; }
		public bool HasBeenSent { get { return SendDate.HasValue; } }
	}
}