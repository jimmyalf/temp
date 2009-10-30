using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Utility;

namespace Spinit.Wpc.Synologen.Test.GeneralInvoice {
	[TestFixture]
	public class TestInvoiceFreeTextSplitting {

		[Test]
		public void Test_Split_String() {
			const string testFormat = "{CustomerName}\r\n{CustomerName}";
			var company = new Company {InvoiceFreeTextFormat = testFormat};
			var order = new Order { CustomerFirstName = "Anders", CustomerLastName = "Andersson"};
			var parsedStrings = CommonConversion.GetFreeTextRows(company, order);
			Assert.AreEqual("Anders Andersson", parsedStrings[0]);
			Assert.AreEqual("Anders Andersson", parsedStrings[1]);
		}
		[Test]
		public void Test_Split_String_Trim_Leading_And_Trailing_Whitespaces() {
			const string testFormat = " {CustomerName}\r\n{CustomerName} ";
			var company = new Company {InvoiceFreeTextFormat = testFormat};
			var order = new Order { CustomerFirstName = "Anders", CustomerLastName = "Andersson"};
			var parsedStrings = CommonConversion.GetFreeTextRows(company, order);
			Assert.AreEqual("Anders Andersson", parsedStrings[0]);
			Assert.AreEqual("Anders Andersson", parsedStrings[1]);
		}
		[Test]
		public void Test_Trim_And_Split_String_Only_NewLine() {
			const string testFormat = "{CustomerName}\n{CustomerName}";
			var company = new Company {InvoiceFreeTextFormat = testFormat};
			var order = new Order { CustomerFirstName = "Anders", CustomerLastName = "Andersson"};
			var parsedStrings = CommonConversion.GetFreeTextRows(company, order);
			Assert.AreEqual("Anders Andersson", parsedStrings[0]);
			Assert.AreEqual("Anders Andersson", parsedStrings[1]);
		}
		[Test]
		public void Test_Trim_And_Split_String_Only_Return() {
			const string testFormat = " {CustomerName}\r{CustomerName} ";
			var company = new Company {InvoiceFreeTextFormat = testFormat};
			var order = new Order { CustomerFirstName = "Anders", CustomerLastName = "Andersson"};
			var parsedStrings = CommonConversion.GetFreeTextRows(company, order);
			Assert.AreEqual("Anders Andersson", parsedStrings[0]);
			Assert.AreEqual("Anders Andersson", parsedStrings[1]);
		}

	}
}