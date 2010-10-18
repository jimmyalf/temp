using System;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters
{
	public class CreateLensSubscriptionPresenter : Presenter<ICreateLensSubscriptionView>
	{

		public CreateLensSubscriptionPresenter(ICreateLensSubscriptionView view) : base(view)
		{
			View.Load += View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			View.Model.CustomerId = HttpContext.Request.Params["customer"].ToIntOrDefault();
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}

}