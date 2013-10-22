using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters
{
	public class ActionMessagePresenter : Presenter<IActionMessageView>
	{
		private ITempDataProvider _tempDataProvider;
		public ActionMessagePresenter(IActionMessageView view) : base(view)
		{
			View.Load += Load;
		}

		private void Load(object sender, EventArgs e)
		{
			_tempDataProvider = (ITempDataProvider) HttpContext.Items["__TempDataProvider"];
			if(_tempDataProvider == null) return;
			var message = _tempDataProvider.Get("ActionMessage");
			View.Model.Message = (message == null) ? null : message.ToString();
			View.Model.HasActionMessage = (message != null);
		}

		public override void ReleaseView()
		{
			View.Load -= Load;
		}
	}
}