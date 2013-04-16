using System;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Data.Queries.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public class InternalDeviationListPresenter : DeviationPresenter<IInternalDeviationListView>
	{
		public InternalDeviationListPresenter(IInternalDeviationListView view, ISession session) : base(view, session)
		{
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			var deviations = Query(new DeviationsQuery { SelectedDeviationType = DeviationType.Internal });
			View.Model = new InternalDeviationListModel(deviations);
		}

		public override void ReleaseView()
		{
			View.Load += View_Load;
		}
	}
}