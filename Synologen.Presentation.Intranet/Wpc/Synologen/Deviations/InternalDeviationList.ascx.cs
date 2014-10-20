using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations
{
	[PresenterBinding(typeof(InternalDeviationListPresenter))] 
	public partial class InternalDeviationList : MvpUserControl<InternalDeviationListModel>, IInternalDeviationListView
	{
        public int? ViewPageId { get; set; }
        protected void Page_Load(object sender, EventArgs e) { }
	}
}