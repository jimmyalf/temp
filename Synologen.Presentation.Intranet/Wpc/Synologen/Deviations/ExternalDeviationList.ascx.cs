using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations
{
	[PresenterBinding(typeof(ExternalDeviationListPresenter))] 
	public partial class ExternalDeviationList : MvpUserControl<ExternalDeviationListModel>, IExternalDeviationListView
	{
        public event EventHandler<ExternalDeviationListEventArgs> SupplierSelected;
	    public int? ViewPageId { get; set; }

	    protected void Page_Load(object sender, EventArgs e)
		{
            drpSuppliers.SelectedIndexChanged += drpSuppliers_SelectedIndexChanged;
        }

        void drpSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var eventArgs = new ExternalDeviationListEventArgs
            {
                SelectedSupplier = drpSuppliers.SelectedValue.ToIntOrDefault(0)
            };

            SupplierSelected(this, eventArgs);
        }

    }
}