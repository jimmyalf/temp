using System.Collections.ObjectModel;
using Spinit.Wpc.Synologen.Presentation.Intranet.Reports.Models;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Controls;
using GrapeCity.ActiveReports.SectionReportModel;
using GrapeCity.ActiveReports.Document.Section;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports
{
    public partial class OrderSubReport : GrapeCity.ActiveReports.SectionReport
	{
        public OrderSubReport(OrderConfirmationModel viewModel)
        {
            DataSource = new Collection<OrderConfirmationModel> { viewModel };
            InitializeComponent();
        }
	}
}
