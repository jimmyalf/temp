using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing.Test.App;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using PercentType = Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.Validation 
{
	[TestFixture]
	public class When_validating_TaxCategory : AssertionHelper
	{
		[Test]
		public void Complete_TaxCategory_Validates() 
		{
			var taxCategory = new SFTITaxCategoryType {
				ID = new IdentifierType{Value="S"},
				TaxScheme = new SFTITaxSchemeType{ID = new IdentifierType{Value = "VAT"}},
                Percent = new PercentType{Value = 25}
			};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			ruleViolations.AssertIsEmpty();
		}
		[Test]
		public void TaxCategory_Missing_ID_Fails_Validation() 
		{
			var taxCategory = new SFTITaxCategoryType {ID = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTITaxCategoryType.ID");
		}

		[Test]
		public void TaxCategory_With_Wrong_ID_Fails_Validation() 
		{
			var taxCategory = new SFTITaxCategoryType {ID = new IdentifierType{Value = "Ö"}};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTITaxCategoryType.ID");
		}	

		[Test]
		public void TaxCategory_Missing_TaxScheme_Fails_Validation() 
		{
			var taxCategory = new SFTITaxCategoryType {TaxScheme = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTITaxCategoryType.TaxScheme");
		}

		[Test]
		public void TaxCategory_Missing_Percent_Fails_Validation() 
		{
			var taxCategory = new SFTITaxCategoryType {Percent = null};
			var ruleViolations = SvefakturaValidator.ValidateObject(taxCategory);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTITaxCategoryType.Percent");
		}	
	}
}