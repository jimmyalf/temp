using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wp.Synologen.Autogiro;
using Spinit.Wpc.Synologen.Autogiro.Test.Factories;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.ShouldlyExtensions;

namespace Spinit.Wpc.Synologen.Autogiro.Test
{
	[TestFixture]
	[Category("AutogiroParserServiceTester")]
	public class When_parsing_consents_to_send
	{
		private readonly IAutogiroParserService _parserService;
		private readonly string _parsedFileContent;

		public When_parsing_consents_to_send()
		{
			var autogiroLineParserService = new AutogiroLineParserService();
			_parserService = new AutogiroParserService(autogiroLineParserService);
			var concentsFile = FileFactory.GetConsentsFile();
			_parsedFileContent = _parserService.ParseConsents(concentsFile);
		}

		[Test]
		public void Parsed_File_Contents_Contains_Expected_Data()
		{
			var expectedOutput = FileContentFactory.GetLayoutA();
			_parsedFileContent.ShouldBe(expectedOutput);
		}
	}

	[TestFixture]
	[Category("AutogiroParserServiceTester")]
	public class When_parsing_payments_to_send
	{
		private readonly IAutogiroParserService _parserService;
		private readonly string _parsedFileContent;

		public When_parsing_payments_to_send()
		{
			var autogiroLineParserService = new AutogiroLineParserService();
			_parserService = new AutogiroParserService(autogiroLineParserService);
			var paymentsFile = FileFactory.GetPaymentsFile();
			_parsedFileContent = _parserService.ParsePayments(paymentsFile);
		}

		[Test]
		public void Parsed_File_Contents_Contains_Expected_Data()
		{
			var expectedOutput = FileContentFactory.GetLayoutB();
			_parsedFileContent.ShouldBe(expectedOutput);
		}
	}

	
	[TestFixture]
	[Category("AutogiroParserServiceTester")]
	public class When_parsing_payments_from_filecontent
	{
	    private readonly IAutogiroParserService _parserService;
	    private readonly PaymentsFile _paymentsFile;
	    private readonly int _expectedNumberOfPosts;

	    public When_parsing_payments_from_filecontent()
	    {
	        var autogiroLineParserService = new AutogiroLineParserService();
	        _expectedNumberOfPosts = 15;
	        _parserService = new AutogiroParserService(autogiroLineParserService);
	        var fileContent = FileContentFactory.GetLayoutD();
	        _paymentsFile = _parserService.ReadPayments(fileContent);
	    }

	    [Test]
	    public void Parsed_File_Contains_Expected_Data()
	    {
	        var expectedReciever = new PaymentReciever { BankgiroNumber = "9912346", CustomerNumber = "471117" };
	        _paymentsFile.WriteDate.ShouldBe(new DateTime(2004,10,27), DateTimeTolerance.SameYearMonthDate);
	        _paymentsFile.Reciever.ShouldBe(expectedReciever);
			_paymentsFile.NumberOfCreditsInFile.ShouldBe(1);
			_paymentsFile.NumberOfDebitsInFile.ShouldBe(14);
			_paymentsFile.TotalCreditAmountInFile.ShouldBe(16874);
			_paymentsFile.TotalDebitAmountInFile.ShouldBe(5475);
	        _paymentsFile.Posts.Count().ShouldBe(_expectedNumberOfPosts);
			_paymentsFile.Posts.For((index,post) =>
			{
				post.PeriodCode.ShouldBe(PeriodCode.PaymentOnceOnSelectedDate);
				post.PaymentDate.ShouldBe(new DateTime(2004,10,28));
				post.NumberOfReoccuringTransactionsLeft.ShouldBe(null);
				post.Reciever.BankgiroNumber.ShouldBe(expectedReciever.BankgiroNumber);
				switch (index)
				{
					case 11: post.Type.ShouldBe(PaymentType.Credit); break;
					case 12: post.Result.ShouldBe(PaymentResult.AGConnectionMissing); break;
					case 13: post.Result.ShouldBe(PaymentResult.InsufficientFunds); break;
					case 14: post.Result.ShouldBe(PaymentResult.WillTryAgain); break;
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
}