using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen {
	[PresenterBinding(typeof(FrameOrderPresenter))] 
	public partial class FrameOrder : MvpUserControl<FrameOrderModel>, IFrameOrderView<FrameOrderModel> {

		public event EventHandler<FrameFormEventArgs> FrameSelected;
		public event EventHandler<FrameFormEventArgs> SubmitForm;
		public event EventHandler<FrameFormEventArgs> GlassTypeSelected;

		protected void Page_Load(object sender, EventArgs e) {
			WireupEventProxy();
		}

		private void WireupEventProxy()
		{
			drpFrames.SelectedIndexChanged += (sender, e) => HandleEvent(FrameSelected);
			drpGlassTypes.SelectedIndexChanged += (sender, e) => HandleEvent(GlassTypeSelected);
			btnSave.Click += (sender, e) => HandleEvent(SubmitForm);
		}

		protected void HandleEvent(EventHandler<FrameFormEventArgs> eventhandler)
		{
			if(eventhandler == null) return;
			var eventArgs = GetEventArgs();
			eventhandler(this, eventArgs);
		}

		private FrameFormEventArgs GetEventArgs()
		{
			return new FrameFormEventArgs
			{
				SelectedFrameId = Int32.Parse(drpFrames.SelectedValue),
				SelectedGlassTypeId = Int32.Parse(drpGlassTypes.SelectedValue),
				SelectedPupillaryDistance = new EyeParameter
				{
					Left = (decimal) double.Parse(drpPupillaryDistanceLeft.SelectedValue),
					Right = (decimal) double.Parse(drpPupillaryDistanceRight.SelectedValue)
				},
				SelectedSphere = new EyeParameter
				{
					Left = (decimal) double.Parse(drpSphereLeft.SelectedValue),
					Right = (decimal) double.Parse(drpSphereRight.SelectedValue)
				},
				SelectedCylinder = new EyeParameter
				{
					Left = (decimal) double.Parse(drpCylinderLeft.SelectedValue),
					Right = (decimal) double.Parse(drpCylinderRight.SelectedValue)
				},
				PageIsValid = Page.IsValid
			};
		}
	}
}