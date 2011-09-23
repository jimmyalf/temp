using NUnit.Framework;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Business.Extensions;

namespace Spinit.Wpc.Synologen.Unit.Test.Extensions{
	[TestFixture]
	public class TestExtensions : AssertionHelper {
		[TestFixtureSetUp]
		public void Setup() { }		

		[Test(Description="Test that any object property can be successfully formatted")]
		public void Test_ToFormattedString_Handles_Any_Property(){
			var listOfOrders = new List<IOrder> {new Order {Id = 1, CompanyId = 2}, new Order {Id = 2, CompanyId = 2}, new Order {Id = 3, CompanyId = 2}};
			var testStringIds = listOfOrders.ToFormattedString(x => x.Id.ToString(), ", ");
			var testStringCompanyIds = listOfOrders.ToFormattedString(x => x.CompanyId.ToString(), ", ");
			const string expectedResultIds = "1, 2, 3";
			const string expectedResultCompanyIds = "2, 2, 2";
			Expect(testStringIds, Is.EqualTo(expectedResultIds));
			Expect(testStringCompanyIds, Is.EqualTo(expectedResultCompanyIds));
		}
		[Test(Description="Test that any object property containing null will be omitted")]
		public void Test_ToFormattedString_Handles_Null(){
			var listOfOrders = new List<IOrder> {new Order { CompanyUnit = "Unit 1"}, new Order {CompanyUnit = null}, new Order {CompanyUnit = "Unit 3"}};
			var testStringIds = listOfOrders.ToFormattedString(x => x.CompanyUnit, ", ");
			const string expectedResultIds = "Unit 1, Unit 3";
			Expect(testStringIds, Is.EqualTo(expectedResultIds));
		}
	}
}