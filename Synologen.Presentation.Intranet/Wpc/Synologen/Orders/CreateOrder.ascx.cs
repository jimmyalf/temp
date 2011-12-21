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
        //public event EventHandler<SelectedSomethingEventArgs> SelectedSomething;

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

        /*
        private void Select_Something(object sender, EventArgs e)
        {
            if (SelectedSomething == null) return;
            var supplierId = Convert.ToInt32(ddlPickSupplier.SelectedValue);
            var articleTypeId = Convert.ToInt32(ddlPickKind.SelectedValue);
            var categoryId = Convert.ToInt32(ddlPickCategory.SelectedValue);
            var articleId = Convert.ToInt32(ddlPickArticle.SelectedValue);
            SelectedSomething(this, new SelectedSomethingEventArgs(categoryId, articleTypeId, supplierId, articleId));
        }
        */

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
            SelectedSupplier(this, new SelectedSomethingEventArgs(categoryId, articleTypeId, supplierId, articleId));
        }

        private void NextStep(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
            var args = new CreateOrderEventArgs
            {
                ArticleId = Convert.ToInt32(ddlPickArticle.SelectedValue),
                //CategoryId = Convert.ToInt32(ddlPickCategory.SelectedValue),
                ShipmentOption = Convert.ToInt32(SupplierOption.SelectedValue),
                //SupplierId = Convert.ToInt32(SupplierOption.SelectedValue),
                //TypeId = Convert.ToInt32(ddlPickKind.SelectedValue),
                LeftBaseCurve = Convert.ToInt32(ddlLeftBaskurva.SelectedValue),
                LeftDiameter = Convert.ToInt32(ddlLeftDiameter.SelectedValue),
                LeftPower = Convert.ToInt32(ddlRightStrength.SelectedValue),
                RightBaseCurve = Convert.ToInt32(ddlRightBaskurva.SelectedValue),
                RightDiameter = Convert.ToInt32(ddlRightDiameter.SelectedValue),
                RightPower = Convert.ToInt32(ddlRightStrength.SelectedValue)
            };
			TryFireSubmit(sender, args);
        }
    }
}