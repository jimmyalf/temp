using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Test.App;

namespace Spinit.Wpc.Synologen.Test.Svefaktura.Validation 
{
	[TestFixture]
	public class TestValidateSimpleIdentifierType : AssertionHelper 
	{
		[Test]
		public void Test_Empty_ID_Fails_Validation() 
		{
		    var invoice = new SFTISimpleIdentifierType {Value = null};
		    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTISimpleIdentifierType.Value");
		}

		[Test]
		public void Test_Invoice_With_Empty_ID_Fails_Validation()
		{
			var invoice = new SFTIInvoiceType {ID = new SFTISimpleIdentifierType()};
		    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
			ruleViolations.AssertContains(x => x.PropertyName == "SFTISimpleIdentifierType.Value");
		}
	}
}