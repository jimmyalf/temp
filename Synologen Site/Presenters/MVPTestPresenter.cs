using System;
using Spinit.Wpc.Synologen.Core.Domain.Persistence;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Presenters
{
	public class MVPTestPresenter : Presenter<IView<MVPTestModel>>
	{
		private readonly IFrameRepository _repository;
		public MVPTestPresenter(IView<MVPTestModel> view, IFrameRepository repository) : base(view)
		{
			_repository = repository;
			View.Load += View_Load;
		}

		//public MVPTestPresenter(IView<MVPTestModel> view) : base(view)
		//{
		//    View.Load += View_Load;
		//}

        public override void ReleaseView()
        {
            View.Load -= View_Load;
        }

        void View_Load(object sender, EventArgs e)
        {
        	View.Model.Message = "Testar Web Forms MVP!";
        }
	}
}