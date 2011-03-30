using System;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;
using Account=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.Account;

namespace Synologen.Lenssubscription.BGServiceCoordinator.AcceptanceTest.TestHelpers
{
	public static class Factory 
	{
		public static string ConcatenateFileSections(params Func<string>[] createFileSectionFunctions) 
		{
			var output = String.Empty;
			foreach (var func in createFileSectionFunctions)
			{
				output += func.Invoke() + "\r\n";
			}
			return output.TrimEnd('\r', '\n');
		}

		public static string GetReceivedPaymentData()
		{
			return
				@"0120041027AUTOGIRO9900                                        4711170009912346  
82200410280    000000000000100100000002430000099123460809001                                       
82200410280    000000000000100200000003840000099123460809002                                       
82200410280    000000000000100400000003350000099123460809004                                       
82200410280    000000000000100500000004620000099123460809005                                       
82200410280    000000000000100600000001720000099123460809006                                       
82200410280    000000000000100700000004840000099123460809007                                       
82200410280    000000000000100800000003140000099123460809008                                       
82200410280    000000000000100900000001120000099123460809009                                       
82200410280    000000000000101000000004870000099123460809010                                       
82200410280    000000000000101100000004340000099123460809011                                        
82200410280    000000000000101200000003370000099123460809012                                        
32200410280    000000000000101400000168740000099123460809745                                         
82200410280    000000000000101300000002530000099123460809013                   2
82200410280    000000000000101400000009690000099123460809014                   1
82200410280    000000000000101500000004890000099123460809015                   9
82200410280    00000000000010150000000489000009912346RÂ‰ˆ≈ƒ÷                   9
09200410279900              0000016874000000010000140000000000547500000000000000";
		}

		public static string GetReceivedPaymentData(int payerId)
		{
			var formattedPayerId = payerId.ToString().PadLeft(16);
			return
				@"0120041027AUTOGIRO9900                                        4711170009912346  
82200410280    {PayerId}00000002430000099123460809001                                       
82200410280    {PayerId}00000003840000099123460809002                                       
82200410280    {PayerId}00000003350000099123460809004                                       
82200410280    {PayerId}00000004620000099123460809005                                       
82200410280    {PayerId}00000001720000099123460809006                                       
82200410280    {PayerId}00000004840000099123460809007                                       
82200410280    {PayerId}00000003140000099123460809008                                       
82200410280    {PayerId}00000001120000099123460809009                                       
82200410280    {PayerId}00000004870000099123460809010                                       
82200410280    {PayerId}00000004340000099123460809011                                        
82200410280    {PayerId}00000003370000099123460809012                                        
32200410280    {PayerId}00000168740000099123460809745                                         
82200410280    {PayerId}00000002530000099123460809013                   2
82200410280    {PayerId}00000009690000099123460809014                   1
82200410280    {PayerId}00000004890000099123460809015                   9
09200410279900              0000016874000000010000140000000000547500000000000000"
					.Replace("{PayerId}",formattedPayerId);
		}

		public static string GetReceivedConsentData()
		{
			return @"012004011899000009912346AG-MEDAVI                                                                                             
7300099123460000000000023344330012121212000019121212121200000041020041018000000 
7300099123460000000000034433890132323211100000555600052100000043220041018041026 
7300099123460000000000042233500100123560000019680305111100000043220041018041026 
7300099123460000000000052244700100000123456719460817222200000043220041018041026 
7300099123460000000000061155134800000987600019461017333300000043220041018041026 
7300099123460000000000044333600000123456777019490730444400000041020041018000000 
7300099123460000195809010000                                 033320041018000000 
092004101899000000007                                                                                                                     ";
		}

		public static string GetReceivedConsentData(int payerId)
		{
			var formattedPayerId = payerId.ToString().PadLeft(16);
			return @"012004011899000009912346AG-MEDAVI                                                                                             
730009912346{PayerId}330012121212000019121212121200000041020041018000000 
730009912346{PayerId}890132323211100000555600052100000043220041018041026 
730009912346{PayerId}500100123560000019680305111100000043220041018041026 
730009912346{PayerId}700100000123456719460817222200000043220041018041026 
730009912346{PayerId}134800000987600019461017333300000043220041018041026 
730009912346{PayerId}600000123456777019490730444400000041020041018000000 
730009912346{PayerId}                                 033320041018000000 
092004101899000000007                                                                                                                     "
				.Replace("{PayerId}",formattedPayerId);
		}

		public static string GetReceivedErrorData()
		{
			return @"0120041022AUTOGIRO9900FELLISTA REG.KONTRL                     4711170009912346  
82200410230   0000000000000101000000050000                01                                       
82200410230   0000000000000102000000020000                03                                       
32200410230   0000000000000103000000010000                02                                       
82200410230   0000000000000104000000015000TESTREF         07                                       
09200410229900000001000000010000000003000000085000                                                           ";
		}

		public static ReceivedFileSection GetReceivedConsentFileSection(int payerId) 
		{
			return new ReceivedFileSection
			{
				CreatedDate = new DateTime(2011, 03, 29),
				SectionData = GetReceivedConsentData(payerId),
				Type = SectionType.ReceivedConsents,
				TypeName = SectionType.ReceivedConsents.GetEnumDisplayName()
			};
		}

