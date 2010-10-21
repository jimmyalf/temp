using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen
{
	[PresenterBinding(typeof(CreateLensSubscriptionPresenter))] 
	public partial class LensSubscriptionCreateSubscription : MvpUserControl<CreateLensSubscriptionModel>, ICreateLensSubscriptionView
	{
		public event EventHandler<SaveSubscriptionEventArgs> Submit;

		protected void Page_Load(object sender, EventArgs e)
		{
			btnSave.Click += Save;
		}

		private void Save(object sender, EventArgs e) 
		{
			if (Submit == null) return;
			Page.Validate();
			if(Page.IsValid == false) return;
			var args = new SaveSubscriptionEventArgs
			{
				AccountNumber = txtAccountNumber.Text,
				ClearingNumber = txtClearingNumber.Text,
				MonthlyAmount = txtMonthlyAmount.Text.ToDecimalOrDefault()
			};
			Submit(this, args);
		}

		//protected void ValidateLength(object source, ServerValidateEventArgs args)
		//{
		//    var control = source as CustomValidator;
		//    if(control == null)
		//    {
		//        args.IsValid = false;
		//        return;
		//    }
		//    if(control.ControlToValidate.Equals(txtClearingNumber.ID))
		//    {
		//        args.IsValid = args.Value.Length.Equals(4);
		//    }
		//    if(control.ControlToValidate.Equals(txtAccountNumber.ID))
		//    {
		//        args.IsValid = args.Value.Length <= 12;
		//    }
		//    return;
		//}
	}
}