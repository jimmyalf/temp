using System;
using Spinit.Wp.Synologen.Autogiro.Helpers;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class AutogiroLineParserService : IAutogiroLineParserService
	{
		public Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Payment ReadPaymentLine(string line) 
		{
			return new Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.Payment
			{
				Type = line.ReadFrom(1).To(2).ToInt().ToEnum<PaymentType>(),
				PaymentDate = line.ReadFrom(3).To(10).ParseDate(),
				PeriodCode = line.ReadFrom(11).To(11).ParseEnum<PeriodCode>(),
				NumberOfReoccuringTransactionsLeft = line.ReadFrom(12).To(14).ParseNullable<int>(StringExtensions.ToInt),
				Transmitter = new Payer{CustomerNumber = line.ReadFrom(16).To(31).TrimStart('0')},
				Amount = line.ReadFrom(32).To(43).ParseAmount(),
				Reciever = new PaymentReciever{BankgiroNumber = line.ReadFrom(44).To(53).TrimStart('0')},
				Reference = line.ReadFrom(54).To(69).TrimEnd(' '),
				Result = line.ReadFrom(80).To(80).ParsePaymentResult()
			};
		}

		public string WritePaymentPostLine(Payment payment) 
		{
			return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
				 payment.Type.ToInteger().ToString().PadLeft(2, '0'),
				 payment.PaymentDate.ToString("yyyyMMdd"),
				 payment.PeriodCode.ToInteger(),
				 Pad(4),
				 payment.Transmitter.CustomerNumber.PadLeft(16, '0'),
				 payment.Amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture).Replace(".", "").PadLeft(12, '0'),
				 payment.Reciever.BankgiroNumber.PadLeft(10, '0'),
				 payment.Reference.PadRight(16),
				 Pad(11)
			);
		}

		public string WritePaymentHeaderLine(PaymentsFile paymentsFile) 
		{ 
			return String.Format("01{0}AUTOGIRO{1}{2}{3}{4}",
                 paymentsFile.WriteDate.ToString("yyyyMMdd"),
                 Pad(44),
                 paymentsFile.Reciever.CustomerNumber.PadLeft(6, '0'),
                 paymentsFile.Reciever.BankgiroNumber.PadLeft(10, '0'),
                 Pad(2)
			);
		}

		public string WriteConsentPostLine(Consent consent)
		{
			return String.Format("{0}{1}{2}{3}{4}{5}", 
                 consent.Type.ToInteger().ToString().PadLeft(2, '0'), 
                 consent.Reciever.BankgiroNumber.PadLeft(10, '0'), 
                 consent.Transmitter.CustomerNumber.PadLeft(16, '0'), 
                 (consent.Type == ConsentType.New) ? consent.AccountAndClearingNumber.PadLeft(16, '0') : String.Empty,
                 (consent.Type == ConsentType.New) ? consent.With(x => x.PersonalIdNumber) ??					  
                                                     consent.With(x => x.OrgNumber).Return(x => x.PadLeft(12, '0'), String.Empty) : String.Empty,
                 Pad(24)
			);
		}

		public string WriteConsentHeaderLine(ConsentsFile consentsFile)
		{
			return String.Format("01{0}AUTOGIRO{1}{2}{3}{4}",
                 consentsFile.WriteDate.ToString("yyyyMMdd"),
                 Pad(44),
                 consentsFile.Reciever.CustomerNumber.PadLeft(6, '0'),
                 consentsFile.Reciever.BankgiroNumber.PadLeft(10, '0'),
                 Pad(2)
			);
		}

		private static string Pad(int totalWidth)
		{
			return String.Empty.PadLeft(totalWidth,' ');
		}
	}
}