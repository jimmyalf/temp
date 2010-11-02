using NUnit.Framework;
using Shouldly;
using Synologen.Autogiro.Test.Factories;

namespace Synologen.Autogiro.Test
{
	[TestFixture]
	[Category("AutogiroParserServiceTester")]
	public class When_parsing_consents_to_send
	{
		private readonly IAutogiroParserService _parserService;
		private readonly string _parsedFile;

		public When_parsing_consents_to_send()
		{
			_parserService = new AutogiroParserService();
			var concentsFile = FileFactory.GetConsentsFile();
			_parsedFile = _parserService.ParseConsents(concentsFile);
		}

		[Test]
		public void Parsed_File_Contents_Contains_Expected_Data()
		{
			var expectedOutput = FileContentsFactory.GetLayoutA();
			_parsedFile.ShouldBe(expectedOutput);
		}
	}

	[TestFixture]
	[Category("AutogiroParserServiceTester")]
	public class When_parsing_payments_to_send
	{
		private readonly IAutogiroParserService _parserService;
		private readonly string _parsedFile;

		public When_parsing_payments_to_send()
		{
			_parserService = new AutogiroParserService();
			var paymentsFile = FileFactory.GetPaymentsFile();
			_parsedFile = _parserService.ParsePayments(paymentsFile);
		}

		[Test]
		public void Parsed_File_Contents_Contains_Expected_Data()
		{
			var expectedOutput = FileContentsFactory.GetLayoutB();
			_parsedFile.ShouldBe(expectedOutput);
		}
	}
}
 