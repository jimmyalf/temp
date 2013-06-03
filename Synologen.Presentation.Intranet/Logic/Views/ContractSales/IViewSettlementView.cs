using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.ContractSales;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.ContractSales
{
	public interface IViewSettlementView : IView<ViewSettlementModel>
	{
		event EventHandler SwitchView;
		event EventHandler MarkAllSaleItemsAsPayed;
		int SubscriptionPageId { get; set; }
		int NewSubscriptionPageId { get; set; }
	}
}