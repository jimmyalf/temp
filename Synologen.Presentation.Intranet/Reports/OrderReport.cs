
using Spinit.Wpc.Synologen.Presentation.Intranet.Reports.Models;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Document.Section;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports
{
    public partial class OrderReport : GrapeCity.ActiveReports.SectionReport
	{
        public OrderReport(OrderConfirmationModel viewModel)
        {
            InitializeComponent();
            SetupSubReports(viewModel);
        }

        private void SetupSubReports(OrderConfirmationModel viewModel)
        {
            reportPageOne.Report = new OrderSubReport(viewModel);
            reportPageTwo.Report = new OrderSubReport(viewModel);
            reportPageThree.Report = new OrderAGDescriptionReport();
        }
	}
}
