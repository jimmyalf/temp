using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class ReceivedPayment
	{
		public int PaymentId { get; set; }
		public int PayerNumber { get; set; }
		public decimal Amount { get; set; }
		public PaymentResult Result { get; set; }
        public PaymentReciever Receiver { get; set; }
        public PaymentPeriodCode PeriodCode { get; set; }
        public PaymentType Type { get; set; }
        public int? NumberOfReoccuringTransactionsLeft { get; set; }
        public string Reference { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedDate { get; set; }
	}
}