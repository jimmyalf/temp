using Microsoft.Reporting.WebForms;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Reports.Invoicing
{
	public interface IInvoiceReportViewService 
	{
		ReportDataSource[] GetInvoiceReportDataSources(Order invoice);
        ReportDataSource[] GetCreditInvoiceReportDataSources(Order invoice, string creditInvoiceNumber);
	    ReportDataSource[] GetInvoiceCopyReportDataSources(Order invoice);
	}
}