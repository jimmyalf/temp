using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;

namespace Spinit.Wpc.Synologen.Invoicing.Types
{
    public interface I_PDF_OrderInvoiceConversionSettings
    {
        string EmailAdminAddress { get; set; }
        string EmailSynologenInvoiceSender { get; set; }
        string EmailSpinitServiceAddress { get; set; }
        string EmailSpinitServiceSendUser { get; set; }
        string EmailSpinitServicePassword { get; set; }
    }

    public class PDF_OrderInvoiceConversionSettings : I_PDF_OrderInvoiceConversionSettings
    {
        public string EmailAdminAddress { get; set; }
        public string EmailSynologenInvoiceSender { get; set; }
        public string EmailSpinitServiceAddress { get; set; }
        public string EmailSpinitServiceSendUser { get; set; }
        public string EmailSpinitServicePassword { get; set; }
    }
}