		public static ReceivedFileSection GetReceivedPaymentFileSection(int payerId) 
		{ 
			return new ReceivedFileSection
			{
				CreatedDate = new DateTime(2011, 03, 29),
				SectionData = GetReceivedPaymentData(payerId),
				Type = SectionType.ReceivedPayments,
				TypeName = SectionType.ReceivedPayments.GetEnumDisplayName()
			};
		}

		public static BGConsentToSend GetConsentToSend(AutogiroPayer payer) 
		{
			return new BGConsentToSend
			{
				Account = new Account { AccountNumber = "1234567890", ClearingNumber = "1234" },
				OrgNumber = null,
				Payer = payer,
				PersonalIdNumber = "197501242858",
				SendDate = null,
				Type = ConsentType.New
			};
		}

		public static BGPaymentToSend GetPaymentToSend(AutogiroPayer payer) 
		{
			return new BGPaymentToSend
			{
				Amount = 548M,
				Payer = payer,
				PaymentDate = new DateTime(2011, 03, 25),
				PaymentPeriodCode = PaymentPeriodCode.PaymentOnceOnSelectedDate,
				Reference = "Reference ABC",
				SendDate = null,
				Type = PaymentType.Debit
			};
		}

		public static string GetConsentData(BGConsentToSend consent, string customerNumber, string bankgiroNumber) 
		{
			return new StringBuilder()
				.AppendLine("01{WriteDate}AUTOGIRO                                            {CustomerNumber}{BankgiroNumber}  ")
				.AppendLine("04{BankgiroNumber}{PayerNumber}{Clearing}{AccountNumber}{PersonalIdNumber}                        ")
				.ToString().TrimEnd('\r','\n')
				.Replace("{WriteDate}", DateTime.Now.ToString("yyyyMMdd"))
				.Replace("{CustomerNumber}", customerNumber.PadLeft(6,'0'))
				.Replace("{BankgiroNumber}", bankgiroNumber.PadLeft(10,'0'))
				.Replace("{PayerNumber}", consent.Payer.Id.ToString().PadLeft(16,'0'))
				.Replace("{Clearing}", consent.Account.ClearingNumber)
				.Replace("{AccountNumber}", consent.Account.AccountNumber.PadLeft(12,'0'))
				.Replace("{PersonalIdNumber}", consent.PersonalIdNumber);
		}

		public static string GetPaymentData(BGPaymentToSend payment, string customerNumber, string bankgiroNumber) 
		{
			return new StringBuilder()
				.AppendLine("01{WriteDate}AUTOGIRO                                            {CustomerNumber}{BankgiroNumber}  ")
				.AppendLine("82{PaymentDate}0    {PayerNumber}{Amount}{BankgiroNumber}{Referens}           ")
				.ToString().TrimEnd('\r','\n')
				.Replace("{WriteDate}", DateTime.Now.ToString("yyyyMMdd"))
				.Replace("{CustomerNumber}", customerNumber.PadLeft(6,'0'))
				.Replace("{BankgiroNumber}", bankgiroNumber.PadLeft(10,'0'))
				.Replace("{PaymentDate}",payment.PaymentDate.ToString("yyyyMMdd"))
				.Replace("{PayerNumber}", payment.Payer.Id.ToString().PadLeft(16,'0'))
				.Replace("{Amount}", payment.Amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture).Replace(".", "").PadLeft(12,'0'))
				.Replace("{Referens}", payment.Reference.PadRight(16,' '));
		}

		public static FileSectionToSend GetConsentFileSectionToSend() 
		{
			return new FileSectionToSend
			{
				CreatedDate = new DateTime(2011, 03, 30),
				SectionData = GetConsentToSendData(),
				SentDate = null,
				Type = SectionType.ConsentsToSend,
			};
		}


		public static FileSectionToSend GetPaymentFileSectionToSend() 
		{
			return new FileSectionToSend
			{
				CreatedDate = new DateTime(2011, 03, 30),
				SectionData = GetPaymentToSendData(),
				SentDate = null,
				Type = SectionType.PaymentsToSend,
			};
		}

		public static string GetConsentToSendData() 
		{
			return @"0120041015AUTOGIRO                                            4711170009912346  
04000991234600000000000001013300001212121212191212121212                                               
04000991234600000000000001028901003232323232005556000521                                               
04000991234600000000000001035001000001000020196803050000                                               
04000991234600000000000001029918000002010150194608170000                                               
04000991234600000000000001029918000002010114193701270000                    AV   
0300099123460000000000000242                                                    ";
		}

		public static string GetPaymentToSendData() 
		{
			return @"0120041026AUTOGIRO                                            4711170009912346  
82200410270    00000010203040510000000750000009912346≈RSKORT-2005                             
82200410270    00000020304050620000000250000009912346KVARTAL-2005                             
32200410280    00000030405060730000000125000009912346≈TERBET                    ";
		}
	}
}