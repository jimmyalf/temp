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
    		ddlPickCategory.SelectedIndexChanged += Select_Category;
            ddlPickSupplier.SelectedIndexChanged += Select_Supplier;
    	    ddlPickKind.SelectedIndexChanged += Select_ArticleType;
            drpArticlesLeft.SelectedIndexChanged += Select_Article;
			drpArticlesRight.SelectedIndexChanged += Select_Article;
    		chkOnlyLeftEye.CheckedChanged += OnlyOneEye_Changed;
			chkOnlyRightEye.CheckedChanged += OnlyOneEye_Changed;
        }

    	private void Select_Category(object sender, EventArgs e)
    	{
    		if(SelectedCategory == null) return;
			SelectedCategory(this, GetOrderChangedEventArgs());
    	}

        private void Select_ArticleType(object sender, EventArgs e)
        {
            if (SelectedArticleType == null) return;
			SelectedArticleType(this, GetOrderChangedEventArgs());
        }

        private void Select_Supplier(object sender, EventArgs e)
        {
            if (SelectedSupplier == null) return;
        	SelectedSupplier(this, GetOrderChangedEventArgs());
        }

        private void Select_Article(object sender, EventArgs e)
        {
            if (SelectedArticle == null) return;
			SelectedArticle(this, GetOrderChangedEventArgs());
        }

    	private void OnlyOneEye_Changed(object sender, EventArgs e)
    	{
			if (SelectedOnlyOneEye == null) return;
			SelectedOnlyOneEye(this, GetOrderChangedEventArgs());
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