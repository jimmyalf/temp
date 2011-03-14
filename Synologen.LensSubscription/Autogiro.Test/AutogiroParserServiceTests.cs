using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.Autogiro.Readers;
using Synologen.LensSubscription.Autogiro.Test.Factories;
using Synologen.LensSubscription.Autogiro.Writers;

namespace Synologen.LensSubscription.Autogiro.Test
{
	[TestFixture]
	[Category("AutogiroParsingTester")]
	public class When_writing_consents
	{
		private readonly string _parsedFileContent;

		public When_writing_consents()
		{
			var concentsFile = FileFactory.GetConsentsFile();
			var fileWriter = new ConsentsFileWriter();
			_parsedFileContent = fileWriter.Write(concentsFile);
		}

		[Test]
		public void Parsed_file_contents_contains_expected_data()
		{
			var expectedOutput = FileContentFactory.GetLayoutA();
			_parsedFileContent.ShouldBe(expectedOutput);
		}
	}

	[TestFixture]
	[Category("AutogiroParsingTester")]
	public class When_writing_payments
	{
		private readonly string _parsedFileContent;

		public When_writing_payments()
		{
			var paymentsFile = FileFactory.GetPaymentsFile();
			var fileWriter = new PaymentsFileWriter();
			_parsedFileContent = fileWriter.Write(paymentsFile);
		}

		[Test]
		public void Parsed_file_contents_contains_expected_data()
		{
			var expectedOutput = FileContentFactory.GetLayoutB();
			_parsedFileContent.ShouldBe(expectedOutput);
		}
	}

	[TestFixture]
	[Category("AutogiroParsingTester")]
	public class When_reading_payments
	{
		private readonly PaymentsFile _paymentsFile;
		private readonly int _expectedNumberOfPosts;
		private readonly PaymentReciever _expectedReciever;
		private readonly DateTime _expectedWriteDate;
		private readonly DateTime _expectedPaymentDate;

		public When_reading_payments()
		{
			_expectedNumberOfPosts = 15;
			_expectedPaymentDate = new DateTime(2004, 10, 28);
			_expectedWriteDate = new DateTime(2004, 10, 27);
			_expectedReciever = new PaymentReciever { BankgiroNumber = "9912346", CustomerNumber = "471117" };
			var fileContent = FileContentFactory.GetLayoutD();
			var fileReader = new PaymentsFileReader();
			_paymentsFile = fileReader.Read(fileContent);
		}

		[Test]
		public void Payments_model_contains_expected_data()
		{
			_paymentsFile.WriteDate.ShouldBe(_expectedWriteDate);
			_paymentsFile.Reciever.ShouldBe(_expectedReciever);
			_paymentsFile.NumberOfCreditsInFile.ShouldBe(1);
			_paymentsFile.NumberOfDebitsInFile.ShouldBe(14);
			_paymentsFile.TotalCreditAmountInFile.ShouldBe(16874);
			_paymentsFile.TotalDebitAmountInFile.ShouldBe(5475);
			_paymentsFile.Posts.Count().ShouldBe(_expectedNumberOfPosts);
			_paymentsFile.Posts.For((index,post) =>
			{
				post.PaymentPeriodCode.ShouldBe(PaymentPeriodCode.PaymentOnceOnSelectedDate);
				post.PaymentDate.ShouldBe(_expectedPaymentDate);
				post.NumberOfReoccuringTransactionsLeft.ShouldBe(null);
				post.Reciever.BankgiroNumber.ShouldBe(_expectedReciever.BankgiroNumber);
				switch (index)
				{
					case 11: post.Type.ShouldBe(PaymentType.Credit); break;
					default: post.Type.ShouldBe(PaymentType.Debit); break;
				}
			});

			_paymentsFile.Posts.ForElementAtIndex(0, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1001");
				post.Amount.ShouldBe(243);
				post.Reference.ShouldBe("0809001");
			});

			_paymentsFile.Posts.ForElementAtIndex(1, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1002");
				post.Amount.ShouldBe(384);
				post.Reference.ShouldBe("0809002");
			});

