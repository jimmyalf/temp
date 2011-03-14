using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve
{
	public class Payment
	{
		public Payer Transmitter { get; set; }
		public PaymentReciever Reciever { get; set; }
		public PaymentType Type { get; set; }
		public DateTime PaymentDate { get; set; }
		public PaymentPeriodCode PaymentPeriodCode { get; set; }
		public int? NumberOfReoccuringTransactionsLeft { get; set; }
		public decimal Amount { get; set; }
		public string Reference { get; set; }
		public PaymentResult Result { get; set;}
	}
}