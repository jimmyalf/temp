using System;
using System.Linq;
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
            SelectedCategory(this, new SelectedSomethingEventArgs(categoryId));
    	}

        private void Select_ArticleType(object sender, EventArgs e)
        {
            if (SelectedArticleType == null) return;
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            SelectedArticleType(this, new SelectedSomethingEventArgs(categoryId, articleTypeId));
        }

        private void Select_Supplier(object sender, EventArgs e)
        {
            if (SelectedSupplier == null) return;
            var supplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue);
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            SelectedSupplier(this, new SelectedSomethingEventArgs(categoryId, articleTypeId, supplierId));
        }

        private void Select_Article(object sender, EventArgs e)
        {
            if (SelectedArticle == null) return;
            var supplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue);
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            var articleId = Convert.ToInt32(ddlPickArticle.SelectedValue);
            var articleOption = Convert.ToInt32(ddlShippingOptions.SelectedValue);
            SelectedArticle(this, new SelectedSomethingEventArgs(categoryId, articleTypeId, supplierId, articleId, articleOption));
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
                var selectedShippingOption = Convert.ToInt32(ddlShippingOptions.SelectedValue);
				//TODO: Consider switching to deciamal type
                var selectedLeftBaseCurve = Convert.ToDecimal(ddlLeftBaskurva.SelectedValue);
                var selectedLeftAxis = Convert.ToDecimal(ddlLeftAxis.SelectedValue);
                var selectedLeftCylinder = Convert.ToDecimal(ddlLeftCylinder.SelectedValue);
                var selectedLeftDiameter = Convert.ToDecimal(ddlLeftDiameter.SelectedValue);
                var selectedLeftPower = Convert.ToDecimal(ddlLeftStrength.SelectedValue);
                var selectedLeftAddition = Convert.ToDecimal(ddlLeftAddition.SelectedValue);
                var selectedRightBaseCurve = Convert.ToDecimal(ddlRightBaskurva.SelectedValue);
                var selectedRightAxis = Convert.ToDecimal(ddlRightAxis.SelectedValue);
                var selectedRightCylinder = Convert.ToDecimal(ddlRightCylinder.SelectedValue);
                var selectedRightDiameter = Convert.ToDecimal(ddlRightDiameter.SelectedValue);
                var selectedRightPower = Convert.ToDecimal(ddlRightStrength.SelectedValue);
                var selectedRightAddition = Convert.ToDecimal(ddlRightAddition.SelectedValue);

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
                ShipmentOption = Convert.ToInt32(ddlShippingOptions.SelectedValue),
                LeftBaseCurve = Convert.ToDecimal(ddlLeftBaskurva.SelectedValue),
                LeftDiameter = Convert.ToDecimal(ddlLeftDiameter.SelectedValue),
                LeftPower = Convert.ToDecimal(ddlLeftStrength.SelectedValue),
                LeftAxis = Convert.ToDecimal(ddlLeftAxis.SelectedValue),
                LeftCylinder = Convert.ToDecimal(ddlLeftCylinder.SelectedValue),
                LeftAddition = Convert.ToDecimal(ddlLeftAddition.SelectedValue),
                RightBaseCurve = Convert.ToDecimal(ddlRightBaskurva.SelectedValue),
                RightDiameter = Convert.ToDecimal(ddlRightDiameter.SelectedValue),
                RightPower = Convert.ToDecimal(ddlRightStrength.SelectedValue),
                RightAxis = Convert.ToDecimal(ddlRightAxis.SelectedValue),
                RightCylinder = Convert.ToDecimal(ddlRightCylinder.SelectedValue),
                RightAddition = Convert.ToDecimal(ddlRightAddition.SelectedValue)
            };
			TryFireSubmit(sender, args);
        }

        private void PreviousStep(object sender, EventArgs e)
        {
            TryFirePrevious(this, new EventArgs());
        }
    }
}