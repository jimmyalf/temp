using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura
{
    public interface ISvefakturaHydrator
    {
        void Fill(IOrder order, SFTIInvoiceType invoice);
    }
}