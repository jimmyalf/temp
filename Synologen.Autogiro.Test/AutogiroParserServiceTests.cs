using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wp.Synologen.Autogiro;
using Spinit.Wpc.Synologen.Autogiro.Test.Factories;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Services;

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

	
	//[TestFixture]
	//[Category("AutogiroParserServiceTester")]
	//public class When_parsing_payments_from_filecontent
	//{
	//    private readonly IAutogiroParserService _parserService;
	//    private readonly PaymentsFile _paymentsFile;
	//    private readonly int _expectedNumberOfPosts;

	//    public When_parsing_payments_from_filecontent()
	//    {
	//        var autogiroLineParserService = new AutogiroLineParserService();
	//        _expectedNumberOfPosts = 15;
	//        _parserService = new AutogiroParserService(autogiroLineParserService);
	//        var fileContent = FileContentFactory.GetLayoutD();
	//        _paymentsFile = _parserService.ReadPayments(fileContent);
	//    }

	//    [Test]
	//    public void Parsed_File_Contains_Expected_Data()
	//    {
	//        var expectedReciever = new PaymentReciever { BankgiroNumber = "9912346", CustomerNumber = "471117" };
	//        _paymentsFile.WriteDate.ShouldBe(new DateTime(2004,10,27), DateTimeTolerance.SameYearMonthDate);
	//        _paymentsFile.Reciever.ShouldBe(expectedReciever);
	//        _paymentsFile.Posts.Count().ShouldBe(_expectedNumberOfPosts);
	//        var payments = _paymentsFile.Posts.ToArray();
	//        //TODO: Continue testing post rows
	//        Assert.Inconclusive("No test created for asserting posts");
	//    }
	//}
}