using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Test.App;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation 
{
	[TestFixture]
	public class TestValidateTaxScheme : AssertionHelper 
	{
		[Test]
		public void Test_Complete_TaxScheme_Validates()
		{
			var partyTaxScheme = new SFTITaxSchemeType
			{
				ID = new IdentifierType{Value="VAT"},
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			ruleViolations.AssertIsEmpty();
		}

		[Test]
		public void Test_TaxScheme_Missing_ID_Fails_Validation() 
		{
			var partyTaxScheme = new SFTITaxSchemeType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(partyTaxScheme);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTITaxSchemeType.ID");
		}	
	}
}