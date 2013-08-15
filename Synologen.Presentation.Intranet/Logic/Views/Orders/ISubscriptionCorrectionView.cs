using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders
{
	public interface ISubscriptionCorrectionView : IView<SubscriptionCorrectionModel>
	{
		int RedirectOnCreatePageId { get; set; }
		int ReturnPageId { get; set; }
		event EventHandler<SubmitCorrectionEventArgs> Submit;
	}
}