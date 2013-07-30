using System.Collections.ObjectModel;
using DataDynamics.ActiveReports;
using Spinit.Wpc.Synologen.Presentation.Intranet.Reports.Models;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Reports
{
	public partial class OrderSubReport : ActiveReport
	{
		public OrderSubReport(OrderConfirmationModel viewModel)
		{
			DataSource = new Collection<OrderConfirmationModel> {viewModel};
			InitializeComponent();
		}
	}
}
