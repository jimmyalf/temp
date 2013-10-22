namespace Spinit.Wpc.Synologen.Reports.Models
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