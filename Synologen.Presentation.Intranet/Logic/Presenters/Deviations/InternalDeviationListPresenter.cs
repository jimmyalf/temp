using System;
using System.Collections.Generic;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public class InternalDeviationListPresenter : DeviationPresenter<IInternalDeviationListView>
	{
        private readonly IRoutingService _routingService;
        private readonly ISynologenMemberService _synologenMemberService;

		public InternalDeviationListPresenter(IInternalDeviationListView view, ISession session, IRoutingService routingService, ISynologenMemberService synologenMemberService) : base(view, session)
		{
		    _routingService = routingService;
		    _synologenMemberService = synologenMemberService;
		    View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
            if (View.ViewPageId.HasValue)
            {
                View.Model.ViewDeviationUrl = _routingService.GetPageUrl(View.ViewPageId.Value);
            }
            var shopId = _synologenMemberService.GetCurrentShopId();
            View.Model.Deviations = Query(new DeviationsQuery { SelectedShop = shopId, SelectedType = DeviationType.Internal, OrderBy = "CreatedDate" });
		}

		public override void ReleaseView()
		{
			View.Load += View_Load;
		}
	}
}