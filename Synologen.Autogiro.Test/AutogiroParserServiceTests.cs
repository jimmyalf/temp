using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wp.Synologen.Autogiro;
using Spinit.Wpc.Synologen.Autogiro.Test.Factories;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.ShouldlyExtensions;

namespace Spinit.Wpc.Synologen.Autogiro.Test
{
	[TestFixture]
	[Category("AutogiroParserServiceTester")]
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
	[Category("AutogiroParserServiceTester")]
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
	[Category("AutogiroParserServiceTester")]
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
	    	var fileReader = new PaymentsFileReader(fileContent);
	    	_paymentsFile = fileReader.Read();
	    }

	    [Test]
	    public void Payments_model_contains_expected_data()
	    {
	        _paymentsFile.WriteDate.ShouldBe(_expectedWriteDate, DateTimeTolerance.SameYearMonthDate);
	        _paymentsFile.Reciever.ShouldBe(_expectedReciever);
			_paymentsFile.NumberOfCreditsInFile.ShouldBe(1);
			_paymentsFile.NumberOfDebitsInFile.ShouldBe(14);
			_paymentsFile.TotalCreditAmountInFile.ShouldBe(16874);
			_paymentsFile.TotalDebitAmountInFile.ShouldBe(5475);
	        _paymentsFile.Posts.Count().ShouldBe(_expectedNumberOfPosts);
			_paymentsFile.Posts.For((index,post) =>
			{
				post.PeriodCode.ShouldBe(PeriodCode.PaymentOnceOnSelectedDate);
				post.PaymentDate.ShouldBe(_expectedPaymentDate);
				post.NumberOfReoccuringTransactionsLeft.ShouldBe(null);
				post.Reciever.BankgiroNumber.ShouldBe(_expectedReciever.BankgiroNumber);
				switch (index)
				{
					case 11: 
						post.Result.ShouldBe(PaymentResult.Approved);
						post.Type.ShouldBe(PaymentType.Credit); 
						break;
					case 12: 
						post.Result.ShouldBe(PaymentResult.AGConnectionMissing);
						post.Type.ShouldBe(PaymentType.Debit);
						break;
					case 13: 
						post.Result.ShouldBe(PaymentResult.InsufficientFunds); 
						post.Type.ShouldBe(PaymentType.Debit);
						break;
					case 14: 
						post.Result.ShouldBe(PaymentResult.WillTryAgain); 
						post.Type.ShouldBe(PaymentType.Debit);
						break;
					default: 
						post.Result.ShouldBe(PaymentResult.Approved); 
						post.Type.ShouldBe(PaymentType.Debit);
						break;
				}
			});

	    	_paymentsFile.Posts.ForElementAtIndex(0, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1001");
				post.Amount.ShouldBe(243);
				post.Reference.ShouldBe("0809001");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(1, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1002");
				post.Amount.ShouldBe(384);
				post.Reference.ShouldBe("0809002");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(2, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1004");
				post.Amount.ShouldBe(335);
				post.Reference.ShouldBe("0809004");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(3, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1005");
				post.Amount.ShouldBe(462);
				post.Reference.ShouldBe("0809005");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(4, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1006");
				post.Amount.ShouldBe(172);
				post.Reference.ShouldBe("0809006");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(5, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1007");
				post.Amount.ShouldBe(484);
				post.Reference.ShouldBe("0809007");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(6, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1008");
				post.Amount.ShouldBe(314);
				post.Reference.ShouldBe("0809008");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(7, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1009");
				post.Amount.ShouldBe(112);
				post.Reference.ShouldBe("0809009");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(8, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1010");
				post.Amount.ShouldBe(487);
				post.Reference.ShouldBe("0809010");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(9, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1011");
				post.Amount.ShouldBe(434);
				post.Reference.ShouldBe("0809011");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(10, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1012");
				post.Amount.ShouldBe(337);
				post.Reference.ShouldBe("0809012");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(11, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1014");
				post.Amount.ShouldBe(16874);
				post.Reference.ShouldBe("0809745");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(12, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1013");
				post.Amount.ShouldBe(253);
				post.Reference.ShouldBe("0809013");
			});

	    	_paymentsFile.Posts.ForElementAtIndex(13, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1014");
				post.Amount.ShouldBe(969);
				post.Reference.ShouldBe("0809014");
			});

    		_paymentsFile.Posts.ForElementAtIndex(14, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("1015");
				post.Amount.ShouldBe(489);
				post.Reference.ShouldBe("0809015");
			});
	    }
	}

	[TestFixture]
	[Category("AutogiroParserServiceTester")]
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
	    	var fileReader = new ConsentsFileReader(fileContent);
	    	_consentsFile = fileReader.Read();
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
				post.ClearingNumber.ShouldBe("3300");
				post.AccountNumber.ShouldBe("121212120000");
				post.PersonalIdNumber.ShouldBe("191212121212");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation);
				post.ConsentValidForDate.ShouldBe(null);
			});
	    	_consentsFile.Posts.ForElementAtIndex(1, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("34433");
				post.ClearingNumber.ShouldBe("8901");
				post.AccountNumber.ShouldBe("323232111000");
				post.PersonalIdNumber.ShouldBe(null);
				post.OrgNumber.ShouldBe("5556000521");
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.ConsentValidForDate.Value.ShouldBe(_expectedConsentValidForDateDate);
			});
	    	_consentsFile.Posts.ForElementAtIndex(2, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("42233");
				post.ClearingNumber.ShouldBe("5001");
				post.AccountNumber.ShouldBe("1235600000");
				post.PersonalIdNumber.ShouldBe("196803051111");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.ConsentValidForDate.Value.ShouldBe(_expectedConsentValidForDateDate);
			});
	    	_consentsFile.Posts.ForElementAtIndex(3, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("52244");
				post.ClearingNumber.ShouldBe("7001");
				post.AccountNumber.ShouldBe("1234567");
				post.PersonalIdNumber.ShouldBe("194608172222");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.ConsentValidForDate.Value.ShouldBe(_expectedConsentValidForDateDate);
			});
	    	_consentsFile.Posts.ForElementAtIndex(4, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("61155");
				post.ClearingNumber.ShouldBe("1348");
				post.AccountNumber.ShouldBe("9876000");
				post.PersonalIdNumber.ShouldBe("194610173333");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.NewConsent);
				post.ConsentValidForDate.Value.ShouldBe(_expectedConsentValidForDateDate);
			});
	    	_consentsFile.Posts.ForElementAtIndex(5, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("44333");
				post.ClearingNumber.ShouldBe("6000");
				post.AccountNumber.ShouldBe("1234567770");
				post.PersonalIdNumber.ShouldBe("194907304444");
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation);
				post.ConsentValidForDate.ShouldBe(null);
			});
	    	_consentsFile.Posts.ForElementAtIndex(6, post =>
			{
				post.Transmitter.CustomerNumber.ShouldBe("195809010000");
				post.ClearingNumber.ShouldBe(null);
				post.AccountNumber.ShouldBe(null);
				post.PersonalIdNumber.ShouldBe(null);
				post.OrgNumber.ShouldBe(null);
				post.CommentCode.ShouldBe(ConsentCommentCode.Canceled);
				post.ConsentValidForDate.ShouldBe(null);
			});
		}
	}
}