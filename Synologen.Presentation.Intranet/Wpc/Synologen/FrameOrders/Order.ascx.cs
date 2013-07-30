using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.FrameOrders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.FrameOrders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.FrameOrders
{
	[PresenterBinding(typeof(EditFrameOrderPresenter))] 
	public partial class Order : MvpUserControl<EditFrameOrderModel>, IEditFrameOrderView<EditFrameOrderModel> 
	{
        public event EventHandler<SupplierSelectedEventArgs> SupplierSelected;
		public event EventHandler<FrameOrGlassTypeSelectedEventArgs> FrameSelected;
		public event EventHandler<EditFrameFormEventArgs> SubmitForm;
		public event EventHandler<FrameOrGlassTypeSelectedEventArgs> GlassTypeSelected;
		public int RedirectPageId { get; set; }

		protected void Page_Load(object sender, EventArgs e) {
			WireupEventProxy();
		}

		private void WireupEventProxy()
		{
		    drpSupplier.SelectedIndexChanged += Supplier_Selected;
		    drpFrames.SelectedIndexChanged += Frame_Selected;
            drpGlassTypes.SelectedIndexChanged += GlassType_Selected;
			btnSave.Click += (sender, e) => HandleEvent(SubmitForm, true);
		}

	    private void Supplier_Selected(object sender, EventArgs e)
	    {
	        if (SupplierSelected != null)
	        {
	            SupplierSelected(this, new SupplierSelectedEventArgs
	            {
                    SelectedSupplierId = drpSupplier.SelectedValue.ToIntOrDefault(0)
	            });
	        }
	    }

        private void Frame_Selected(object sender, EventArgs e)
        {
            if (FrameSelected != null)
            {
                FrameSelected(this, CreateArgs());
            }
        }

        private FrameOrGlassTypeSelectedEventArgs CreateArgs()
        {
            return new FrameOrGlassTypeSelectedEventArgs
            {
                SelectedFrameId = drpFrames.SelectedValue.ToIntOrDefault(0),
                SelectedGlassTypeId = drpGlassTypes.SelectedValue.ToIntOrDefault(0),
                SelectedSupplierId = drpSupplier.SelectedValue.ToIntOrDefault(0),
                SelectedPupillaryDistance = new EyeParameter
                {
                    Left = drpPupillaryDistanceLeft.SelectedValue.ToDecimalOrDefault(int.MinValue),
                    Right = drpPupillaryDistanceRight.SelectedValue.ToDecimalOrDefault(int.MinValue)
                },
                SelectedCylinder = new EyeParameter
                {
                    Left = drpCylinderLeft.SelectedValue.ToDecimalOrDefault(int.MinValue),
                    Right = drpCylinderRight.SelectedValue.ToDecimalOrDefault(int.MinValue)
                },
                SelectedSphere = new EyeParameter
                {
                    Left = drpSphereLeft.SelectedValue.ToDecimalOrDefault(int.MinValue),
                    Right = drpSphereRight.SelectedValue.ToDecimalOrDefault(int.MinValue)
                },
                SelectedAddition = new EyeParameter
                {
                    Left = drpAdditionLeft.SelectedValue.ToDecimalOrDefault(0),
                    Right = drpAdditionRight.SelectedValue.ToDecimalOrDefault(0)
                },
                SelectedHeight = new EyeParameter
                {
                    Left = drpHeightLeft.SelectedValue.ToDecimalOrDefault(0),
                    Right = drpHeightRight.SelectedValue.ToDecimalOrDefault(0)
                },
            };
        }

        private void GlassType_Selected(object sender, EventArgs e)
        {
            if (GlassTypeSelected != null)
            {
                GlassTypeSelected(this, CreateArgs());
            }
        }

	    protected void HandleEvent(EventHandler<EditFrameFormEventArgs> eventhandler, bool validate)
		{
			if(eventhandler == null) return;
			var eventArgs = GetEventArgs();
			if(validate) {
				Page.Validate();
				eventArgs.PageIsValid = Page.IsValid;
			}
			eventhandler(this, eventArgs);
		}
		protected void HandleEvent(EventHandler<EditFrameFormEventArgs> eventhandler)
		{
			HandleEvent(eventhandler, false);
		}

		private EditFrameFormEventArgs GetEventArgs()
		{
			return new EditFrameFormEventArgs
			{
				SelectedSupplierId = drpSupplier.SelectedValue.ToIntOrDefault(0),
                SelectedFrameId = drpFrames.SelectedValue.ToIntOrDefault(0),
				SelectedGlassTypeId = drpGlassTypes.SelectedValue.ToIntOrDefault(0),
				SelectedPupillaryDistance = new EyeParameter
				{
					Left = drpPupillaryDistanceLeft.SelectedValue.ToDecimalOrDefault(int.MinValue),
					Right = drpPupillaryDistanceRight.SelectedValue.ToDecimalOrDefault(int.MinValue)
				},
				SelectedSphere = new EyeParameter
				{
					Left = drpSphereLeft.SelectedValue.ToDecimalOrDefault(int.MinValue),
					Right = drpSphereRight.SelectedValue.ToDecimalOrDefault(int.MinValue)
				},
				SelectedCylinder = new EyeParameter
				{
					Left = drpCylinderLeft.SelectedValue.ToDecimalOrDefault(int.MinValue),
					Right = drpCylinderRight.SelectedValue.ToDecimalOrDefault(int.MinValue)
				},
				SelectedAxis = new EyeParameter<int>
				{
					Left = txtAxisLeft.Text.ToIntOrDefault(int.MinValue),
					Right = txtAxisRight.Text.ToIntOrDefault(int.MinValue)
				},
                SelectedAddition = new EyeParameter
                {
                	Left = drpAdditionLeft.SelectedValue.ToDecimalOrDefault(0),
					Right = drpAdditionRight.SelectedValue.ToDecimalOrDefault(0)
                },
                SelectedHeight = new EyeParameter
                {
                	Left = drpHeightLeft.SelectedValue.ToDecimalOrDefault(0),
					Right = drpHeightRight.SelectedValue.ToDecimalOrDefault(0)
                },
                Reference = txtReference.Text
			};
		}

	}
}