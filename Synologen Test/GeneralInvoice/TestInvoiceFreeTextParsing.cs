using NUnit.Framework;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Utility;

namespace Spinit.Wpc.Synologen.Test.GeneralInvoice {

	[TestFixture]
	public class TestInvoiceFreeTextParsing {
		private readonly CompanyRow emptyCompany = new CompanyRow();
		private readonly OrderRow emptyOrder = new OrderRow();

		[Test]
		public void Test_Parsing_CustomerName() {
			const string testFormat = "Test: {CustomerName}";
			var order = new OrderRow { CustomerFirstName = "Anders", CustomerLastName = "Andersson"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: Anders Andersson", parsedString);
		}
		[Test]
		public void Test_Parsing_CustomerName_Null() {
			const string testFormat = "Test: {CustomerName}";
			var order = new OrderRow ();
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: ", parsedString);
		}
		[Test]
		public void Test_Parsing_CustomerName_Missing_First_Name() {
			const string testFormat = "Test: {CustomerName}";
			var order = new OrderRow { CustomerLastName = "Andersson"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: Andersson", parsedString);
		}
		[Test]
		public void Test_Parsing_CustomerName_Missing_Last_Name() {
			const string testFormat = "Test: {CustomerName}";
			var order = new OrderRow { CustomerFirstName = "Anders"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: Anders", parsedString);
		}

		[Test]
		public void Test_Parsing_Customer_First_Name() {
			const string testFormat = "Test: {CustomerFirstName}";
			var order = new OrderRow { CustomerFirstName = "Anders"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: Anders", parsedString);
		}
		[Test]
		public void Test_Parsing_Customer_First_Name_Null() {
			const string testFormat = "Test: {CustomerFirstName}";
			var order = new OrderRow();
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: ", parsedString);
		}
		[Test]
		public void Test_Parsing_Customer_Last_Name() {
			const string testFormat = "Test: {CustomerLastName}";
			var order = new OrderRow { CustomerLastName = "Andersson"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: Andersson", parsedString);
		}
		[Test]
		public void Test_Parsing_Customer_Last_Name_Null() {
			const string testFormat = "Test: {CustomerLastName}";
			var order = new OrderRow();
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerPersonalIdNumber() {
			const string testFormat = "Test: {CustomerPersonalIdNumber}";
			var order = new OrderRow { PersonalIdNumber = "197001015374"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: 197001015374", parsedString);
		}
		[Test]
		public void Test_Parsing_CustomerPersonalIdNumber_Null() {
			const string testFormat = "Test: {CustomerPersonalIdNumber}";
			var order = new OrderRow();
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_CompanyUnit() {
			const string testFormat = "Test: {CompanyUnit}";
			var order = new OrderRow { CompanyUnit = "Enhet 123456"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: Enhet 123456", parsedString);
		}
		[Test]
		public void Test_Parsing_CompanyUnit_Null() {
			const string testFormat = "Test: {CompanyUnit}";
			var order = new OrderRow();
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: ", parsedString);
		}
		[Test]
		public void Test_Parsing_CustomerPersonalBirthDateString() {
			const string testFormat = "Test: {CustomerPersonalBirthDateString}";
			var order = new OrderRow { PersonalIdNumber = "197001015374"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: 19700101", parsedString);
		}
		[Test]
		public void Test_Parsing_CustomerPersonalBirthDateString_Null() {
			const string testFormat = "Test: {CustomerPersonalBirthDateString}";
			var order = new OrderRow();
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: ", parsedString);
		}
		[Test]
		public void Test_Parsing_CustomerPersonalBirthDateString_With_Incorrect_PersonalIdNumber() {
			const string testFormat = "Test: {CustomerPersonalBirthDateString}";
			var order = new OrderRow { PersonalIdNumber = "19700101537"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: ", parsedString);
		}
		[Test]
		public void Test_Parsing_BuyerCompanyId() {
			const string testFormat = "Test: {BuyerCompanyId}";
			var order = new OrderRow { CompanyId = 23};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: 23", parsedString);
		}
		[Test]
		public void Test_Parsing_RST() {
			const string testFormat = "Test: {RST}";
			var order = new OrderRow { RstText = "Kostnadsställe ABC"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, order, emptyCompany);
			Assert.AreEqual("Test: Kostnadsställe ABC", parsedString);
		}
		[Test]
		public void Test_Parsing_BankCode() {
			const string testFormat = "Test: {BankCode}";
			var company = new CompanyRow{ BankCode  = "99998"};
			var parsedString = CommonConversion.ParseInvoiceFreeTeext(testFormat, emptyOrder, company);
			Assert.AreEqual("Test: 99998", parsedString);
		}
	}
}