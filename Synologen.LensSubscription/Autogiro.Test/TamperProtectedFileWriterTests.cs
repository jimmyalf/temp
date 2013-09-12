using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.Autogiro.Test.Factories;
using Synologen.LensSubscription.Autogiro.Test.TestHelpers;
using Synologen.LensSubscription.Autogiro.Writers;

namespace Synologen.LensSubscription.Autogiro.Test
{
	[TestFixture, Category("TamperProtectedFileWriterTests")]
	public class When_writing_a_tamper_protected_file : TamperProtectedFileWriterTestBase
	{
		private string fileData;
		private string fileWriterOutput;
		private string condensateTypeName;
		private string expectedSealOpeningRecord;
		private string expectedTamperProtectionRecord;
		private string kvvHash;
		private string macHash;


		public When_writing_a_tamper_protected_file()
		{
			Context = () =>
			{
				fileData = TamperProtectedFileWriterFactory.GenerateFileData();
				condensateTypeName = "HMAC";
				kvvHash = TamperProtectedFileWriterFactory.GenerateFakeHash(2);
				macHash = TamperProtectedFileWriterFactory.GenerateFakeHash(55);
				expectedSealOpeningRecord = string.Format("00{0}{1}{2}", WriteDate.ToString("yyMMdd"), condensateTypeName, Enumerable.Repeat(' ', 68).AsString());
				expectedTamperProtectionRecord = string.Format("99{0}{1}{2}        ",WriteDate.ToString("yyMMdd"), kvvHash, macHash);

				A.CallTo(() => HashService.CondensateTypeName).Returns(condensateTypeName);
				A.CallTo(() => HashService.GetHash(TamperProtectedFileWriter.KeyVerificationToken)).Returns(kvvHash);
				A.CallTo(() => HashService.GetHash(expectedSealOpeningRecord + "\r\n" + fileData)).Returns(macHash);
			};

			Because = fileWriter =>
			{
				fileWriterOutput = fileWriter.Write(fileData);
			};
		}

		[Test]
		public void Output_contains_seal_opening_record_on_first_line()
		{
			var firstLine = fileWriterOutput.Split("\r\n".ToCharArray()).First();
			firstLine.ShouldBe(expectedSealOpeningRecord);
		}

		[Test]
		public void Output_contains_all_input_lines()
		{
			var outputLines = fileWriterOutput.Split("\r\n".ToCharArray());
			var inputLines = fileData.Split("\r\n".ToCharArray());
			inputLines.Each(line => outputLines.ShouldContain(line));
		}

		[Test]
		public void Output_contains_tamper_protection_record_on_last_line()
		{
			var lastLine = fileWriterOutput.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Last();
			lastLine.ShouldBe(expectedTamperProtectionRecord);
		}
	}
}