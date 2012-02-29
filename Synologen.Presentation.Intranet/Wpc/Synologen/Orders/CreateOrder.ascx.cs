using System;
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
    public partial class CreateOrder : OrderUserControl<CreateOrderModel,CreateOrderEventArgs>, ICreateOrderView
    {
    	public event EventHandler<SelectedSomethingEventArgs> SelectedCategory;
        public event EventHandler<SelectedSomethingEventArgs> SelectedArticleType;
        public event EventHandler<SelectedSomethingEventArgs> SelectedSupplier;
        public event EventHandler<SelectedSomethingEventArgs> SelectedArticle;

    	protected void Page_Load(object sender, EventArgs e)
        {
            btnNextStep.Click += NextStep;
    	    btnPreviousStep.Click += PreviousStep;
            btnPreviousStep.Click += TryFirePrevious;
            btnCancel.Click += TryFireAbort;
    		ddlPickCategory.SelectedIndexChanged += Select_Category;
            ddlPickSupplier.SelectedIndexChanged += Select_Supplier;
    	    ddlPickKind.SelectedIndexChanged += Select_ArticleType;
            ddlPickArticle.SelectedIndexChanged += Select_Article;
        }

        private void Select_Category(object sender, EventArgs e)
    	{
    		if(SelectedCategory == null) return;
    		var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            SelectedCategory(this, new SelectedSomethingEventArgs{SelectedCategoryId = categoryId});
    	}

        private void Select_ArticleType(object sender, EventArgs e)
        {
            if (SelectedArticleType == null) return;
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            SelectedArticleType(this, new SelectedSomethingEventArgs{SelectedCategoryId = categoryId, SelectedArticleTypeId = articleTypeId});
        }

        private void Select_Supplier(object sender, EventArgs e)
        {
            if (SelectedSupplier == null) return;
            var supplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue);
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
			SelectedSupplier(this, new SelectedSomethingEventArgs{SelectedCategoryId = categoryId, SelectedArticleTypeId = articleTypeId, SelectedSupplierId = supplierId});
        }

        private void Select_Article(object sender, EventArgs e)
        {
            if (SelectedArticle == null) return;
            var supplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue);
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            var articleId = Convert.ToInt32(ddlPickArticle.SelectedValue);
            var articleOption = Convert.ToInt32(ddlShippingOptions.SelectedValue);
			SelectedArticle(this, new SelectedSomethingEventArgs{SelectedCategoryId = categoryId, SelectedArticleTypeId = articleTypeId, SelectedSupplierId = supplierId, SelectedArticleId = articleId, SelectedShippingOption = articleOption});
        }

        private void NextStep(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
            //{
				//SelectedArticle(this, new SelectedSomethingEventArgs
				//{
				//    SelectedCategoryId = Convert.ToInt32(ddlPickCategory.SelectedValue),
				//    SelectedArticleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue),
				//    SelectedArticleId = Convert.ToInt32(ddlPickArticle.SelectedValue),
				//    SelectedShippingOption = Convert.ToInt32(ddlShippingOptions.SelectedValue),
				//    SelectedSupplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue),
				//    SelectedBaseCurve = GetEyeParameter(ddlLeftBaskurva,ddlRightBaskurva),
				//    SelectedDiameter = GetEyeParameter(ddlLeftDiameter, ddlRightDiameter),
				//    SelectedPower = GetEyeParameter(ddlLeftStrength, ddlRightStrength),
				//    SelectedAxis = GetEyeParameter(ddlLeftAxis, ddlRightAxis),
				//    SelectedCylinder = GetEyeParameter(ddlLeftCylinder, ddlRightCylinder),
				//    SelectedAddition = GetEyeParameter(ddlLeftAddition,ddlRightAddition)
				//});
                //return;
            //}

            var args = new CreateOrderEventArgs
            {
                ArticleId = Convert.ToInt32(ddlPickArticle.SelectedValue),
                ShipmentOption = Convert.ToInt32(ddlShippingOptions.SelectedValue),
				BaseCurve = GetEyeParameterNullable(ddlLeftBaskurva,ddlRightBaskurva),
				Diameter = GetEyeParameterNullable(ddlLeftDiameter, ddlRightDiameter),
				Power = GetEyeParameterNullable(ddlLeftStrength, ddlRightStrength),
				Axis = GetEyeParameterNullable(ddlLeftAxis, ddlRightAxis),
				Cylinder = GetEyeParameterNullable(ddlLeftCylinder, ddlRightCylinder),
				Addition = GetEyeParameterNullable(ddlLeftAddition,ddlRightAddition)
            };
			TryFireSubmit(sender, args);
        }

		private EyeParameter<decimal?> GetEyeParameterNullable(DropDownList leftDropDownListControl, DropDownList rightDropDownListControl)
		{
			return new EyeParameter<decimal?>
			{
				Left = Convert.ToDecimal(leftDropDownListControl.SelectedValue),
				Right = Convert.ToDecimal(rightDropDownListControl.SelectedValue)
			};
		}

		//private EyeParameter<decimal> GetEyeParameter(DropDownList leftDropDownListControl, DropDownList rightDropDownListControl)
		//{
		//    return new EyeParameter<decimal>
		//    {
		//        Left = Convert.ToDecimal(leftDropDownListControl.SelectedValue),
		//        Right = Convert.ToDecimal(rightDropDownListControl.SelectedValue)
		//    };
		//}

        private void PreviousStep(object sender, EventArgs e)
        {
            TryFirePrevious(this, new EventArgs());
        }
    }
}