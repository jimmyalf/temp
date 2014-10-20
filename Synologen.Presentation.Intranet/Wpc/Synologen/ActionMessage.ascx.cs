using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen
{
	[PresenterBinding(typeof(ActionMessagePresenter))]
	public partial class ActionMessage :  MvpUserControl<ActionMessageModel>, IActionMessageView
	{
		protected void Page_Load(object sender, EventArgs e) 
		{

		}
	}
}