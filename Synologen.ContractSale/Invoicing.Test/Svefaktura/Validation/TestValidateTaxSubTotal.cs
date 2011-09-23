using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Test.App;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using PercentType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation 
{
	[TestFixture]
	public class TestValidateTaxSubTotal : AssertionHelper 
	{
		[Test]
		public void Test_Complete_TaxSubTotal_Validates() 
		{
			var taxSubTotal = new SFTITaxSubTotalType 
			{
				TaxableAmount = new AmountType {Value = 123.45m},
				TaxAmount = new TaxAmountType{ Value = 123.45m},
				TaxCategory = new SFTITaxCategoryType {
					ID = new IdentifierType{Value = "S"},
					TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value="VAT"}},
                    Percent = new PercentType{ Value = 25}
				}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxSubTotal);
			ruleViolations.AssertIsEmpty();
		}

		[Test]
		public void Test_TaxSubTotal_Missing_TaxableAmount_Fails_Validation() 
		{
			var taxSubTotal = new SFTITaxSubTotalType { TaxableAmount = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(taxSubTotal);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTITaxSubTotalType.TaxableAmount");
		}	

		[Test]
		public void Test_TaxSubTotal_Missing_TaxAmount_Fails_Validation() 
		{
			var taxSubTotal = new SFTITaxSubTotalType { TaxAmount = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(taxSubTotal);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTITaxSubTotalType.TaxAmount");
		}

		[Test]
		public void Test_TaxSubTotal_Missing_TaxCategory_Fails_Validation() 
		{
			var taxSubTotal = new SFTITaxSubTotalType { TaxCategory = null };
			var ruleViolations = SvefakturaValidator.ValidateObject(taxSubTotal);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTITaxSubTotalType.TaxCategory");
		}
		
	}
}