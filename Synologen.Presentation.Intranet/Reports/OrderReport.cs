using DataDynamics.ActiveReports;
using Spinit.Wpc.Synologen.Presentation.Intranet.Reports.Models;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports
{
	public partial class OrderReport : ActiveReport
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
