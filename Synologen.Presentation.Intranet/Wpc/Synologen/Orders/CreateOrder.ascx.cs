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
    		ddlPickCategory.SelectedIndexChanged += Category_Selected;
            ddlPickSupplier.SelectedIndexChanged += Supplier_Selected;
    	    ddlPickKind.SelectedIndexChanged += ArticleType_Selected;
            drpArticlesLeft.SelectedIndexChanged += Article_Selected_Left;
			drpArticlesRight.SelectedIndexChanged += Article_Selected_Right;
    		chkOnlyLeftEye.CheckedChanged += OnlyOneEye_Selected;
			chkOnlyRightEye.CheckedChanged += OnlyOneEye_Selected;
        }

    	private void Category_Selected(object sender, EventArgs e)
    	{
    		if(SelectedCategory == null) return;
    		var args = GetOrderChangedEventArgs(resetParametersLeft: true, resetParametersRight: true, resetArticle: true, resetArticleType: true, resetShippingOption: true, resetSupplier: true);
    		SelectedCategory(this, args);
    	}

    	private void ArticleType_Selected(object sender, EventArgs e)
    	{
    		if(SelectedArticleType == null) return;
			var args = GetOrderChangedEventArgs(resetParametersLeft: true, resetParametersRight: true, resetArticle: true, resetShippingOption: true, resetSupplier: true);
    		SelectedArticleType(this, args);
    	}

    	private void Supplier_Selected(object sender, EventArgs e)
    	{
    		if(SelectedSupplier == null) return;
			var args = GetOrderChangedEventArgs(resetParametersLeft: true, resetParametersRight: true, resetArticle: true, resetShippingOption: true);
    		SelectedSupplier(this, args);
    	}

    	private void Article_Selected_Left(object sender, EventArgs e)
    	{
    		if(SelectedArticle == null) return;
			var args = GetOrderChangedEventArgs(resetParametersLeft: true);
    		SelectedArticle(this, args);
    	}

    	private void Article_Selected_Right(object sender, EventArgs e)
    	{
    		if(SelectedArticle == null) return;
			var args = GetOrderChangedEventArgs(resetParametersRight: true);
    		SelectedArticle(this, args);
    	}

    	private void OnlyOneEye_Selected(object sender, EventArgs e)
    	{
    		if(SelectedOnlyOneEye == null) return;
			var args = GetOrderChangedEventArgs();
    		SelectedOnlyOneEye(this, args);
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

		private OrderChangedEventArgs GetOrderChangedEventArgs(
			bool resetParametersLeft = false, 
			bool resetParametersRight = false, 
			bool resetArticle = false, 
			bool resetShippingOption = false,
			bool resetSupplier = false,
			bool resetArticleType = false)
		{
			return new OrderChangedEventArgs
			{
				SelectedCategoryId = GetValueOrDefault(ddlPickCategory), 
				SelectedArticleTypeId =  GetValueOrDefault(ddlPickKind, reset: resetArticleType), 
				SelectedSupplierId = GetValueOrDefault(ddlPickSupplier, reset: resetSupplier), 
				SelectedShippingOption =  GetValueOrDefault(ddlShippingOptions, reset: resetShippingOption),
				SelectedArticleId =  GetEyeParameter(drpArticlesLeft, drpArticlesRight, Convert.ToInt32, 0, resetArticle, resetArticle),
				SelectedBaseCurve = GetEyeParameter(ddlLeftBaskurva, ddlRightBaskurva, Convert.ToDecimal, -9999, resetParametersLeft, resetParametersRight),
				SelectedDiameter = GetEyeParameter(ddlLeftDiameter, ddlRightDiameter, Convert.ToDecimal, -9999, resetParametersLeft, resetParametersRight),
				SelectedPower = GetEyeParameter(txtLeftStrength, txtRightStrength, "", resetParametersLeft, resetParametersRight),
				SelectedAxis = GetEyeParameter(txtLeftAxis, txtRightAxis, "", resetParametersLeft, resetParametersRight),
				SelectedCylinder = GetEyeParameter(txtLeftCylinder, txtRightCylinder, "", resetParametersLeft, resetParametersRight),
				SelectedAddition = GetEyeParameter(txtLeftAddition, txtRightAddition, "", resetParametersLeft, resetParametersRight),
				SelectedQuantity = GetEyeParameter(txtLeftQuantity, txtRightQuantity, "", resetParametersLeft, resetParametersRight),
				SelectedReference = txtReference.Text,
				OnlyUse = GetEyeParameter(chkOnlyLeftEye, chkOnlyRightEye)
			};
		}

		private int? GetValueOrDefault(ListControl control, int defaultValue = default(int), bool reset = false)
		{
			if (reset) return null;
		    var value = Convert.ToInt32(control.SelectedValue);
		    if(value == defaultValue) return null;
		    return value;
		}

		private EyeParameter<T?> GetEyeParameter<T>(ListControl leftControl, ListControl rightControl, Func<string,T> converter, T defaultValue, bool resetLeft = false, bool resetRight = false)
			where T:struct
		{
			return GetEyeParameter(leftControl.SelectedValue, rightControl.SelectedValue, converter, defaultValue, resetLeft, resetRight);
		}

		private EyeParameter<string> GetEyeParameter(ITextControl leftControl, ITextControl rightControl, string defaultValue, bool resetLeft = false, bool resetRight = false)
		{
			var left = DisableLeft || resetLeft ? defaultValue: leftControl.Text;
			var right = DisableRight || resetRight ? defaultValue: rightControl.Text;
			return new EyeParameter<string>
			{
				Left = (left == defaultValue) ? null : left,
				Right = (right == defaultValue) ? null : right,
			};
		}

		private EyeParameter<T?> GetEyeParameter<T>(string leftValue, string rightValue, Func<string,T> converter, T defaultValue, bool resetLeft = false, bool resetRight = false)
			where T:struct
		{
			var left = DisableLeft || resetLeft ? defaultValue: converter(leftValue);
			var right = DisableRight || resetRight ? defaultValue: converter(rightValue);
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