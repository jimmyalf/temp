using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen {
	[PresenterBinding(typeof(FrameOrderPresenter))] 
	public partial class FrameOrder : MvpUserControl<FrameOrderModel>, IFrameOrderView<FrameOrderModel> {

		public event EventHandler<FrameSelectedEventArgs> FrameSelected;
		public event EventHandler<FrameOrderFormSubmitEventArgs> SubmitForm;

		protected void Page_Load(object sender, EventArgs e) {
			WireupEventProxy();
		}

		private void WireupEventProxy()
		{
			drpFrames.SelectedIndexChanged += OnFrameChange;
			btnSave.Click += OnSubmit;
		}

		protected void OnFrameChange(object sender, EventArgs e) 
		{ 
			if(FrameSelected != null)
			{
				FrameSelected(this, 
					new FrameSelectedEventArgs {
						SelectedFrameId = Int32.Parse(drpFrames.SelectedValue),
						SelectedPupillaryDistanceLeft = (decimal) double.Parse(drpPupillaryDistanceLeft.SelectedValue),
						SelectedPupillaryDistanceRight = (decimal) double.Parse(drpPupillaryDistanceRight.SelectedValue),
					}
				);
			}
		}

		protected void OnSubmit(object sender, EventArgs e) 
		{ 
			if(SubmitForm != null)
			{
				SubmitForm(this,
					new FrameOrderFormSubmitEventArgs {
						SelectedFrameId = Int32.Parse(drpFrames.SelectedValue),
						SelectedPupillaryDistanceLeft = (decimal) double.Parse(drpPupillaryDistanceLeft.SelectedValue),
						SelectedPupillaryDistanceRight = (decimal) double.Parse(drpPupillaryDistanceRight.SelectedValue),
						PageIsValid = Page.IsValid
					}
				);
			}
		}
	}
}