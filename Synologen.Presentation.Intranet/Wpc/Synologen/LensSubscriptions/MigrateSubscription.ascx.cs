using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.LensSubscriptions
{
	[PresenterBinding(typeof(MigrateSubscriptionPresenter))]
	public partial class MigrateSubscription : MvpUserControl<MigrateSubscriptionModel>, IMigrateSubscriptionView
	{
		public event EventHandler<MigrateSubscriptionEventArgs> Migrate;
		public int NewSubscriptionPageId { get; set; }
		public int ReturnPageId { get; set; }

		protected void Page_Load(object sender, EventArgs e) { }

		protected void Submit_Migration(object sender, EventArgs e)
		{
			if(Migrate == null) return;
			var additionalWithdrawals = txtAdditionalWithdrawals.Text.ToInt();
			var startBalance = txtStartBalance.Text.ToDecimal();
			Migrate(this, new MigrateSubscriptionEventArgs(additionalWithdrawals, startBalance));
		}
	}
}