using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Synologen.LensSubscription.Autogiro.FileIO.Test.Helpers;
using Synologen.LensSubscription.Autogiro.Readers;

namespace Synologen.LensSubscription.Autogiro.FileIO.Test.Autogiro
{
	[TestFixture]
	[Category("AutogiroFileReaderTester")]
	public class When_reading_payments_file : IOBase
	{
		private PaymentsFile _readPaymentsFile;
		private readonly int _expectedNumberOfPosts;
		private readonly DateTime _expectedWriteDate;
		private readonly PaymentReciever _expectedReciever;
		private AutogiroFileReader<PaymentsFile> _reader;

		public When_reading_payments_file()
		{
			_expectedNumberOfPosts = 15;
			_expectedWriteDate = new DateTime(2004, 10, 27);
			_expectedReciever = new PaymentReciever { BankgiroNumber = "9912346", CustomerNumber = "471117" };
			var filePath = GetReadFilePath(@"Autogiro\AGP Layout D.txt");
			Context = () =>
			{
				var fileReader = new PaymentsFileReader();
				_reader = new AutogiroFileReader<PaymentsFile>(fileReader, filePath);
			};
			Because = () => _readPaymentsFile = _reader.Read();
		}

		[Test]
		public void File_object_has_expected_properties_set()
		{
			_readPaymentsFile.WriteDate.ShouldBe(_expectedWriteDate);
			_readPaymentsFile.Reciever.ShouldBe(_expectedReciever);
			_readPaymentsFile.NumberOfCreditsInFile.ShouldBe(1);
			_readPaymentsFile.NumberOfDebitsInFile.ShouldBe(14);
			_readPaymentsFile.TotalCreditAmountInFile.ShouldBe(16874);
			_readPaymentsFile.TotalDebitAmountInFile.ShouldBe(5475);
			_readPaymentsFile.Posts.Count().ShouldBe(_expectedNumberOfPosts);
		}
	}

	[TestFixture]
	[Category("AutogiroFileReaderTester")]
	public class When_reading_consents_file : IOBase
	{
		private ConsentsFile _readConsentsFile;
		private readonly DateTime _expectedWriteDate;
		private readonly string _expectedRecieverBankgiroNumber;
		private AutogiroFileReader<ConsentsFile> _reader;

		public When_reading_consents_file()
		{
			_expectedWriteDate = new DateTime(2004, 01, 18);
			_expectedRecieverBankgiroNumber = "9912346";
			var filePath = GetReadFilePath(@"Autogiro\AGP Layout E.txt");
			Context = () =>
			{
				var fileReader = new ConsentsFileReader();
				_reader = new AutogiroFileReader<ConsentsFile>(fileReader, filePath);
			};
			Because = () => _readConsentsFile = _reader.Read();
			
		}

		[Test]
		public void File_object_has_expected_properties_set()
		{
			_readConsentsFile.PaymentRecieverBankgiroNumber.ShouldBe(_expectedRecieverBankgiroNumber);
			_readConsentsFile.WriteDate.ShouldBe(_expectedWriteDate);
			_readConsentsFile.NumberOfItemsInFile.ShouldBe(7);
		}
	}

	[TestFixture]
	[Category("AutogiroFileReaderTester")]
	public class When_reading_errors_file : IOBase
	{
		private ErrorsFile _readErrorsFile;
		private readonly DateTime _expectedWriteDate;
		private readonly PaymentReciever _expectedReciever;
		private AutogiroFileReader<ErrorsFile> _reader;

		public When_reading_errors_file()
		{
			_expectedWriteDate = new DateTime(2004, 10, 22);
			_expectedReciever = new PaymentReciever { BankgiroNumber = "9912346", CustomerNumber = "471117" };
			var filePath = GetReadFilePath(@"Autogiro\AGP Layout F.txt");
			Context = () =>
			{
				var fileReader = new ErrorFileReader();
				_reader = new AutogiroFileReader<ErrorsFile>(fileReader, filePath);
			};
			Because = () => _readErrorsFile = _reader.Read();
		}

		[Test]
		public void File_object_has_expected_properties_set()
		{
			_readErrorsFile.WriteDate.ShouldBe(_expectedWriteDate);
			_readErrorsFile.NumberOfCreditsInFile.ShouldBe(1);
			_readErrorsFile.NumberOfDebitsInFile.ShouldBe(3);
			_readErrorsFile.Reciever.BankgiroNumber.ShouldBe(_expectedReciever.BankgiroNumber);
			_readErrorsFile.Reciever.CustomerNumber.ShouldBe(_expectedReciever.CustomerNumber);
			_readErrorsFile.TotalCreditAmountInFile.ShouldBe(100M);
			_readErrorsFile.TotalDebitAmountInFile.ShouldBe(850M);
			_readErrorsFile.Posts.Count().ShouldBe(4);

		}
	}
}