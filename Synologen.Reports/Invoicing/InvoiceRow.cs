using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Reports.Invoicing
{
    public class InvoiceRow
    {
        public string RowAmount { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string SinglePrice { get; set; }

        public static IList<InvoiceRow> GetList()
        {
            return new List<InvoiceRow>();
        }
    }
}