using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing;

namespace Spinit.Wpc.Synologen.Test.GeneralInvoice 
{
	[TestFixture]
	public class TestInvoiceFreeTextSplitting 
	{
		[Test]
		public void Test_Split_String() 
		{
			var order = GetOrder("{CustomerName}\r\n{CustomerName}");
			var parsedStrings = CommonConversion.GetFreeTextRows(order);
			Assert.AreEqual("Anders Andersson", parsedStrings[0]);
			Assert.AreEqual("Anders Andersson", parsedStrings[1]);
		}

		[Test]
		public void Test_Split_String_Trim_Leading_And_Trailing_Whitespaces() 
		{
			var order = GetOrder(" {CustomerName}\r\n{CustomerName} ");
			var parsedStrings = CommonConversion.GetFreeTextRows(order);
			Assert.AreEqual("Anders Andersson", parsedStrings[0]);
			Assert.AreEqual("Anders Andersson", parsedStrings[1]);
		}

		[Test]
		public void Test_Trim_And_Split_String_Only_NewLine()
		{
			var order = GetOrder("{CustomerName}\n{CustomerName}");
			var parsedStrings = CommonConversion.GetFreeTextRows(order);
			Assert.AreEqual("Anders Andersson", parsedStrings[0]);
			Assert.AreEqual("Anders Andersson", parsedStrings[1]);
		}

		[Test]
		public void Test_Trim_And_Split_String_Only_Return()
		{
			var order = GetOrder(" {CustomerName}\r{CustomerName} ");
			var parsedStrings = CommonConversion.GetFreeTextRows(order);
			Assert.AreEqual("Anders Andersson", parsedStrings[0]);
			Assert.AreEqual("Anders Andersson", parsedStrings[1]);
		}

		private static Order GetOrder(string freeTextFormat)
		{
			return new Order
			{
				CustomerFirstName = "Anders", 
				CustomerLastName = "Andersson",
				ContractCompany = new Company{ InvoiceFreeTextFormat = freeTextFormat}
			};
		}

	}
}