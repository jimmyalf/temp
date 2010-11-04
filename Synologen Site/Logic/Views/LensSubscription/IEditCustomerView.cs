using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription
{
	public interface IEditCustomerView : IView<EditCustomerModel>
	{
		event EventHandler<SaveCustomerEventArgs> Submit;
		int RedirectOnSavePageId { get; set; }
		int EditSubscriptionPageId { get; set; }
		int CreateSubscriptionPageId { get; set; }
	}
}
