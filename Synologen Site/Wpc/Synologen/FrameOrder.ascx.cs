using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Extensions;
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
				SelectedFrameId = drpFrames.SelectedValue.ToIntOrDefault(0),
				SelectedGlassTypeId = drpGlassTypes.SelectedValue.ToIntOrDefault(0),
				SelectedPupillaryDistance = new EyeParameter
				{
					Left = drpPupillaryDistanceLeft.SelectedValue.ToDecimalOrDefault(0),
					Right = drpPupillaryDistanceRight.SelectedValue.ToDecimalOrDefault(0)
				},
				SelectedSphere = new EyeParameter
				{
					Left = drpSphereLeft.SelectedValue.ToDecimalOrDefault(0),
					Right = drpSphereRight.SelectedValue.ToDecimalOrDefault(0)
				},
				SelectedCylinder = new EyeParameter
				{
					Left = drpCylinderLeft.SelectedValue.ToDecimalOrDefault(0),
					Right = drpCylinderRight.SelectedValue.ToDecimalOrDefault(0)
				},
				SelectedAxis = new EyeParameter
				{
					Left = txtAxisLeft.Text.ToDecimalOrDefault(0),
					Right = txtAxisRight.Text.ToDecimalOrDefault(0)
				},
				PageIsValid = Page.IsValid
			};
		}


	}
}