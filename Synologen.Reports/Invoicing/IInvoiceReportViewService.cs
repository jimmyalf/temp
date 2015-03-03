using Microsoft.Reporting.WebForms;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;

namespace Spinit.Wpc.Synologen.Reports.Invoicing
{
	public interface IInvoiceReportViewService 
	{
		ReportDataSource[] GetInvoiceReportDataSources(Order invoice, PDF_InvoiceType invoiceType);
        ReportDataSource[] GetCreditInvoiceReportDataSources(Order invoice, string creditInvoiceNumber);
	}
}