using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription
{
	public interface ICreateLensSubscriptionView : IView<CreateLensSubscriptionModel>
	{
		event EventHandler<SaveSubscriptionEventArgs> Submit;
		int RedirectOnSavePageId { get; set; }
	}
}