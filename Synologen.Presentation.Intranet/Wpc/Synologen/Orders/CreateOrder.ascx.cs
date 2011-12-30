using System;
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
            SelectedCategory(this, new SelectedSomethingEventArgs(categoryId, 0, 0, 0));
    	}

        private void Select_ArticleType(object sender, EventArgs e)
        {
            if (SelectedArticleType == null) return;
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);

            SelectedArticleType(this, new SelectedSomethingEventArgs(categoryId, articleTypeId, 0, 0));
        }

        private void Select_Supplier(object sender, EventArgs e)
        {
            if (SelectedSupplier == null) return;
            var supplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue);
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            SelectedSupplier(this, new SelectedSomethingEventArgs(categoryId, articleTypeId, supplierId, 0));
        }

        private void Select_Article(object sender, EventArgs e)
        {
            if (SelectedArticle == null) return;
            var supplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue);
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            var articleId = Convert.ToInt32(ddlPickArticle.SelectedValue);
            SelectedArticle(this, new SelectedSomethingEventArgs(categoryId, articleTypeId, supplierId, articleId));
        }

        private void NextStep(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                var supplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue);
                var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
                var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
                var articleId = Convert.ToInt32(ddlPickArticle.SelectedValue);
                var selectedShippingOption = Convert.ToInt32(rbShippingOptions.SelectedValue);

                var selectedLeftBaseCurve = (float)Convert.ToDecimal(ddlLeftBaskurva.SelectedValue);
                var selectedLeftAxis = (float)Convert.ToDecimal(ddlLeftAxis.SelectedValue);
                var selectedLeftCylinder = (float)Convert.ToDecimal(ddlLeftCylinder.SelectedValue);
                var selectedLeftDiameter = (float)Convert.ToDecimal(ddlLeftDiameter.SelectedValue);
                var selectedLeftPower = (float)Convert.ToDecimal(ddlLeftStrength.SelectedValue);
                var selectedLeftAddition = (float)Convert.ToDecimal(ddlLeftAddition.SelectedValue);
                var selectedRightBaseCurve = (float)Convert.ToDecimal(ddlRightBaskurva.SelectedValue);
                var selectedRightAxis = (float)Convert.ToDecimal(ddlRightAxis.SelectedValue);
                var selectedRightCylinder = (float)Convert.ToDecimal(ddlRightCylinder.SelectedValue);
                var selectedRightDiameter = (float)Convert.ToDecimal(ddlRightDiameter.SelectedValue);
                var selectedRightPower = (float)Convert.ToDecimal(ddlRightStrength.SelectedValue);
                var selectedRightAddition = (float)Convert.ToDecimal(ddlRightAddition.SelectedValue);

                SelectedArticle(this, new SelectedSomethingEventArgs(
                    categoryId, 
                    articleTypeId, 
                    supplierId, 
                    articleId, 
                    selectedShippingOption,
                    selectedLeftPower, 
                    selectedLeftBaseCurve, 
                    selectedLeftDiameter, 
                    selectedLeftCylinder,
                    selectedLeftAxis,
                    selectedLeftAddition,
                    selectedRightPower, 
                    selectedRightBaseCurve, 
                    selectedRightDiameter, 
                    selectedRightCylinder, 
                    selectedRightAxis,
                    selectedRightAddition));
                return;
            }

            var args = new CreateOrderEventArgs
            {
                ArticleId = Convert.ToInt32(ddlPickArticle.SelectedValue),
                ShipmentOption = Convert.ToInt32(rbShippingOptions.SelectedValue),

                LeftBaseCurve = (float)Convert.ToDecimal(ddlLeftBaskurva.SelectedValue),
                LeftDiameter = (float)Convert.ToDecimal(ddlLeftDiameter.SelectedValue),
                LeftPower = (float)Convert.ToDecimal(ddlLeftStrength.SelectedValue),
                LeftAxis = (float)Convert.ToDecimal(ddlLeftAxis.SelectedValue),
                LeftCylinder = (float)Convert.ToDecimal(ddlLeftCylinder.SelectedValue),
                LeftAddition = (float)Convert.ToDecimal(ddlLeftAddition.SelectedValue),
                RightBaseCurve = (float)Convert.ToDecimal(ddlRightBaskurva.SelectedValue),
                RightDiameter = (float)Convert.ToDecimal(ddlRightDiameter.SelectedValue),
                RightPower = (float)Convert.ToDecimal(ddlRightStrength.SelectedValue),
                RightAxis = (float)Convert.ToDecimal(ddlRightAxis.SelectedValue),
                RightCylinder = (float)Convert.ToDecimal(ddlRightCylinder.SelectedValue),
                RightAddition = (float)Convert.ToDecimal(ddlRightAddition.SelectedValue)
            };
			TryFireSubmit(sender, args);
        }
    }
}