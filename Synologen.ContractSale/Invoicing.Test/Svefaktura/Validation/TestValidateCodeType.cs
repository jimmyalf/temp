using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.Validation 
{
	[TestFixture]
	public class TestValidateCodeType : AssertionHelper
    {
		[Test]
		public void Test_Empty_InvoiceTypeCode_Fails_Validation() 
        {
		    var invoiceTypeCode = new CodeType();
		    var ruleViolations = SvefakturaValidator.ValidateObject(invoiceTypeCode).ToList();
		    Expect(ruleViolations.Count(x => x.PropertyName.Equals("CodeType.Value")), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}

		[Test]
		public void Test_Invoice_With_Empty_InvoiceTypeCode_Fails_Validation()
		{
		    var invoice = new SFTIInvoiceType { InvoiceTypeCode = new CodeType() };
		    var ruleViolations = SvefakturaValidator.ValidateObject(invoice).ToList();
		    Expect(ruleViolations.Count(x => x.PropertyName.Equals("CodeType.Value")), Is.EqualTo(1), SvefakturaValidator.FormatRuleViolations(ruleViolations));
		}
	}
}