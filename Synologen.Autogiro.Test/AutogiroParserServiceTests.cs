using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Autogiro.Test.Factories;

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
			_parserService = new AutogiroParserService();
			var concentsFile = FileFactory.GetConsentsFile();
			_parsedFileContent = _parserService.ParseConsents(concentsFile);
		}

		[Test]
		public void Parsed_File_Contents_Contains_Expected_Data()
		{
			var expectedOutput = FileContentsFactory.GetLayoutA();
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
			_parserService = new AutogiroParserService();
			var paymentsFile = FileFactory.GetPaymentsFile();
			_parsedFileContent = _parserService.ParsePayments(paymentsFile);
		}

		[Test]
		public void Parsed_File_Contents_Contains_Expected_Data()
		{
			var expectedOutput = FileContentsFactory.GetLayoutB();
			_parsedFileContent.ShouldBe(expectedOutput);
		}
	}
}