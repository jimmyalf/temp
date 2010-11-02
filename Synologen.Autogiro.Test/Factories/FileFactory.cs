using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;

namespace Synologen.Autogiro.Test.Factories
{
	public static  class FileFactory
	{
		public static ConsentsFile GetConsentsFile()
		{
			var reciever = new PaymentReciever { BankgiroNumber = "9912346", CustomerNumber = "471117" };
			return new ConsentsFile
			{
				Reciever = reciever,
				WriteDate = new DateTime(2004, 10, 15),
				Posts = new[]
				{
					new Consent{Type = ConsentType.New, Reciever = reciever, Transmitter = new Payer{CustomerNumber = "101"}, AccountAndClearingNumber = "3300001212121212", PersonalIdNumber = "191212121212"},
					new Consent{Type = ConsentType.New, Reciever = reciever, Transmitter = new Payer{CustomerNumber = "102"}, AccountAndClearingNumber = "8901003232323232", OrgNumber = "5556000521"},
					new Consent{Type = ConsentType.New, Reciever = reciever, Transmitter = new Payer{CustomerNumber = "103"}, AccountAndClearingNumber = "5001000001000020", PersonalIdNumber = "196803050000"},
					new Consent{Type = ConsentType.New, Reciever = reciever, Transmitter = new Payer{CustomerNumber = "102"}, AccountAndClearingNumber = "9918000002010150", PersonalIdNumber = "194608170000"},
					new Consent{Type = ConsentType.New, Reciever = reciever, Transmitter = new Payer{CustomerNumber = "102"}, AccountAndClearingNumber = "9918000002010114", PersonalIdNumber = "193701270000"},
					new Consent{Type = ConsentType.Cancellation, Reciever = reciever, Transmitter = new Payer{CustomerNumber = "242"}}
				}
			};
		}

		public static PaymentsFile GetPaymentsFile() 
		{ 
			var reciever = new PaymentReciever { BankgiroNumber = "9912346", CustomerNumber = "471117" };
			return new PaymentsFile
			{
				Reciever = reciever,
				WriteDate = new DateTime(2004, 10, 26),
				Posts = new[]
				{
					new Payment{Type = PaymentType.Debit, PaymentDate = new DateTime(2004,10,27), PeriodCode = PeriodCode.PaymentOnceOnSelectedDate, Transmitter = new Payer{CustomerNumber = "1020304051"}, Amount = 750, Reference = "ÅRSKORT-2005", Reciever = reciever},
					new Payment{Type = PaymentType.Debit, PaymentDate = new DateTime(2004,10,27), PeriodCode = PeriodCode.PaymentOnceOnSelectedDate, Transmitter = new Payer{CustomerNumber = "2030405062"}, Amount = 250, Reference = "KVARTAL-2005", Reciever = reciever},
					new Payment{Type = PaymentType.Credit, PaymentDate = new DateTime(2004,10,28), PeriodCode = PeriodCode.PaymentOnceOnSelectedDate, Transmitter = new Payer{CustomerNumber = "3040506073"}, Amount = 125, Reference = "ÅTERBET", Reciever = reciever},
				}
			};
		}
		/*
0120041026AUTOGIRO                                            4711170009912346  
82200410270    00000010203040510000000750000009912346ÅRSKORT-2005                             
82200410270    00000020304050620000000250000009912346KVARTAL-2005                             
32200410280    00000030405060730000000125000009912346ÅTERBET         
		 */
	}
}