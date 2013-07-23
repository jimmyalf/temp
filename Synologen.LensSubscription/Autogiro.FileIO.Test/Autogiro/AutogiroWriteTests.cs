using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;
using Synologen.LensSubscription.Autogiro.FileIO.Test.Autogiro.Factories;
using Synologen.LensSubscription.Autogiro.FileIO.Test.Helpers;
using Synologen.LensSubscription.Autogiro.Writers;

namespace Synologen.LensSubscription.Autogiro.FileIO.Test.Autogiro
{
	[TestFixture]
	[Category("AutogiroFileWriterTester")]
	public class When_writing_payments_file : IOBase
	{
		private readonly string _filePath;
		private readonly string _expectedFileContents;
		private AutogiroFileWriter<PaymentsFile> _writer;

		public When_writing_payments_file()
		{
			_expectedFileContents = FileContentFactory.GetLayoutB();
			_filePath = GetWriteFilePath(@"\Autogiro_Payments.txt");
			var paymentsToWrite = FileFactory.GetPaymentsFile();
			Context = () =>
			{
				var fileWriter = new PaymentsFileWriter();
				_writer = new AutogiroFileWriter<PaymentsFile>(fileWriter, _filePath);
			};
			Because = () => { _writer.Write(paymentsToWrite); };
			
		}

		[Test]
		public void Written_file_contains_expected_content()
		{
			var writtenFileContents = ReadFileContents(_filePath);
			writtenFileContents.ShouldBe(_expectedFileContents);
		}
	}

	[TestFixture]
	[Category("AutogiroFileWriterTester")]
	public class When_writing_consents_file : IOBase
	{
		private readonly string _expectedFileContents;
		private readonly string _filePath;
		private AutogiroFileWriter<ConsentsFile> _writer;

		public When_writing_consents_file()
		{
			_expectedFileContents = FileContentFactory.GetLayoutA();
			_filePath = GetWriteFilePath(@"\Autogiro_Consents.txt");
			var consentsToWrite = FileFactory.GetConsentsFile();
			Context = () =>
			{
				var fileWriter = new ConsentsFileWriter();
				_writer = new AutogiroFileWriter<ConsentsFile>(fileWriter, _filePath);
			};
			Because = () => { _writer.Write(consentsToWrite); };
		}

		[Test]
		public void Written_file_contains_expected_content()
		{
			var writtenFileContents = ReadFileContents(_filePath);
			writtenFileContents.ShouldBe(_expectedFileContents);
		}
	}
}