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
			reportPageOne.Report = new OrderSubReport(viewModel).SetHeader("Beställning - Butikens kopia");
			reportPageTwo.Report = new OrderSubReport(viewModel).SetHeader("Beställning - Kundens kopia");
		}
	}
}
