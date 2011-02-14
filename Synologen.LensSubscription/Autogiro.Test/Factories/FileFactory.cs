using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;

namespace Spinit.Wpc.Synologen.Autogiro.Test.Factories
{
	public static class FileFactory
	{
		public static ConsentsFile GetConsentsFile()
		{
			const string recieverBankGiroNumber = "9912346";
			var reciever = new PaymentReciever { BankgiroNumber = recieverBankGiroNumber, CustomerNumber = "471117" };
			return new ConsentsFile
			{
				Reciever = reciever,
				WriteDate = new DateTime(2004, 10, 15),
				Posts = new[]
				{
					new Consent{Type = ConsentType.New, RecieverBankgiroNumber = recieverBankGiroNumber, Transmitter = new Payer{CustomerNumber = "101"}, Account = new Account{ ClearingNumber = "3300", AccountNumber = "1212121212" }, PersonalIdNumber = "191212121212"},
					new Consent{Type = ConsentType.New, RecieverBankgiroNumber = recieverBankGiroNumber, Transmitter = new Payer{CustomerNumber = "102"}, Account = new Account{ ClearingNumber = "8901", AccountNumber = "3232323232" }, OrgNumber = "5556000521"},
					new Consent{Type = ConsentType.New, RecieverBankgiroNumber = recieverBankGiroNumber, Transmitter = new Payer{CustomerNumber = "103"}, Account = new Account{ ClearingNumber = "5001", AccountNumber = "1000020" }, PersonalIdNumber = "196803050000"},
					new Consent{Type = ConsentType.New, RecieverBankgiroNumber = recieverBankGiroNumber, Transmitter = new Payer{CustomerNumber = "102"}, Account = new Account{ ClearingNumber = "9918", AccountNumber = "2010150" }, PersonalIdNumber = "194608170000"},
					new Consent{Type = ConsentType.New, RecieverBankgiroNumber = recieverBankGiroNumber, Transmitter = new Payer{CustomerNumber = "102"}, Account = new Account{ ClearingNumber = "9918", AccountNumber = "2010114" }, PersonalIdNumber = "193701270000"},
					new Consent{Type = ConsentType.Cancellation, RecieverBankgiroNumber = recieverBankGiroNumber, Transmitter = new Payer{CustomerNumber = "242"}}
				}
			};
		}

		public static PaymentsFile GetPaymentsFile()
		{
			const string recieverBankGiroNumber = "9912346";
			var reciever = new PaymentReciever { BankgiroNumber = recieverBankGiroNumber, CustomerNumber = "471117" };
			return new PaymentsFile
			{
				Reciever = reciever,
				WriteDate = new DateTime(2004, 10, 26),
				Posts = new[]
				{
					new Payment{Type = PaymentType.Debit, PaymentDate = new DateTime(2004,10,27), PeriodCode = PeriodCode.PaymentOnceOnSelectedDate, Transmitter = new Payer{CustomerNumber = "1020304051"}, Amount = 750, Reference = "ÅRSKORT-2005", RecieverBankgiroNumber = recieverBankGiroNumber},
					new Payment{Type = PaymentType.Debit, PaymentDate = new DateTime(2004,10,27), PeriodCode = PeriodCode.PaymentOnceOnSelectedDate, Transmitter = new Payer{CustomerNumber = "2030405062"}, Amount = 250, Reference = "KVARTAL-2005", RecieverBankgiroNumber = recieverBankGiroNumber},
					new Payment{Type = PaymentType.Credit, PaymentDate = new DateTime(2004,10,28), PeriodCode = PeriodCode.PaymentOnceOnSelectedDate, Transmitter = new Payer{CustomerNumber = "3040506073"}, Amount = 125, Reference = "ÅTERBET", RecieverBankgiroNumber = recieverBankGiroNumber},
				}
			};
		}
	}
}