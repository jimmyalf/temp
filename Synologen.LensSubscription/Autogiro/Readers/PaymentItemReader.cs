using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.Autogiro.Helpers;

namespace Synologen.LensSubscription.Autogiro.Readers
{
	public class PaymentItemReader : IItemReader<Payment>
	{
		public Payment Read(string line)
		{
			return new Payment
			{
				Type = line.ReadFrom(1).To(2).ToInt().ToEnum<PaymentType>(),
				PaymentDate = line.ReadFrom(3).To(10).ParseDate(),
				PaymentPeriodCode = line.ReadFrom(11).To(11).ParseEnum<PaymentPeriodCode>(),
				NumberOfReoccuringTransactionsLeft = line.ReadFrom(12).To(14).ParseNullable<int>(StringExtensions.ToInt),
				Transmitter = new Payer{CustomerNumber = line.ReadFrom(16).To(31).TrimStart('0')},
				Amount = line.ReadFrom(32).To(43).ParseAmount(),
				Reciever = new PaymentReciever{BankgiroNumber = line.ReadFrom(44).To(53).TrimStart('0')},
				Reference = line.ReadFrom(54).To(69).TrimEnd(' '),
				Result = line.ReadFrom(80).To(80).ParsePaymentResult()
			};
		}
	}
}