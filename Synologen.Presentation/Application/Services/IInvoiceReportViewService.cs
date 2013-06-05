using Microsoft.Reporting.WebForms;
using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IInvoiceReportViewService 
	{
		ReportDataSource[] GetInvoiceReportDataSources(Order invoice);
	}
}