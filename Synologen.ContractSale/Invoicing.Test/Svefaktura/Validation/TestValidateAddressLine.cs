using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.Validation 
{
	[TestFixture]
	public class TestValidateAddressLine : AssertionHelper 
    {
		[Test]
		public void Test_Complete_AddressLine_Validates()
		{
		    var addressLine = new SFTIAddressLineType
		    {
		        Line = new List<LineType> { new LineType { Value = "Addressline one" } }
		    };
			var ruleViolations = SvefakturaValidator.ValidateObject(addressLine).ToList();
			Expect(ruleViolations.Count(), Is.EqualTo(0), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}

		[Test]
		public void Test_AddressLine_Missing_Line_Fails_Validation()
		{
		    var addressLine = new SFTIAddressLineType { Line = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(addressLine).ToList();
			Expect(ruleViolations.Count(x => x.PropertyName.Equals("SFTIAddressLineType.Line")), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}

		[Test]
		public void Test_AddressLine_With_Empty_Line_Fails_Validation()
		{
		    var addressLine = new SFTIAddressLineType { Line = new List<LineType>() };
			var ruleViolations = SvefakturaValidator.ValidateObject(addressLine).ToList();
			Expect(ruleViolations.Count(x => x.PropertyName.Equals("SFTIAddressLineType.Line")), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}