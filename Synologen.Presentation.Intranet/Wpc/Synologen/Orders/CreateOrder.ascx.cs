using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(CreateOrderPresenter))]
    public partial class CreateOrder : MvpUserControl<CreateOrderModel>, ICreateOrderView
    {
        public int NextPageId { get; set; }
        public event EventHandler<SelectedArticleTypeEventArgs> SelectedArticleType;
        public event EventHandler<SelectedSupplierEventArgs> SelectedSupplier;
    	public event EventHandler<CreateOrderEventArgs> Submit;
    	public event EventHandler<SelectedCategoryEventArgs> SelectedCategory;

    	protected void Page_Load(object sender, EventArgs e)
        {
            btnNextStep.Click += NextStep;
            btnPreviousStep.Click += PreviousStep;
            btnCancel.Click += Cancel;
        }

        private void Cancel(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PreviousStep(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NextStep(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
            if (Submit == null) return;
            var args = new CreateOrderEventArgs
            {
                ArticleId = Convert.ToInt32(ddlPickArticle.SelectedValue),
                CategoryId = Convert.ToInt32(ddlPickCategory.SelectedValue),
                ShipmentOption = Convert.ToInt32(SupplierOption.SelectedValue),
                SupplierId = Convert.ToInt32(SupplierOption.SelectedValue),
                TypeId = Convert.ToInt32(ddlPickKind.SelectedValue),
                LeftBaseCurve = Convert.ToInt32(ddlLeftBaskurva.SelectedValue),
                LeftDiameter = Convert.ToInt32(ddlLeftDiameter.SelectedValue),
                LeftPower = Convert.ToInt32(ddlRightStrength.SelectedValue),
                RightBaseCurve = Convert.ToInt32(ddlRightBaskurva.SelectedValue),
                RightDiameter = Convert.ToInt32(ddlRightDiameter.SelectedValue),
                RightPower = Convert.ToInt32(ddlRightStrength.SelectedValue)
            };
            Submit(this, args);
        }

    	protected void Category_SelectedIndexChanged(object sender, EventArgs e)
    	{
			//if (SelectedCategory == null) return;
			//var selectedValue = ddlPickCategory.SelectedValue;
			//var id = Convert.ToInt32(selectedValue);
			//SelectedCategory(this,new SelectedCategoryEventArgs(id));
    	}
    }
}