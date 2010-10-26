using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription
{
	public interface IEditLensSubscriptionView : IView<EditLensSubscriptionModel> 
	{
		event EventHandler<SaveSubscriptionEventArgs> Submit;
		event EventHandler<EventArgs> StopSubscription;
		event EventHandler<EventArgs> StartSubscription;
		int RedirectOnSavePageId { get; set; }
	}
}