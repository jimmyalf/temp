using System;
using System.Text;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Autogiro.Test
{
	public class AutogiroParserService : IAutogiroParserService {
		public string ParseConsents(ConsentsFile file)
		{
			Func<ConsentsFile, string> headerParser = consentFile =>  String.Format("01{0}AUTOGIRO{1}{2}{3}{4}",
				consentFile.WriteDate.ToString("yyyyMMdd"),
				Pad(44),
				consentFile.Reciever.CustomerNumber.PadLeft(6, '0'),
				consentFile.Reciever.BankgiroNumber.PadLeft(10, '0'),
				Pad(2)
			);

			Func<Consent, string> lineParser = consent => String.Format("{0}{1}{2}{3}{4}{5}", 
                consent.Type.ToInteger().ToString().PadLeft(2, '0'), 
                consent.Reciever.BankgiroNumber.PadLeft(10, '0'), 
                consent.Transmitter.CustomerNumber.PadLeft(16, '0'), 
                (consent.Type == ConsentType.New) ? consent.AccountAndClearingNumber.PadLeft(16, '0') : String.Empty,
                (consent.Type == ConsentType.New) ? consent.With(x => x.PersonalIdNumber) ??					  
                                                    consent.With(x => x.OrgNumber).Return(x => x.PadLeft(12, '0'), String.Empty) : String.Empty,
                Pad(24)
          	);
			var builder = new StringBuilder().AppendLine(headerParser(file));
			file.Posts.Each(consent => builder.AppendLine(lineParser(consent)));
			return builder.ToString().TrimEnd(new []{'\r','\n'});
		}
		public string ParsePayments(PaymentsFile file)
		{
			Func<PaymentsFile, string> headerParser = paymentsFile =>  String.Format("01{0}AUTOGIRO{1}{2}{3}{4}",
				 paymentsFile.WriteDate.ToString("yyyyMMdd"),
				 Pad(44),
				 paymentsFile.Reciever.CustomerNumber.PadLeft(6, '0'),
				 paymentsFile.Reciever.BankgiroNumber.PadLeft(10, '0'),
				 Pad(2)
       		);

			Func<Payment, string> lineParser = payment => String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
				payment.Type.ToInteger().ToString().PadLeft(2, '0'),
				payment.PaymentDate.ToString("yyyyMMdd"),
				payment.PeriodCode.ToInteger().ToString(),
				Pad(4),
				payment.Transmitter.CustomerNumber.PadLeft(16, '0'),
				payment.Amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture).Replace(".", "").PadLeft(12, '0'),
				payment.Reciever.BankgiroNumber.PadLeft(10, '0'),
				payment.Reference.PadRight(16),
				Pad(11)
			);
			var builder = new StringBuilder().AppendLine(headerParser(file));
			file.Posts.Each(consent => builder.AppendLine(lineParser(consent)));
			return builder.ToString().TrimEnd(new []{'\r','\n'});
		}
		public Core.Domain.Model.Autogiro.Recieve.PaymentsFile ReadPayments(string fileContents) { throw new NotImplementedException(); }
		public Core.Domain.Model.Autogiro.Recieve.ConsentsFile ReadConsents(string fileContents) { throw new NotImplementedException(); }
		public Core.Domain.Model.Autogiro.Recieve.ErrorsFile ReadErrors(string fileContents) { throw new NotImplementedException(); }
		private static string Pad(int totalWidth)
		{
			return String.Empty.PadLeft(totalWidth,' ');
		}
	}
}