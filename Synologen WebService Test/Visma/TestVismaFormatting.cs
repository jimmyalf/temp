using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Visma.Utility;

namespace Spinit.Wpc.Synologen.Test.Visma {
	[TestFixture]
	public class TestVismaFormatting {

		#region RST and CompanyUnit Formatting
		[Test]
		public void TestGetRSTDescription() {
			var order = new OrderRow {RstText = "01234", CompanyUnit = "Testenheten"};
			var description = Formatting.BuildOrderRstDescription(order, 30);
			Assert.AreEqual("RST: 01234, Enhet: Testenheten",description);
		}
		[Test]
		public void TestGetRSTDescription_RST_Missing() {
			var order = new OrderRow {CompanyUnit = "Testenheten"};
			var description = Formatting.BuildOrderRstDescription(order, 30);
			Assert.AreEqual("Enhet: Testenheten",description);
		}
		[Test]
		public void TestGetRSTDescription_CompanyUnit_Missing() {
			var order = new OrderRow {RstText = "01234"};
			var description = Formatting.BuildOrderRstDescription(order, 30);
			Assert.AreEqual("RST: 01234",description);
		}
		[Test]
		public void TestGetRSTDescription_EmptyOrder() {
			var order = new OrderRow();
			var description = Formatting.BuildOrderRstDescription(order, 30);
			Assert.AreEqual(null, description);
		}
		[Test]
		public void TestGetRSTDescription_Gets_Truncated() {
			var order = new OrderRow {RstText = "01234", CompanyUnit = "Testenheten"};
			var description = Formatting.BuildOrderRstDescription(order, 20);
			Assert.AreEqual(20, description.Length);
		}
		#endregion

		#region Customer Info
		[Test]
		public void TestGetCustomerDescription() {
			var order = new OrderRow {PersonalIdNumber = "19700101-5374", CustomerFirstName = "Adam", CustomerLastName = "Bertil"};
			var description = Formatting.BuildOrderCustomerDescription(order, 40);
			Assert.AreEqual("Kund: 19700101-5374, Adam Bertil",description);
		}
		[Test]
		public void TestGetCustomerDescription_PersonalIdNumber_Missing() {
			var order = new OrderRow {CustomerFirstName = "Adam", CustomerLastName = "Bertil"};
			var description = Formatting.BuildOrderCustomerDescription(order, 40);
			Assert.AreEqual("Kund: Adam Bertil",description);
		}
		[Test]
		public void TestGetCustomerDescription_CustomerFirstName_Missing() {
			var order = new OrderRow {PersonalIdNumber = "19700101-5374", CustomerLastName = "Bertil"};
			var description = Formatting.BuildOrderCustomerDescription(order, 40);
			Assert.AreEqual("Kund: 19700101-5374, Bertil",description);
		}
		[Test]
		public void TestGetCustomerDescription_CustomerLastName_Missing() {
			var order = new OrderRow {PersonalIdNumber = "19700101-5374", CustomerFirstName = "Adam"};
			var description = Formatting.BuildOrderCustomerDescription(order, 40);
			Assert.AreEqual("Kund: 19700101-5374, Adam",description);
		}
		[Test]
		public void TestGetCustomerDescription_Missing_All_But_PersonalIdNumber() {
			var order = new OrderRow {PersonalIdNumber = "19700101-5374"};
			var description = Formatting.BuildOrderCustomerDescription(order, 40);
			Assert.AreEqual("Kund: 19700101-5374",description);
		}
		[Test]
		public void TestGetCustomerDescription_Missing_All_But_CustomerFirstName() {
			var order = new OrderRow {CustomerFirstName = "Adam"};
			var description = Formatting.BuildOrderCustomerDescription(order, 40);
			Assert.AreEqual("Kund: Adam",description);
		}
		[Test]
		public void TestGetCustomerDescription_Missing_All_But_CustomerLastName() {
			var order = new OrderRow { CustomerLastName = "Bertil"};
			var description = Formatting.BuildOrderCustomerDescription(order, 40);
			Assert.AreEqual("Kund: Bertil",description);
		}
		[Test]
		public void TestGetCustomerDescription_Empty() {
			var order = new OrderRow();
			var description = Formatting.BuildOrderCustomerDescription(order, 40);
			Assert.AreEqual(null, description);
		}
		[Test]
		public void TestGetCustomerDescription_Gets_Truncated() {
			var order = new OrderRow {PersonalIdNumber = "19700101-5374", CustomerFirstName = "Adam", CustomerLastName = "Bertil"};
			var description = Formatting.BuildOrderCustomerDescription(order, 20);
			Assert.AreEqual(20, description.Length);
		}
		#endregion

		[Test]
		public void TestTruncateString_Truncates_If_String_Is_Longer_Than_Limit() {
			var truncatedString = Formatting.TruncateString("abcdefgh", 3);
			Assert.AreEqual("abc",truncatedString);
		}
		[Test]
		public void TestTruncateString_Does_Not_Truncate_If_String_Is_Shorter_Than_Limit() {
			var notTruncatedString = Formatting.TruncateString("abcdefgh", 20);
			Assert.AreEqual("abcdefgh",notTruncatedString);
		}
	}
}