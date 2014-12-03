using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.LensSubscription
{
	public interface IEditLensSubscriptionView : IView<EditLensSubscriptionModel> 
	{
		event EventHandler<SaveSubscriptionEventArgs> Submit;
		event EventHandler<EventArgs> StopSubscription;
		event EventHandler<EventArgs> StartSubscription;
		event EventHandler<SaveSubscriptionEventArgs> UpdateForm;
		int RedirectOnSavePageId { get; set; }
		int ReturnPageId { get; set; }
	}
}