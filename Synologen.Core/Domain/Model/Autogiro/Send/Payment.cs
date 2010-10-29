using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send
{
	public class Payment
	{
		public Payer Transmitter { get; set; }
		public PaymentReciever Reciever { get; set; }
		public PaymentType Type { get; set; }
		public DateTime PaymentDate { get; set; }
		public PeriodCode PeriodCode { get; set; }
		public int? NumberOfReoccuringTransactions { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
	}
}