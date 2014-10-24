using System;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public class ExternalDeviationListPresenter : DeviationPresenter<IExternalDeviationListView>
	{
	    private readonly IRoutingService _routingService;
        private readonly ISynologenMemberService _synologenMemberService;
	    private readonly DeviationSupplierListItem _defaultSupplier = new DeviationSupplierListItem { Id = 0, Name = "-- Alla leverantörer --" };

		public ExternalDeviationListPresenter(IExternalDeviationListView view, IRoutingService routingService, ISession session, ISynologenMemberService synologenMemberService) : base(view, session)
		{
		    View.Load += View_Load;
		    View.SupplierSelected += View_SupplierSelected;

            _routingService = routingService;
		    _synologenMemberService = synologenMemberService;
		}

		public void View_Load(object sender, EventArgs e)
		{
            if (View.ViewPageId.HasValue)
            {
                View.Model.ViewDeviationUrl = _routingService.GetPageUrl(View.ViewPageId.Value);
            }
            var shopId = _synologenMemberService.GetCurrentShopId();
            View.Model.Suppliers = Query(new SuppliersQuery()).ToDeviationSupplierList().InsertFirst(_defaultSupplier);
            View.Model.Deviations = Query(new DeviationsQuery { SelectedShop = shopId, SelectedType = DeviationType.External, OrderBy = "CreatedDate" });
		}

        public void View_SupplierSelected(object sender, ExternalDeviationListEventArgs e)
        {
            View.Model.SelectedSupplierId = e.SelectedSupplier;
            View.Model.Suppliers = Query(new SuppliersQuery()).ToDeviationSupplierList().InsertFirst(_defaultSupplier);
            var shopId = _synologenMemberService.GetCurrentShopId();
            View.Model.Deviations = Query(new DeviationsQuery { SelectedShop = shopId, SelectedType = DeviationType.External, SelectedSupplier = e.SelectedSupplier, OrderBy = "CreatedDate", });
	    }

	    public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}