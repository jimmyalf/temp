using System;
using Spinit.Wpc.Synologen.Presentation.Site.Models.ContractSales;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.ContractSales
{
	public interface IViewSettlementView : IView<ViewSettlementModel>
	{
		event EventHandler SwitchView;
		event EventHandler MarkAllSaleItemsAsPayed;
	}
}