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
            drpArticlesLeft.SelectedIndexChanged += Select_Article;
			drpArticlesRight.SelectedIndexChanged += Select_Article;
        }

        private void Select_Category(object sender, EventArgs e)
    	{
    		if(SelectedCategory == null) return;
            SelectedCategory(this, new SelectedSomethingEventArgs
            {
            	SelectedCategoryId = Convert.ToInt32(ddlPickCategory.SelectedValue)
            });
    	}

        private void Select_ArticleType(object sender, EventArgs e)
        {
            if (SelectedArticleType == null) return;
            SelectedArticleType(this, new SelectedSomethingEventArgs
            {
            	SelectedCategoryId = Convert.ToInt32(ddlPickCategory.SelectedValue), 
				SelectedArticleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue)
            });
        }

        private void Select_Supplier(object sender, EventArgs e)
        {
            if (SelectedSupplier == null) return;
			SelectedSupplier(this, new SelectedSomethingEventArgs
			{
				SelectedCategoryId = Convert.ToInt32(ddlPickCategory.SelectedValue), 
				SelectedArticleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue), 
				SelectedSupplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue)
			});
        }

        private void Select_Article(object sender, EventArgs e)
        {
            if (SelectedArticle == null) return;
			SelectedArticle(this, new SelectedSomethingEventArgs
			{
				SelectedCategoryId = Convert.ToInt32(ddlPickCategory.SelectedValue), 
				SelectedArticleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue), 
				SelectedSupplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue), 
				SelectedArticleId = new EyeParameter<int>
				{
					Left = Convert.ToInt32(drpArticlesLeft.SelectedValue), 
					Right = Convert.ToInt32(drpArticlesRight.SelectedValue), 
				},
				SelectedShippingOption = Convert.ToInt32(ddlShippingOptions.SelectedValue)
			});
        }

        private void NextStep(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
            var args = new CreateOrderEventArgs
            {
                ArticleId = new EyeParameter<int>
				{
					Left = Convert.ToInt32(drpArticlesLeft.SelectedValue), 
					Right = Convert.ToInt32(drpArticlesRight.SelectedValue), 
				},
                ShipmentOption = Convert.ToInt32(ddlShippingOptions.SelectedValue),
				BaseCurve = GetEyeParameter(ddlLeftBaskurva, ddlRightBaskurva),
				Diameter = GetEyeParameter(ddlLeftDiameter, ddlRightDiameter),
				Power = GetEyeParameter(txtLeftStrength, txtRightStrength),
				Axis = GetEyeParameter(txtLeftAxis, txtRightAxis),
				Cylinder = GetEyeParameter(txtLeftCylinder, txtRightCylinder),
				Addition = GetEyeParameter(txtLeftAddition, txtRightAddition),
				Reference = txtReference.Text
            };
			TryFireSubmit(sender, args);
        }

		private EyeParameter<decimal?> GetEyeParameter(ListControl leftDropDownListControl, ListControl rightDropDownListControl)
		{
			return new EyeParameter<decimal?>
			{
				Left = Convert.ToDecimal(leftDropDownListControl.SelectedValue),
				Right = Convert.ToDecimal(rightDropDownListControl.SelectedValue)
			};
		}
		private EyeParameter<string> GetEyeParameter(ITextControl leftTextControl, ITextControl rightTextControl)
		{
			return new EyeParameter<string>
			{
				Left = leftTextControl.Text,
				Right = rightTextControl.Text
			};
		}

        private void PreviousStep(object sender, EventArgs e)
        {
            TryFirePrevious(this, new EventArgs());
        }
    }
}