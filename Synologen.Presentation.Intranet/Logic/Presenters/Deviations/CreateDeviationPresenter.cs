using System;
using NHibernate;
using Spinit.Wpc.Synologen.Data.Commands.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations
{
	public class CreateDeviationPresenter : DeviationPresenter<ICreateDeviationView>
	{
		public CreateDeviationPresenter(ICreateDeviationView view, ISession session) : base(view, session)
		{
			View.Load += View_Load;
			View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs e)
		{
			// TODO: Implement
		}

		public void View_Submit(object sender, CreateDeviationEventArgs e)
		{
			Execute(new CreateDeviationCommand());
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}
	}
}