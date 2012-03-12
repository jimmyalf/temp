using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(CreateOrderPresenter))]
    public partial class CreateOrder : OrderUserControl<CreateOrderModel,OrderChangedEventArgs>, ICreateOrderView
    {
    	public event EventHandler<OrderChangedEventArgs> SelectedCategory;
        public event EventHandler<OrderChangedEventArgs> SelectedArticleType;
        public event EventHandler<OrderChangedEventArgs> SelectedSupplier;
        public event EventHandler<OrderChangedEventArgs> SelectedArticle;
    	public event EventHandler<OrderChangedEventArgs> SelectedOnlyOneEye;

    	protected void Page_Load(object sender, EventArgs e)
        {
            btnNextStep.Click += NextStep;
    	    btnPreviousStep.Click += PreviousStep;
            btnPreviousStep.Click += TryFirePrevious;
            btnCancel.Click += TryFireAbort;
    		ddlPickCategory.SelectedIndexChanged += (s, args) => TriggerEvent(SelectedCategory);
            ddlPickSupplier.SelectedIndexChanged += (s, args) => TriggerEvent(SelectedSupplier);
    	    ddlPickKind.SelectedIndexChanged += (s, args) => TriggerEvent(SelectedArticleType);
            drpArticlesLeft.SelectedIndexChanged += (s, args) => TriggerArticleChanged();
			drpArticlesRight.SelectedIndexChanged += (s, args) => TriggerArticleChanged();
    		chkOnlyLeftEye.CheckedChanged += (s, args) => TriggerEvent(SelectedOnlyOneEye);
			chkOnlyRightEye.CheckedChanged += (s, args) => TriggerEvent(SelectedOnlyOneEye);
        }

		private void TriggerEvent(EventHandler<OrderChangedEventArgs> eventHandler)
		{
			if(eventHandler == null) return;
			eventHandler(this, GetOrderChangedEventArgs());
		}

		private void TriggerArticleChanged()
		{
			if(SelectedArticle == null) return;
			SelectedArticle(this, GetOrderArticleChangedEventArgs());
		}

        private void NextStep(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
			TryFireSubmit(sender, GetOrderChangedEventArgs());
        }

        private void PreviousStep(object sender, EventArgs e)
        {
            TryFirePrevious(this, new EventArgs());
        }

		private OrderChangedEventArgs GetOrderChangedEventArgs()
		{
			return new OrderChangedEventArgs
			{
				SelectedCategoryId = GetValueOrDefault(ddlPickCategory), 
				SelectedArticleTypeId = GetValueOrDefault(ddlPickKind), 
				SelectedSupplierId = GetValueOrDefault(ddlPickSupplier), 
				SelectedShippingOption = GetValueOrDefault(ddlShippingOptions),
				SelectedArticleId = GetEyeParameter(drpArticlesLeft, drpArticlesRight, Convert.ToInt32, 0),
				SelectedBaseCurve = GetEyeParameter(ddlLeftBaskurva, ddlRightBaskurva, Convert.ToDecimal, -9999),
				SelectedDiameter = GetEyeParameter(ddlLeftDiameter, ddlRightDiameter, Convert.ToDecimal, -9999),
				SelectedPower = GetEyeParameter(txtLeftStrength, txtRightStrength, ""),
				SelectedAxis = GetEyeParameter(txtLeftAxis, txtRightAxis, ""),
				SelectedCylinder = GetEyeParameter(txtLeftCylinder, txtRightCylinder, ""),
				SelectedAddition = GetEyeParameter(txtLeftAddition, txtRightAddition, ""),
				SelectedQuantity = GetEyeParameter(txtLeftQuantity, txtRightQuantity, ""),
				SelectedReference = txtReference.Text,
				OnlyUse = GetEyeParameter(chkOnlyLeftEye, chkOnlyRightEye)
			};
		}

		private OrderChangedEventArgs GetOrderArticleChangedEventArgs()
		{
			return new OrderChangedEventArgs
			{
				SelectedCategoryId = GetValueOrDefault(ddlPickCategory), 
				SelectedArticleTypeId = GetValueOrDefault(ddlPickKind), 
				SelectedSupplierId = GetValueOrDefault(ddlPickSupplier), 
				SelectedShippingOption = GetValueOrDefault(ddlShippingOptions),
				SelectedArticleId = GetEyeParameter(drpArticlesLeft, drpArticlesRight, Convert.ToInt32, 0),
				SelectedBaseCurve = new EyeParameter<decimal?>(null, null),
				SelectedDiameter = new EyeParameter<decimal?>(null, null),
				SelectedPower = new EyeParameter<string>(),
				SelectedAxis = new EyeParameter<string>(),
				SelectedCylinder = new EyeParameter<string>(),
				SelectedAddition = new EyeParameter<string>(),
				SelectedQuantity = new EyeParameter<string>(),
				SelectedReference = txtReference.Text,
				OnlyUse = GetEyeParameter(chkOnlyLeftEye, chkOnlyRightEye)
			};
		}

		private int? GetValueOrDefault(ListControl control, int defaultValue = default(int))
		{
		    var value = Convert.ToInt32(control.SelectedValue);
		    if(value == defaultValue) return null;
		    return value;
		}

		private EyeParameter<T?> GetEyeParameter<T>(ListControl leftControl, ListControl rightControl, Func<string,T> converter, T defaultValue)
			where T:struct
		{
			return GetEyeParameter(leftControl.SelectedValue, rightControl.SelectedValue, converter, defaultValue);
		}

		private EyeParameter<string> GetEyeParameter(ITextControl leftControl, ITextControl rightControl, string defaultValue)
		{
			var left = DisableLeft ? defaultValue: leftControl.Text;
			var right = DisableRight ? defaultValue: rightControl.Text;
			return new EyeParameter<string>
			{
				Left = (left == defaultValue) ? null : left,
				Right = (right == defaultValue) ? null : right,
			};
		}

		private EyeParameter<T?> GetEyeParameter<T>(string leftValue, string rightValue, Func<string,T> converter, T defaultValue)
			where T:struct
		{
			var left = DisableLeft ? defaultValue: converter(leftValue);
			var right = DisableRight ? defaultValue: converter(rightValue);
			return new EyeParameter<T?>
			{
				Left = left.Equals(defaultValue) ? (T?) null : left,
				Right = right.Equals(defaultValue) ? (T?) null : right,
			};
		}

		private EyeParameter<bool> GetEyeParameter(ICheckBoxControl leftControl, ICheckBoxControl rightControl)
		{
			return new EyeParameter<bool>
			{
				Left = leftControl.Checked,
				Right = rightControl.Checked
			};
		}

		private bool DisableLeft { get { return chkOnlyRightEye.Checked; } }
		private bool DisableRight { get { return chkOnlyLeftEye.Checked; } }
    }
}