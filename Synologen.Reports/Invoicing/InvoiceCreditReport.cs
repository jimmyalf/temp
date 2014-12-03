namespace Spinit.Wpc.Synologen.Reports.Invoicing
{
    public class InvoiceCreditReport : InvoiceCopyReport
    {
        public string InvoiceHeading
        {
            get { return "Kreditfaktura " + InvoiceCreditNumber; }
        }

        public string InvoiceCreditNumber { get; set; }
    }
}