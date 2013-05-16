using System;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public class ExternalDeviationListPresenter : DeviationPresenter<IExternalDeviationListView>
	{
	    private readonly IRoutingService _routingService;
	    private readonly DeviationSupplierListItem _defaultSupplier = new DeviationSupplierListItem { Id = 0, Name = "-- Alla leverantörer --" };

		public ExternalDeviationListPresenter(IExternalDeviationListView view, IRoutingService routingService, ISession session) : base(view, session)
		{
		    View.Load += View_Load;
		    View.SupplierSelected += View_SupplierSelected;

            _routingService = routingService;
		}

		public void View_Load(object sender, EventArgs e)
		{
            View.Model.ViewDeviationUrl = _routingService.GetPageUrl(417);
            View.Model.Suppliers = Query(new SuppliersQuery()).ToDeviationSupplierList().InsertFirst(_defaultSupplier);
		    View.Model.Deviations = Query(new DeviationsQuery {SelectedType = DeviationType.External});
		}

        private void View_SupplierSelected(object sender, ExternalDeviationListEventArgs e)
        {
            View.Model.SelectedSupplierId = e.SelectedSupplier;
            View.Model.Suppliers = Query(new SuppliersQuery()).ToDeviationSupplierList().InsertFirst(_defaultSupplier);
            View.Model.Deviations = Query(new DeviationsQuery { SelectedType = DeviationType.External, SelectedSupplier = e.SelectedSupplier});
	    }

	    public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}