			_paymentsFile.Posts.ForElementAtIndex(2, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1004");
				post.Amount.ShouldBe(335);
				post.Reference.ShouldBe("0809004");
			});

			_paymentsFile.Posts.ForElementAtIndex(3, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1005");
				post.Amount.ShouldBe(462);
				post.Reference.ShouldBe("0809005");
			});

			_paymentsFile.Posts.ForElementAtIndex(4, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1006");
				post.Amount.ShouldBe(172);
				post.Reference.ShouldBe("0809006");
			});

			_paymentsFile.Posts.ForElementAtIndex(5, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1007");
				post.Amount.ShouldBe(484);
				post.Reference.ShouldBe("0809007");
			});

			_paymentsFile.Posts.ForElementAtIndex(6, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1008");
				post.Amount.ShouldBe(314);
				post.Reference.ShouldBe("0809008");
			});

			_paymentsFile.Posts.ForElementAtIndex(7, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1009");
				post.Amount.ShouldBe(112);
				post.Reference.ShouldBe("0809009");
			});

			_paymentsFile.Posts.ForElementAtIndex(8, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1010");
				post.Amount.ShouldBe(487);
				post.Reference.ShouldBe("0809010");
			});

			_paymentsFile.Posts.ForElementAtIndex(9, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1011");
				post.Amount.ShouldBe(434);
				post.Reference.ShouldBe("0809011");
			});

			_paymentsFile.Posts.ForElementAtIndex(10, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved); 
				post.Transmitter.CustomerNumber.ShouldBe("1012");
				post.Amount.ShouldBe(337);
				post.Reference.ShouldBe("0809012");
			});

			_paymentsFile.Posts.ForElementAtIndex(11, post =>
			{
				post.Result.ShouldBe(PaymentResult.Approved);
				post.Transmitter.CustomerNumber.ShouldBe("1014");
				post.Amount.ShouldBe(16874);
				post.Reference.ShouldBe("0809745");
			});

			_paymentsFile.Posts.ForElementAtIndex(12, post =>
			{
				post.Result.ShouldBe(PaymentResult.AGConnectionMissing);
				post.Transmitter.CustomerNumber.ShouldBe("1013");
				post.Amount.ShouldBe(253);
				post.Reference.ShouldBe("0809013");
			});

			_paymentsFile.Posts.ForElementAtIndex(13, post =>
			{
				post.Result.ShouldBe(PaymentResult.InsufficientFunds);
				post.Transmitter.CustomerNumber.ShouldBe("1014");
				post.Amount.ShouldBe(969);
				post.Reference.ShouldBe("0809014");
			});

			_paymentsFile.Posts.ForElementAtIndex(14, post =>
			{
				post.Result.ShouldBe(PaymentResult.WillTryAgain); 
				post.Transmitter.CustomerNumber.ShouldBe("1015");
				post.Amount.ShouldBe(489);
				post.Reference.ShouldBe("0809015");
			});
		}
	}

	[TestFixture]
	[Category("AutogiroParsingTester")]
	public class When_reading_consents
	{
		private readonly ConsentsFile _consentsFile;
		private readonly DateTime _expectedWriteDate;
		private readonly DateTime _expectedActionDate;
		private readonly DateTime _expectedConsentValidForDateDate;
		private readonly string _expectedRecieverBankgiroNumber;

		public When_reading_consents()
		{
			_expectedActionDate = new DateTime(2004, 10, 18);
			_expectedConsentValidForDateDate = new DateTime(2004, 10, 26);
			_expectedWriteDate = new DateTime(2004, 01, 18);
			_expectedRecieverBankgiroNumber = "9912346";
			var fileContent = FileContentFactory.GetLayoutE();
			var fileReader = new ConsentsFileReader();
			_consentsFile = fileReader.Read(fileContent);
		}

		[Test]
		public void Consents_model_contains_expected_data()
		{
			_consentsFile.PaymentRecieverBankgiroNumber.ShouldBe(_expectedRecieverBankgiroNumber);
			_consentsFile.WriteDate.ShouldBe(_expectedWriteDate);
			_consentsFile.NumberOfItemsInFile.ShouldBe(7);
			_consentsFile.Posts.Each(post =>
			{
				post.RecieverBankgiroNumber.ShouldBe("9912346");
				post.ActionDate.ShouldBe(_expectedActionDate);
				post.InformationCode.ShouldBe(ConsentInformationCode.InitiatedByPaymentRecipient);
			});
			_consentsFile.Posts.ForElementAtIndex(0, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("23344");
				post.Account.ClearingNumber.ShouldBe("3300");
				post.Account.AccountNumber.ShouldBe("121212120000");
				post.PersonalIdNumber.ShouldBe("191212121212");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration);
				post.ConsentValidForDate.ShouldBe(null);
			});
			_consentsFile.Posts.ForElementAtIndex(1, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("34433");
				post.Account.ClearingNumber.ShouldBe("8901");
				post.Account.AccountNumber.ShouldBe("323232111000");
				post.PersonalIdNumber.ShouldBe(null);
				post.OrgNumber.ShouldBe("5556000521");
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.ConsentValidForDate.Value.ShouldBe(_expectedConsentValidForDateDate);
			});
			_consentsFile.Posts.ForElementAtIndex(2, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("42233");
				post.Account.ClearingNumber.ShouldBe("5001");
				post.Account.AccountNumber.ShouldBe("1235600000");
				post.PersonalIdNumber.ShouldBe("196803051111");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.ConsentValidForDate.Value.ShouldBe(_expectedConsentValidForDateDate);
			});
			_consentsFile.Posts.ForElementAtIndex(3, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("52244");
				post.Account.ClearingNumber.ShouldBe("7001");
				post.Account.AccountNumber.ShouldBe("1234567");
				post.PersonalIdNumber.ShouldBe("194608172222");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.ConsentValidForDate.Value.ShouldBe(_expectedConsentValidForDateDate);
			});
			_consentsFile.Posts.ForElementAtIndex(4, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("61155");
				post.Account.ClearingNumber.ShouldBe("1348");
				post.Account.AccountNumber.ShouldBe("9876000");
				post.PersonalIdNumber.ShouldBe("194610173333");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.ConsentValidForDate.Value.ShouldBe(_expectedConsentValidForDateDate);
			});
			_consentsFile.Posts.ForElementAtIndex(5, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("44333");
				post.Account.ClearingNumber.ShouldBe("6000");
				post.Account.AccountNumber.ShouldBe("1234567770");
				post.PersonalIdNumber.ShouldBe("194907304444");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration);
				post.ConsentValidForDate.ShouldBe(null);
			});
			_consentsFile.Posts.ForElementAtIndex(6, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("195809010000");
				post.Account.ClearingNumber.ShouldBe(null);
				post.Account.AccountNumber.ShouldBe(null);
				post.PersonalIdNumber.ShouldBe(null);
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.Canceled);
				post.ConsentValidForDate.ShouldBe(null);
			});
		}
	}

	[TestFixture]
	[Category("AutogiroParsingTester")]
	public class When_reading_complementary_consents
	{
		private readonly ConsentsFile _consentsFile;

		public When_reading_complementary_consents()
		{
			var fileContent = FileContentFactory.GetComplementaryLayoutE();
			var fileReader = new ConsentsFileReader();
			_consentsFile = fileReader.Read(fileContent);
		}

		[Test]
		public void Consents_model_contains_expected_comment_codes_and_information_codes()
		{
			_consentsFile.NumberOfItemsInFile.ShouldBe(18);
			_consentsFile.Posts.ForElementAtIndex(0, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentTurnedDownByBank);
				post.InformationCode.ShouldBe(ConsentInformationCode.InitiatedByPaymentRecipient);
			});
			_consentsFile.Posts.ForElementAtIndex(1, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentTurnedDownByPayer);
				post.InformationCode.ShouldBe(ConsentInformationCode.InitiatedByPaymentRecipient);
			});
			_consentsFile.Posts.ForElementAtIndex(2, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.AccountTypeNotApproved);
				post.InformationCode.ShouldBe(ConsentInformationCode.InitiatedByPayer);
			});
			_consentsFile.Posts.ForElementAtIndex(3, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentMissingInBankgiroConsentRegister);
				post.InformationCode.ShouldBe(ConsentInformationCode.InitiatedByPayer);
			});
			_consentsFile.Posts.ForElementAtIndex(4, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.IncorrectAccountOrPersonalIdNumber);
				post.InformationCode.ShouldBe(ConsentInformationCode.AnswerToNewAccountApplication);
			});
			_consentsFile.Posts.ForElementAtIndex(5, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentCanceledByBankgiro);
				post.InformationCode.ShouldBe(ConsentInformationCode.InitiatedByPayersBank);
			});
			_consentsFile.Posts.ForElementAtIndex(6, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentCanceledByBankgiroBecauseOfMissingStatement);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(7, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(8, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentTemporarilyStoppedByPayer);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(9, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.TemporaryConsentStopRevoked);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(10, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.IncorrectPersonalIdNumber);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(11, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.IncorrectPayerNumber);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(12, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.IncorrectAccountNumber);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(13, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.MaxAmountNotAllowed);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(14, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.IncorrectPaymentReceiverBankgiroNumber);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(15, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.PaymentReceiverBankgiroNumberMissing);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(16, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
			_consentsFile.Posts.ForElementAtIndex(17, post =>
			{
				post.CommentCode.ShouldBe(ConsentCommentCode.Canceled);
				post.InformationCode.ShouldBe(ConsentInformationCode.PaymentRecieversBankGiroAccountClosed);
			});
		}
	}

	[TestFixture]
	[Category("AutogiroParsingTester")]
	public class When_reading_errors
	{
		private readonly ErrorsFile _errorsFile;
		private readonly DateTime _expectedWriteDate;
		private readonly DateTime _expectedPaymentDate;
		private readonly int _expectedNumberOfPosts;
		private readonly PaymentReciever _expectedReciever;

		public When_reading_errors()
		{
			_expectedNumberOfPosts = 4;
			_expectedWriteDate = new DateTime(2004, 10, 22);
			_expectedPaymentDate =  new DateTime(2004, 10, 23);
			_expectedReciever = new PaymentReciever { BankgiroNumber = "9912346", CustomerNumber = "471117" };
			var fileContent = FileContentFactory.GetLayoutF();
			var fileReader = new ErrorFileReader();
			_errorsFile = fileReader.Read(fileContent);
		}

		[Test]
		public void Errors_model_contains_expected_data()
		{
			_errorsFile.WriteDate.ShouldBe(_expectedWriteDate);
			_errorsFile.Reciever.BankgiroNumber.ShouldBe(_expectedReciever.BankgiroNumber);
			_errorsFile.Reciever.CustomerNumber.ShouldBe(_expectedReciever.CustomerNumber);
			_errorsFile.NumberOfCreditsInFile.ShouldBe(1);
			_errorsFile.NumberOfDebitsInFile.ShouldBe(3);
			_errorsFile.TotalCreditAmountInFile.ShouldBe(100.00M);
			_errorsFile.TotalDebitAmountInFile.ShouldBe(850.00M);
			_errorsFile.Posts.Count().ShouldBe(_expectedNumberOfPosts);

			_errorsFile.Posts.For((index, post) =>
			{
				post.PaymentPeriodCode.ShouldBe(PaymentPeriodCode.PaymentOnceOnSelectedDate);
				post.PaymentDate.ShouldBe(_expectedPaymentDate);
				post.NumberOfReoccuringTransactionsLeft.ShouldBe(null);
			});

			_errorsFile.Posts.ForElementAtIndex(0, post =>
			{
				post.Amount.ShouldBe(500M);
				post.Reference.ShouldBe(string.Empty);
				post.CommentCode.ShouldBe(ErrorCommentCode.ConsentMissing);
				post.Transmitter.CustomerNumber.ShouldBe("101");
				post.Type.ShouldBe(PaymentType.Debit);
			});

			_errorsFile.Posts.ForElementAtIndex(1, post =>
			{
				post.Amount.ShouldBe(200M);
				post.Reference.ShouldBe(string.Empty);
				post.CommentCode.ShouldBe(ErrorCommentCode.ConsentStopped);
				post.Transmitter.CustomerNumber.ShouldBe("102");
				post.Type.ShouldBe(PaymentType.Debit);
			});

			_errorsFile.Posts.ForElementAtIndex(2, post =>
			{
				post.Amount.ShouldBe(100M);
				post.Reference.ShouldBe(string.Empty);
				post.CommentCode.ShouldBe(ErrorCommentCode.AccountNotYetApproved);
				post.Transmitter.CustomerNumber.ShouldBe("103");
				post.Type.ShouldBe(PaymentType.Credit);
			});

			_errorsFile.Posts.ForElementAtIndex(3, post =>
			{
				post.Amount.ShouldBe(150M);
				post.Reference.ShouldBe("TESTREF");
				post.CommentCode.ShouldBe(ErrorCommentCode.NotYetDebitable);
				post.Transmitter.CustomerNumber.ShouldBe("104");
				post.Type.ShouldBe(PaymentType.Debit);
			});
		}
	}
}