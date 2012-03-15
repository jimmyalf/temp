using System;
using System.IO;
using System.Web.UI;
using Spinit.Wpc.Core.UI;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Reports;
using Spinit.Wpc.Synologen.Presentation.Intranet.Reports.Models;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
	public partial class OrderReportView : Page
	{
		private readonly IOrderRepository _orderRepository;

		public OrderReportView()
		{
			_orderRepository = ServiceLocator.Current.GetInstance<IOrderRepository>();
		}

    	protected void Page_Load(object sender, EventArgs e)
    	{
			if(!RequestOrderId.HasValue) return;
    		var order = _orderRepository.Get(RequestOrderId.Value);
    		var viewModel = new OrderConfirmationModel(order);
    		var stream = new OrderReport(viewModel).ToPdfStream();
    		var fileName = string.Format("Bekräftelse {0}.pdf", order.Id);
    		OutputFile(stream, fileName);
    	}

		private void OutputFile(MemoryStream stream, string fileName)
		{
			Response.ContentType = "application/pdf";
			Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
			Response.BinaryWrite(stream.ToArray());
			Response.End();
		}

		private int? RequestOrderId
		{
			get { return Page.Request.Params["order"].ToNullableInt(); }
		} 
	}
}