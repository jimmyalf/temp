using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription
{
	public interface IMigrateSubscriptionView : IView<MigrateSubscriptionModel>
	{
		event EventHandler<MigrateSubscriptionEventArgs> Migrate;
		int NewSubscriptionPageId { get; set; }
		int ReturnPageId { get; set; }
	}

	public class MigrateSubscriptionEventArgs : EventArgs
	{
		public int AdditionalWithdrawals { get; set; }
		public MigrateSubscriptionEventArgs(int additionalWithdrawals)
		{
			AdditionalWithdrawals = additionalWithdrawals;
		}
	}
}