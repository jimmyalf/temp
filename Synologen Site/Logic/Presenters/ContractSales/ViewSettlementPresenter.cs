using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.ContractSales;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.ContractSales
{
	public class ViewSettlementPresenter : Presenter<IViewSettlementView>
	{
		public ViewSettlementPresenter(IViewSettlementView view) : base(view)
		{
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e) { throw new NotImplementedException(); }

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}