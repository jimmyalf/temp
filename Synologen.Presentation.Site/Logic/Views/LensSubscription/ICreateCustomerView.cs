using System;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription
{
	public interface ICreateCustomerView : IView<CreateCustomerModel>
	{
		event EventHandler<SaveCustomerEventArgs> Submit;
		int RedirectOnSavePageId { get; set; }
	}
}
