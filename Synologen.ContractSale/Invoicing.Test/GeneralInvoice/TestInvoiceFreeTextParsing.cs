using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Invoicing.Test.GeneralInvoice
{
	[TestFixture]
	public class TestInvoiceFreeTextParsing 
	{

		[Test]
		public void Test_Parsing_CustomerName() 
		{
			var order = GetOrder("Test: {CustomerName}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: Anders Andersson", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerName_Null() {
			var order = GetEmptyOrder("Test: {CustomerName}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerName_Missing_First_Name() 
		{
			var order = GetOrder("Test: {CustomerName}");
			order.CustomerFirstName = null;
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: Andersson", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerName_Missing_Last_Name()
		{
			var order = GetOrder("Test: {CustomerName}");
			order.CustomerLastName = null;
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: Anders", parsedString);
		}

		[Test]
		public void Test_Parsing_Customer_First_Name()
		{
			var order = GetOrder("Test: {CustomerFirstName}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: Anders", parsedString);
		}

		[Test]
		public void Test_Parsing_Customer_First_Name_Null() 
		{
			var order = GetEmptyOrder("Test: {CustomerFirstName}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_Customer_Last_Name()
		{
			var order = GetOrder("Test: {CustomerLastName}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: Andersson", parsedString);
		}

		[Test]
		public void Test_Parsing_Customer_Last_Name_Null() 
		{
			var order = GetEmptyOrder("Test: {CustomerLastName}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerPersonalIdNumber() 
		{
			var order = GetOrder("Test: {CustomerPersonalIdNumber}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: 197001015376", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerPersonalIdNumber_Null() 
		{
			var order = GetEmptyOrder("Test: {CustomerPersonalIdNumber}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_CompanyUnit() 
		{
			var order = GetOrder("Test: {CompanyUnit}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: Enhet 123456", parsedString);
		}
		[Test]
		public void Test_Parsing_CompanyUnit_Null() 
		{
			var order = GetEmptyOrder("Test: {CompanyUnit}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerPersonalBirthDateString() 
		{
			var order = GetOrder("Test: {CustomerPersonalBirthDateString}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: 19700101", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerPersonalBirthDateString_Null() 
		{
			var order = GetEmptyOrder("Test: {CustomerPersonalBirthDateString}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_CustomerPersonalBirthDateString_With_Incorrect_PersonalIdNumber() 
		{
			var order = GetOrder("Test: {CustomerPersonalBirthDateString}");
			order.PersonalIdNumber = "1234";
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: ", parsedString);
		}

		[Test]
		public void Test_Parsing_BuyerCompanyId()
		{
			var order = GetOrder("Test: {BuyerCompanyId}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: 23", parsedString);
		}

		[Test]
		public void Test_Parsing_RST() 
		{
			var order = GetOrder("Test: {RST}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: Kostnadsställe ABC", parsedString);
		}
		[Test]
		public void Test_Parsing_BankCode() 
		{
			var order = GetOrder("Test: {BankCode}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: 99998", parsedString);
		}

		[Test]
		public void Test_Parsing_SellingShopName() 
		{
			var order = GetOrder("Test: {SellingShopName}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: Butiken AB", parsedString);
		}

		[Test]
		public void Test_Parsing_SellingShopNumber() 
		{
			var order = GetOrder("Test: {SellingShopNumber}");
			var parsedString = order.ParseFreeText();
			Assert.AreEqual("Test: 0123456789", parsedString);
		}

		private static Order GetOrder(string freeTextFormat)
		{
			return new Order
			{
				CustomerFirstName = "Anders",
				CustomerLastName = "Andersson",
				PersonalIdNumber = "197001015376",
				CompanyId = 23,
				RstText = "Kostnadsställe ABC",
				CompanyUnit = "Enhet 123456",
				SellingShop = new Shop
				{
					Name = "Butiken AB",
					Number  = "0123456789"
				},
				ContractCompany = new Company
				{
					BankCode  = "99998",
					InvoiceFreeTextFormat = freeTextFormat
				}
			};
		}

		private static Order GetEmptyOrder(string freeTextFormat)
		{
			return new Order
			{
				SellingShop = new Shop(),
				ContractCompany = new Company
				{
					InvoiceFreeTextFormat = freeTextFormat
				}
			};
		}

	}
}